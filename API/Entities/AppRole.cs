using Microsoft.AspNetCore.Identity;


// Đây là một mối quan hệ nhiều nhiều khác
// Mỗi người dùng ứng dụng có thể có nhiều vai trò
// Mỗi vai trò có thể có nhiều người dùng
namespace API.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}