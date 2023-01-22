using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Data.Entities;

namespace PMS.DataEF.EntityConfigurations
{
    public class ManageUserEntityConfiguration : IEntityTypeConfiguration<ManageUser>
    {
        public void Configure(EntityTypeBuilder<ManageUser> builder)
        {
            builder.Property(x => x.Id).HasMaxLength(50).IsRequired(true);
        }
    }
}
