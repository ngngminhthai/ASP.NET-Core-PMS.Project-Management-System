using PMS.Infrastructure.SharedKernel;

namespace WebApplication1.Data.Entities
{
    public class Product : DomainEntity<int>
    {
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
