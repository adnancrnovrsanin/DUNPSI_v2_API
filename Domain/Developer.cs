namespace Domain
{
    public class Developer
    {
        public Guid Id { get; set; }
        public string AppUserId { get; set; }
        public string Position { get; set; }
        public int NumberOfActiveTasks { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<RequirementManagement> AssignedRequirements { get; set; }
        public ICollection<Rating> ReceivedRatings { get; set; }
        public ICollection<DeveloperTeamPlacement> AssignedTeams { get; set; }
    }
}