using API.Data;
using API.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        // Sử dụng dấu gạch dưới để có quyền truy cập vào ngữ cảnh phạm vi cơ sở dữ liệu 
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            // Đưa người dùng từ cơ sở dữ liệu vào 1 danh sách
            // để sử dụng 2 danh sách, cần đưa vào 1 khung, lớp hoặc 1 không gian tên khác và gọi Tolisst()
            var users = _context.Users.ToListAsync();
            return await users;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(); // Trả về mã lỗi 404 nếu không tìm thấy user.
            }
            return user;
        }
    }
}