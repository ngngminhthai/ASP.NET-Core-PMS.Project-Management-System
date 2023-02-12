using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Entities.ProjectAggregate;

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
            //var user;
            if (!_userManager.Users.Any())
            {
                await _userManager.CreateAsync(new ManageUser()
                {
                    UserName = "emlasieunhan118@gmail.com",
                    Email = "emlasieunhan118@gmail.com",
                }, "Pa$$w0rd");
                var user = await _userManager.FindByNameAsync("emlasieunhan118@gmail.com");
                await _userManager.AddToRoleAsync(user, "Admin");

                await _userManager.CreateAsync(new ManageUser()
                {
                    UserName = "emlasieunhan119@gmail.com",
                    Email = "emlasieunhan119@gmail.com",
                }, "Pa$$w0rd");
                user = await _userManager.FindByNameAsync("emlasieunhan119@gmail.com");
                await _userManager.AddToRoleAsync(user, "User");

                await _userManager.CreateAsync(new ManageUser()
                {
                    UserName = "emlasieunhan117@gmail.com",
                    Email = "emlasieunhan117@gmail.com",
                }, "Pa$$w0rd");
                user = await _userManager.FindByNameAsync("emlasieunhan117@gmail.com");
                await _userManager.AddToRoleAsync(user, "User");
            }
            if (_context.Products.Count() == 0)
            {
                for (int i = 1; i <= 20; i++)
                {
                    _context.Products.Add(new Product { Name = $"Product {i}", Price = i * 10, TestProperty = "123" });
                }
            }
            if (_context.Projects.Count() == 0)
            {
                var user1 = await _userManager.FindByNameAsync("emlasieunhan118@gmail.com");
                var user2 = await _userManager.FindByNameAsync("emlasieunhan117@gmail.com");
                List<Project> projects = new List<Project>()

                {
                   new Project{Name= "Singleton", Description = "Singleton is a creational design pattern that lets you ensure that a class has only one instance, while providing a global access point to this instance.", Creator = user1  },
                   new Project{Name= "vip", Description = "asdad asdasd adadsd adasdasd adasd asdasdas", Creator = user2  },
                   new Project{Name= "adu", Description = "adu adu adu adu adu adu adu adu adu adu", Creator = user2  },
                   new Project{Name= "promax", Description = "aa aaa aaa aaa aaa aaa aaa aaaaa aaaaa  aaaaa a aa aaaa aa aaa", Creator = user2  }
                };
                _context.Projects.AddRange(projects);
            }
            if (_context.ProjectComments.Count() == 0)
            {
                var user1 = await _userManager.FindByNameAsync("emlasieunhan118@gmail.com");
                var project1 = _context.Projects.Where(p => p.Name.Equals("Singleton")).FirstOrDefault();
                List<ProjectComment> projectComment = new List<ProjectComment>()

                {
                   new ProjectComment{Project= project1, Author = user1 ,NumberOfLike =1
                   ,Content = "Singleton is a creational design pattern that lets you ensure that a class has only one instance, while providing a global access point to this instance."
                   , level=0  },

                };
                _context.ProjectComments.AddRange(projectComment);
            }
            await _context.SaveChangesAsync();
        }
    }
}
