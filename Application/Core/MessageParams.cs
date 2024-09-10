namespace Application.Core
{
    public class MessageParams : PaginationParams
    {
        public string Email { get; set; }
        public string Container { get; set; } = "Unread";
    }
}