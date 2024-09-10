using Domain;

namespace API.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}