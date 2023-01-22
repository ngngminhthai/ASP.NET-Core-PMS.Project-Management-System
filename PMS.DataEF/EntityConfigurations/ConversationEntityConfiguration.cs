using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Data.Entities.ConversationAggregate;

namespace PMS.DataEF.EntityConfigurations
{
    public class ConversationEntityConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.HasOne(c => c.Admin)
                   .WithMany()
                   .HasForeignKey(c => c.AdminId);
        }
    }
}
