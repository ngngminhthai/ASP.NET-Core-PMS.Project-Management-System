using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Data.Entities.ConversationAggregate;

namespace PMS.DataEF.EntityConfigurations
{
    public class MessageEntityConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasOne(m => m.User)
                   .WithMany()
                   .HasForeignKey(m => m.SenderId);
        }
    }
}
