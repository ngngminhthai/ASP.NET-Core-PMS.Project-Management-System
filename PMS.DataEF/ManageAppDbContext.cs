using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PMS.Data.Entities.UserAggregate;
using PMS.DataEF.EntityConfigurations;
using PMS.Infrastructure.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Entities.ConversationAggregate;
using WebApplication1.Data.Entities.ProjectAggregate;
using WebApplication1.Data.Entities.UserAggregate;

namespace WebApplication1.Data
{
    public class ManageAppDbContext : IdentityDbContext<ManageUser>
    {
        public ManageAppDbContext(DbContextOptions options) : base(options)
        {

        }

        public ManageAppDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.ApplyConfiguration(new IdentityRoleEntityConfiguration());
            builder.ApplyConfiguration(new ManageUserEntityConfiguration());
            builder.ApplyConfiguration(new ConversationUserEntityConfiguration());
            builder.ApplyConfiguration(new ConversationEntityConfiguration());
            builder.ApplyConfiguration(new MessageEntityConfiguration());
            builder.ApplyConfiguration(new ProjectUserEntityConfiguration());

            #region Configurations
            /*builder.Entity<IdentityRole>().Property(x => x.Id).HasMaxLength(50).IsRequired(true);
            builder.Entity<ManageUser>().Property(x => x.Id).HasMaxLength(50).IsRequired(true);


            builder.Entity<ConversationUser>()
              .HasKey(e => new { e.UserId, e.ConversationId });

            builder.Entity<Conversation>()
                .HasOne(c => c.Admin)
                .WithMany()
                .HasForeignKey(c => c.AdminId);

            builder.Entity<ConversationUser>()
                .HasOne(e => e.User)
                .WithMany(s => s.ConversationUser)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ConversationUser>()
                .HasOne(e => e.Conversation)
                .WithMany(c => c.ConversationUser)
                .HasForeignKey(e => e.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Message>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.SenderId);

            builder.Entity<ProjectUser>()
             .HasKey(e => new { e.UserId, e.ProjectId });

            builder.Entity<ProjectUser>()
               .HasOne(e => e.User)
               .WithMany(s => s.ProjectUsers)
               .HasForeignKey(e => e.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProjectUser>()
                .HasOne(e => e.Project)
                .WithMany(c => c.ProjectUsers)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);*/
            #endregion
        }


        public DbSet<ManageUser> ManageUsers { get; set; }
        public DbSet<ConversationUser> ConversationUsers { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<ProjectComment> ProjectComments { get; set; }
        public DbSet<UserCalendar> UserCalendars { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<TicketResponse> TicketResponses { get; set; }
        public DbSet<UploadedFiles> UploadedFiles { get; set; }


        public override int SaveChanges()
        {
            TrackingEntities();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            TrackingEntities();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        private void TrackingEntities()
        {
            var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);

            foreach (EntityEntry item in modified)
            {
                var changedOrAddedItem = item.Entity as IUpdateTimeStamp;
                if (changedOrAddedItem != null)
                {
                    if (item.State == EntityState.Added)
                    {
                        changedOrAddedItem.DateCreated = DateTime.Now;
                    }
                    changedOrAddedItem.DateModified = DateTime.Now;
                }
            }
        }

    }
}
