using Domain.ModelsDTOs;

namespace Application.Teams
{
    public class TeamCreateDto
    {
        public Guid Id { get; set; }
        public List<DeveloperDto> Developers { get; set; }
    }
}