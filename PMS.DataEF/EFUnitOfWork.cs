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
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
