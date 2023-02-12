using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WebApplication1.Data;

public class ManageAppDbContextFactory : IDesignTimeDbContextFactory<ManageAppDbContext>
{
    ManageAppDbContext IDesignTimeDbContextFactory<ManageAppDbContext>.CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ManageAppDbContext>();
        optionsBuilder.UseSqlServer("Server=tcp:pms221.database.windows.net,1433;Initial Catalog=PMS;Persist Security Info=False;User ID=pms;Password=Nguyenminhthai0602;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        return new ManageAppDbContext(optionsBuilder.Options);
    }
}