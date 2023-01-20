using PMS.Infrastructure.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data.Entities
{
    [Table("Functions")]
    public class Function : DomainEntity<string>
    {

        [Required]
        [StringLength(128)]
        public string Name { set; get; }

        [Required]
        [StringLength(250)]
        public string URL { set; get; }


        [StringLength(128)]
        public string ParentId { set; get; }

        public Status Status { set; get; }
    }
}
