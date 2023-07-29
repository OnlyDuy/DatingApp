using API.Entites;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    // kế thừa từ ngữ cảnh DB
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        // Thiết lập 1 đối tượng User đưa vào API
        public DbSet<AppUser> Users { get; set; }

        // Cấu hình lớp khơi rđộng này để có thể đưa DataContext vào các phân khác của ứng dụng
        
    }
}