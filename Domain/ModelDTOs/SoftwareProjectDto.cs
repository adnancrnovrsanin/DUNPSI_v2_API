namespace Domain.ModelsDTOs
{
    public class SoftwareProjectDto
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DueDate { get; set; }
        public string Status { get; set; }
        public Guid AssignedTeamId { get; set; }
        public TeamDto AssignedTeam { get; set; }
    }
}