namespace Domain
{
    public class RequirementManagement
    {
        public Guid Id { get; set; }
        public Guid AssigneeId { get; set; }
        public Guid RequirementId { get; set; }
        public string Note { get; set; }
        public DateTime DueDate { get; set; }
        public string MediaUrl { get; set; }
        public Requirement Requirement { get; set; }
        public Developer Assignee { get; set; }
    }
}