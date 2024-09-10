namespace Domain
{
    public class DeveloperTeamPlacement
    {
        public Guid DeveloperId { get; set; }
        public Guid DevelopmentTeamId { get; set; }
        public Developer Developer { get; set; }
        public Team DevelopmentTeam { get; set; }
    }
}