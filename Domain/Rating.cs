namespace Domain
{
    public class Rating
    {
        public Guid Id { get; set; }
        public Guid RequirementId { get; set; }
        public Guid ProjectManagerId { get; set; }
        public Guid DeveloperId { get; set; }
        public int RatingValue { get; set; }
        public string Comment { get; set; }
        public DateTime DateTimeRated { get; set; } = DateTime.UtcNow;
        public Requirement Requirement { get; set; }
        public ProjectManager ProjectManager { get; set; }
        public Developer Developer { get; set; }
    }
}