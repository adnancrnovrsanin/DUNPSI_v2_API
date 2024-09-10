namespace Domain.ModelsDTOs
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public string SenderId { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPhotoUrl { get; set; }
        public string RecipientId { get; set; }
        public string RecipientEmail { get; set; }
        public string RecipientPhotoUrl { get; set; }
        public string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; }
    }
}