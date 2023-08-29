// Đây là kho lưu trữ interface
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.interfaces
{
    public interface IUserRepository
    {
        // Cập nhật người dùng từ Ứng dụng
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string userName);

        // Thêm 2 phương thức mới

        // Trả về các thành viên
        Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
        // Trả về 1 thành viên
        Task<MemberDto> GetMemberAsync(string username);
    }
}