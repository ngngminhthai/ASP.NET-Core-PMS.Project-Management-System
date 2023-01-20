using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TeduCoreApp.Data.Entities
{
    public class AppRole : IdentityRole
    {
        [Key]
        public override string Id { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
    }
}
