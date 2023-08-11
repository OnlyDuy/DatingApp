using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entites;
using API.interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using API.Extensions;

namespace API.Controllers
{
    [Authorize]
    // Ủy quyền
    public class UsersController : BaseApiController
    {
        // Sử dụng dấu gạch dưới để có quyền truy cập vào ngữ cảnh phạm vi cơ sở dữ liệu 
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            // Đưa người dùng từ cơ sở dữ liệu vào 1 danh sách
            // để sử dụng 2 danh sách, cần đưa vào 1 khung, lớp hoặc 1 không gian tên khác và gọi Tolisst()
            var users = await _userRepository.GetMembersAsync();
            return Ok(users);
        }

        [HttpGet("{username}", Name = "GetUser")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var user = await _userRepository.GetMemberAsync(username);
            return user;
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {   
            // Lấy tên người dùng trong trường hợp cập nhật
            var username = User.GetUsername();
            var user = await _userRepository.GetUserByUsernameAsync(username);

            // Đang cập nhật hoặc sự dụng điều này để cập nhật 1 đối tượng thì có thể dùng Map()
            _mapper.Map(memberUpdateDto, user);

            _userRepository.Update(user);

            if (await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user");
        }

        // Controller cho phép người dùng thêm ảnh mới 
        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            // Nhận người dùng theo tên
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
            // kết quả là 1 dịch vụ ảnh
            var result = await _photoService.AddPhotoAsync(file);
            // Kiểm tra lỗi
            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                // Url an toàn và tuyệt đối
                Url = result.SecureUrl.AbsoluteUri,
                // Id công khai
                PublicId = result.PublicId
            };


            // Kiểm tra liệu người dùng có bất kỳ bức ảnh nào vào lúc này hay không
            if (user.Photos.Count == 0)
            {
                // Nếu không có thì đây là bức ảnh đầu tiên đc tải lên và nó là ảnh chính   
                photo.IsMain = true;
            }

            // Thêm hình ảnh
            user.Photos.Add(photo);

            // Lưu cấc thay đổi
            if (await _userRepository.SaveAllAsync())
            {
                // Trả lại người dùng và ảnh của người dùng
                return CreatedAtRoute("GetUser", new {username = user.UserName} ,_mapper.Map<PhotoDto>(photo));
            }
            
            return BadRequest("Problem adding photo");
        }

    }
}