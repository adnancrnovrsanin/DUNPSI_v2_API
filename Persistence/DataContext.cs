using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected DataContext()
        {
        }

        public DbSet<Developer> Developers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ProductManager> ProductManagers { get; set; }
        public DbSet<ProjectManager> ProjectManagers { get; set; }
        public DbSet<ProjectPhase> ProjectPhases { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Requirement> Requirements { get; set; }
        public DbSet<RequirementManagement> RequirementManagements { get; set; }
        public DbSet<SoftwareCompany> SoftwareCompanies { get; set; }
        public DbSet<SoftwareProject> SoftwareProjects { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<DeveloperTeamPlacement> DeveloperTeamPlacements { get; set; }
        public DbSet<InitialProjectRequest> InitialProjectRequests { get; set; }
        public DbSet<Domain.Connection> Connections { get; set; }
        public DbSet<Domain.Group> Groups { get; set; }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DeveloperTeamPlacement>(dtp =>
            {
                dtp.HasKey(dt => new { dt.DeveloperId, dt.DevelopmentTeamId });

                dtp.HasOne(x => x.Developer)
                    .WithMany(x => x.AssignedTeams)
                    .HasForeignKey(x => x.DeveloperId);
                dtp.HasOne(x => x.DevelopmentTeam)
                    .WithMany(t => t.AssignedDevelopers)
                    .HasForeignKey(x => x.DevelopmentTeamId);
            });

            builder.Entity<RequirementManagement>(rm =>
            {
                rm.HasKey(r => new { r.RequirementId, r.AssigneeId });

                rm.HasOne(x => x.Requirement)
                    .WithMany(x => x.Assignees)
                    .HasForeignKey(x => x.RequirementId);

                rm.HasOne(x => x.Assignee)
                    .WithMany(d => d.AssignedRequirements)
                    .HasForeignKey(x => x.AssigneeId);
            });

            builder.Entity<Rating>(r =>
            {
                r.HasOne(x => x.Requirement)
                    .WithMany(x => x.DeveloperRatings)
                    .HasForeignKey(x => x.RequirementId);

                r.HasOne(x => x.ProjectManager)
                    .WithMany(x => x.GivenRatings)
                    .HasForeignKey(x => x.ProjectManagerId);

                r.HasOne(x => x.Developer)
                    .WithMany(x => x.ReceivedRatings)
                    .HasForeignKey(x => x.DeveloperId);
            });

            builder.Entity<Message>(m =>
            {
                m.HasOne(x => x.Sender)
                    .WithMany(x => x.MessagesSent)
                    .HasForeignKey(x => x.SenderId);

                m.HasOne(x => x.Recipient)
                    .WithMany(x => x.MessagesReceived)
                    .HasForeignKey(x => x.RecipientId);
            });

            builder.Entity<InitialProjectRequest>(m => {
                m.HasOne(x => x.AppointedManager)
                    .WithMany(x => x.AppointedRequests)
                    .HasForeignKey(x => x.AppointedManagerId);
            });
        }
    }
}
