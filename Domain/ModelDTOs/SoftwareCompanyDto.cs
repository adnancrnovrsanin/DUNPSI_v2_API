namespace Domain.ModelsDTOs
{
    public class SoftwareCompanyDto
    {
        public Guid Id { get; set; }
        public string AppUserId { get; set; }
        public string RepresentativeName { get; set; }
        public string RepresentativeSurname { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Web { get; set; }
        public ICollection<SoftwareProjectDto> CurrentProjects { get; set; }
    }
}