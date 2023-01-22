using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Data.Entities.ConversationAggregate;

namespace PMS.DataEF.EntityConfigurations
{
    public class ConversationUserEntityConfiguration : IEntityTypeConfiguration<ConversationUser>
    {
        public void Configure(EntityTypeBuilder<ConversationUser> builder)
        {
            builder.HasKey(e => new { e.UserId, e.ConversationId });

            builder.HasOne(e => e.User)
                   .WithMany(s => s.ConversationUser)
                   .HasForeignKey(e => e.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Conversation)
                    .WithMany(c => c.ConversationUser)
                    .HasForeignKey(e => e.ConversationId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
