using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public DataContext(DbContextOptions options, IPasswordHasher<AppUser> passwordHasher) : base(options)
        {
            _passwordHasher = passwordHasher;
        }

        protected DataContext()
        {
        }

        public DbSet<Admin> Admins { get; set; }
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

            builder.Entity<Developer>()
                .HasOne(d => d.AppUser)
                .WithOne()
                .HasForeignKey<Developer>(d => d.AppUserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<ProjectManager>()
                .HasOne(d => d.AppUser)
                .WithOne()
                .HasForeignKey<ProjectManager>(d => d.AppUserId)
                .OnDelete(DeleteBehavior.SetNull);
            
            builder.Entity<ProductManager>()
                .HasOne(d => d.AppUser)
                .WithOne()
                .HasForeignKey<ProductManager>(d => d.AppUserId)
                .OnDelete(DeleteBehavior.SetNull);

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


            AppUser adminUser = new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "admin",
                Email = "admin@example.com",
                Name = "Admin",
                Surname = "Admin",
                Role = Role.ADMIN,
                EmailConfirmed = true
            };

            adminUser.PasswordHash = _passwordHasher.HashPassword(adminUser, "Test123.");

            builder.Entity<AppUser>().HasData(
                adminUser
            );

            builder.Entity<Admin>().HasData(
               new Admin
               {
                   Id = Guid.NewGuid(),
                   AppUserId = adminUser.Id
               }
            );
        }
    }
}
