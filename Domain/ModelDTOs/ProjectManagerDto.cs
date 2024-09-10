namespace Domain.ModelsDTOs
{
    public class ProjectManagerDto
    {
        public Guid Id { get; set; }
        public string AppUserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }    
        public string Email { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string CertificateUrl { get; set; }
        public int YearsOfExperience { get; set; }  
        public Guid? CurrentTeamId { get; set; }  
    }
}