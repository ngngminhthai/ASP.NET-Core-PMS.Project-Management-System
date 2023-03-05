using PMS.Data.Entities;
using PMS.Data.IRepositories;
using WebApplication1.Data;

namespace PMS.DataEF.Repositories
{
    public class TagRepository : EFRepository<Tag, int>, ITagRepository
    {
        public TagRepository(ManageAppDbContext context) : base(context)
        {
        }
    }
}
