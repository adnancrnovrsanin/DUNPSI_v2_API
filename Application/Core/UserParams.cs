namespace Application.Core
{
    public class UserParams
    {
        public string CurrentEmail { get; set; }
        public string OrderBy { get; set; } = "lastActive";
    }
}