using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Role Role { get; set; }
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}