namespace Domain
{
    public class SoftwareCompany
    {
        public Guid Id { get; set; }
        public string AppUserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Web { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<InitialProjectRequest> InitialProjectRequests { get; set; }
        public ICollection<SoftwareProject> Projects { get; set; }
    }
}