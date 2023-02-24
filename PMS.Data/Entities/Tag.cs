using PMS.Infrastructure.SharedKernel;

namespace PMS.Data.Entities
{
    public class Tag : DomainEntity<int>
    {
        public string TagName { get; set; }
    }
}
