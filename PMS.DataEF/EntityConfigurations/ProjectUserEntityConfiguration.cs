using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Data.Entities.ProjectAggregate;

namespace PMS.DataEF.EntityConfigurations
{
    public class ProjectUserEntityConfiguration : IEntityTypeConfiguration<ProjectUser>
    {
        public void Configure(EntityTypeBuilder<ProjectUser> builder)
        {
            builder.HasKey(e => new { e.UserId, e.ProjectId });

            builder.HasOne(e => e.User)
                   .WithMany(s => s.ProjectUsers)
                   .HasForeignKey(e => e.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Project)
                    .WithMany(c => c.ProjectUsers)
                    .HasForeignKey(e => e.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
