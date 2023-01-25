using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;

namespace WebApplication1.Services
{
    public class ClaimsPrincipalFactory : UserClaimsPrincipalFactory<ManageUser, AppRole>
    {
        UserManager<ManageUser> _userManger;

        public ClaimsPrincipalFactory(UserManager<ManageUser> userManager,
            RoleManager<AppRole> roleManager, IOptions<IdentityOptions> options)
            : base(userManager, roleManager, options)
        {
            _userManger = userManager;
        }

        public async override Task<ClaimsPrincipal> CreateAsync(ManageUser user)
        {
            var principal = await base.CreateAsync(user);
            var roles = await _userManger.GetRolesAsync(user);
            ((ClaimsIdentity)principal.Identity).AddClaims(new[]
            {
                new Claim("Email",user.Email),
                new Claim("Roles",string.Join(";",roles))
            });
            return principal;
        }
    }
}
