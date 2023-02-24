using PMS.Infrastructure.SharedKernel;

namespace PMS.Data.Entities
{
    public class Tag : DomainEntity<int>
    {
        string TagName { get; set; }
    }
}
