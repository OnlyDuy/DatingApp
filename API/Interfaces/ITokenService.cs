using API.Entities;

namespace API.interfaces
{
    // Một giao diện sẽ không có logic triển khai nào 
    // Chỉ chứa tên của chức năng mà giao diện cung cấp
    public interface ITokenService
    {
        // Tạo mã thông báo
        Task<string> CreateToken(AppUser user);
    }
}