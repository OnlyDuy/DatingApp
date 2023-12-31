namespace API.DTOs
{
    public class UserDto
    {
        public string Username { get; set; } = default!;
        public string Token { get; set; } = default!;
        public string PhotoUrl { get; set; } = default!;
        public string KnownAs { get; set; } = default!;
        public string Gender { get; set; } = "";
    }
}