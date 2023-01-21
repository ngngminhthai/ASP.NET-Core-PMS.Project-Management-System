using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Data.Entities;

namespace PMS.DataEF.Repositories
{
    public class InitDatabase
    {
        private readonly ManageAppDbContext _context;
        private UserManager<ManageUser> _userManager;
        private RoleManager<AppRole> _roleManager;

        public InitDatabase()
        {
        }

        public InitDatabase(ManageAppDbContext context, UserManager<ManageUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Description = "Manage all system"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "User",
                    NormalizedName = "User",
                    Description = "Who use the system services"
                });
            }
            if (!_userManager.Users.Any())
            {
                await _userManager.CreateAsync(new ManageUser()
                {
                    UserName = "thainm",
                    Email = "emlasieunhan118@gmail.com",
                }, "Pa$$w0rd");
                var user = await _userManager.FindByNameAsync("thainm");
                await _userManager.AddToRoleAsync(user, "Admin");

                await _userManager.CreateAsync(new ManageUser()
                {
                    UserName = "anhltn",
                    Email = "emlasieunhan119@gmail.com",
                }, "Pa$$w0rd");
                user = await _userManager.FindByNameAsync("anhltn");
                await _userManager.AddToRoleAsync(user, "User");

                await _userManager.CreateAsync(new ManageUser()
                {
                    UserName = "thuantd",
                    Email = "emlasieunhan117@gmail.com",
                }, "Pa$$w0rd");
                user = await _userManager.FindByNameAsync("thuantd");
                await _userManager.AddToRoleAsync(user, "User");
            }
            if (_context.Products.Count() == 0)
            {
                for (int i = 1; i <= 20; i++)
                {
                    _context.Products.Add(new Product { Name = $"Product {i}", Price = i * 10 });
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
