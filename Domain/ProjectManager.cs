namespace Domain
{
    public class ProjectManager
    {
        public Guid Id { get; set; }
        public string AppUserId { get; set; }
        public string CertificateUrl { get; set; }
        public int YearsOfExperience { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<Team> ManagedTeams { get; set; }
        public ICollection<Rating> GivenRatings { get; set; }
        public ICollection<InitialProjectRequest> AppointedRequests { get; set; }
    }
}