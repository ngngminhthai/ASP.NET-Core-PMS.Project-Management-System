using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WebApplication1.Data;

public class ManageAppDbContextFactory : IDesignTimeDbContextFactory<ManageAppDbContext>
{
    ManageAppDbContext IDesignTimeDbContextFactory<ManageAppDbContext>.CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ManageAppDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS; database=SignalRCRUD; Integrated security=true; TrustServerCertificate=true");

        return new ManageAppDbContext(optionsBuilder.Options);
    }
}