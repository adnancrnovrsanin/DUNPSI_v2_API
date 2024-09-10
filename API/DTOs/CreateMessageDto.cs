namespace API.DTOs
{
    public class CreateMessageDto
    {
        public string RecipientEmail { get; set; }
        public string Content { get; set; }
    }
}