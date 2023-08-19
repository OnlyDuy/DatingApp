using API.Entites;
using API.Entities;
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

        public DbSet<UserLike> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserLike>()
                .HasKey(k => new {k.SourceUserId, k.LikedUserId});

            builder.Entity<UserLike>()
                .HasOne(s => s.SourceUser)
                .WithMany(l => l.LikedUsers)
                .HasForeignKey(s => s.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<UserLike>()
                .HasOne(s => s.LikedUser)
                .WithMany(l => l.LikedByUsers)
                .HasForeignKey(s => s.LikedUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        
    }
}