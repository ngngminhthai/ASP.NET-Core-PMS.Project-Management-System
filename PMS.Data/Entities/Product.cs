using PMS.Infrastructure.Interfaces;
using PMS.Infrastructure.SharedKernel;
using System;

namespace WebApplication1.Data.Entities
{
    public class Product : DomainEntity<int>, IUpdateTimeStamp
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
