using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PMS.DataEF.EntityConfigurations
{
    public class PriorityEntityConfiguration : IEntityTypeConfiguration<Priority>
    {

        public void Configure(EntityTypeBuilder<Priority> builder)
        {
            builder.HasNoKey();
        }
    }
}
