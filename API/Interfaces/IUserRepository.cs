// Đây là kho lưu trữ interface
using API.Entites;

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
    }
}