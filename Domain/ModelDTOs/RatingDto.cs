namespace Domain.ModelsDTOs
{
    public class RatingDto
    {
        public string Id { get; set; }
        public string RequirementId { get; set; }
        public string ProjectManagerId { get; set; }
        public string DeveloperId { get; set; }
        public int RatingValue { get; set; }
        public string Comment { get; set; }
        public string DateTimeRated { get; set; }
    }
}