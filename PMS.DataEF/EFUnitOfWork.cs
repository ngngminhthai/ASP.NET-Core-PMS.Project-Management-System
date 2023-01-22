using PMS.Infrastructure.SharedKernel;
using WebApplication1.Data;

namespace PMS.DataEF.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly ManageAppDbContext _context;
        public EFUnitOfWork(ManageAppDbContext context)
        {
            _context = context;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
