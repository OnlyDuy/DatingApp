using API.Extensions;

namespace API.Entites
{
    public class AppUser {
        public int Id { get; set; }
        public string UserName { get; set; } = default!;
        public byte[] PasswordHash { get; set; } = default!;
        public byte[] PasswordSalt { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; } = default!;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public string Gender { get; set; } = default!;
        public string Introduction { get; set; } = default!;
        public string LookingFor { get; set; } = default!;
        public string Interests { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public ICollection<Photo> Photos { get; set; } = default!;

        public int GetAge()
        {
            return DateOfBirth.CalculateAge();
        }
    }

    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; } = default!;
        public bool IsMain { get; set; }
        public string PublicId { get; set; } = default!;
        
    }
}