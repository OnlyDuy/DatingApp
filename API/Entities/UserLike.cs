using API.Entities;

namespace API.Entities
{
    public class UserLike
    {
        public AppUser SourceUser { get; set; } = default!;
        public int SourceUserId { get; set; }

        public AppUser LikedUser { get; set; } = default!;
        public int LikedUserId { get; set; }
    }
}