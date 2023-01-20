using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data.Entities
{
    public class AppRole : IdentityRole
    {
        [StringLength(250)]
        public string Description { get; set; }
    }
}
