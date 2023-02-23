using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Data.Entities.ProjectAggregate;

namespace PMS.DataEF.EntityConfigurations
{
    public class ProjecTask_UserEntityConfiguration : IEntityTypeConfiguration<ProjectTask_User>
    {
        public void Configure(EntityTypeBuilder<ProjectTask_User> builder)
        {
            builder.HasKey(e => new { e.UserId, e.ProjectTaskId });

            builder.HasOne(e => e.User)
                   .WithMany(s => s.ProjectTask_Users)
                   .HasForeignKey(e => e.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.ProjectTask)
                    .WithMany(c => c.ProjectTask_Users)
                    .HasForeignKey(e => e.ProjectTaskId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
