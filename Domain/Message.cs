namespace Domain
{
    public class Message
    {
        public Guid Id { get; set; }
        public string SenderId { get; set; }
        public string SenderEmail { get; set; }
        public AppUser Sender { get; set; }
        public string RecipientId { get; set; }
        public string RecipientEmail { get; set; }
        public AppUser Recipient { get; set; }
        public string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; } = DateTime.UtcNow;
        public bool SenderDeleted { get; set; }
        public bool RecipientDeleted { get; set; }
    }
}