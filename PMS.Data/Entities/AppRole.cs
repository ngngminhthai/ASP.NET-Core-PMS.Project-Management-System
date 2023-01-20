using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data.Entities
{
    public class AppRole : IdentityRole
    {
        [Key]
        public override string Id { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
    }
}
