using Microsoft.AspNetCore.Identity;
using PMS.Data.Entities.ProjectAggregate;
using System;
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

                List<ProjectTask> projects1Tasks = new List<ProjectTask>
                {
                    new ProjectTask{Name = "Project Task 1", Description = "Project Task 1", PriorityValue = 1, WorkingStatusValue = 1,
                        ProjectTask_Users = new List<ProjectTask_User>
                        {
                            new ProjectTask_User{User = user1}
                        }, EndDate = new DateTime(2023, 3, 5), StartDate = new DateTime(2023, 3, 2)
                    },
                    new ProjectTask{Name = "Project Task 2", Description = "Project Task 2", PriorityValue = 2, WorkingStatusValue = 2,  ProjectTask_Users = new List<ProjectTask_User>
                        {
                            new ProjectTask_User{User = user1}
                        }, EndDate = new DateTime(2023, 3, 3), StartDate = new DateTime(2023, 3, 2)
                    },
                    new ProjectTask{Name = "Project Task 3", Description = "Project Task 3", PriorityValue = 3, WorkingStatusValue = 1
                    ,EndDate = new DateTime(2023, 3, 2)
                    }
                };

                List<ProjectTask> projects2Tasks = new List<ProjectTask>
                {
                    new ProjectTask{Name = "Project Task 4", Description = "Project Task 1", PriorityValue = 1, WorkingStatusValue = 1,
                        ProjectTask_Users = new List<ProjectTask_User>
                        {
                            new ProjectTask_User{User = user1}
                        }, EndDate = new DateTime(2023, 3, 6), StartDate = new DateTime(2023, 3, 2)
                    },
                    new ProjectTask{Name = "Project Task 5", Description = "Project Task 2", PriorityValue = 2, WorkingStatusValue = 2,  ProjectTask_Users = new List<ProjectTask_User>
                        {
                            new ProjectTask_User{User = user1}
                        }, EndDate = new DateTime(2023, 2, 26), StartDate = new DateTime(2023, 3, 2)
                    },
                    new ProjectTask{Name = "Project Task 6", Description = "Project Task 3", PriorityValue = 3, WorkingStatusValue = 1, EndDate = new DateTime(2023, 3, 4)}
                };

                List<Project> projects = new List<Project>()

                {
                   new Project{Name= "Project 1", Description = "Singleton is a creational design pattern that lets you ensure that a class has only one instance, while providing a global access point to this instance.", Creator = user1, ProjectUploadedFiles =  new List<ProjectUploadedFile>()
                   {
                       new ProjectUploadedFile{File = "20230207135547660godocker.jpg"},
                       new ProjectUploadedFile{File = "20230207182543323dienthoai.jpg"},
                   }, ProjectTasks = projects1Tasks
                },
                   new Project{Name= "vip", Description = "asdad asdasd adadsd adasdasd adasd asdasdas", Creator = user2},
                   new Project{Name= "adu", Description = "adu adu adu adu adu adu adu adu adu adu", Creator = user2  },
                   new Project{Name= "promax", Description = "aa aaa aaa aaa aaa aaa aaa aaaaa aaaaa  aaaaa a aa aaaa aa aaa", Creator = user2  },
                   new Project{Name= "Project 2", Description = "asdad asdasd adadsd adasdasd adasd asdasdas", Creator = user2, ProjectTasks = projects2Tasks },

                };
                _context.Projects.AddRange(projects);
                _context.SaveChanges();

                var project1 = _context.Projects.FirstOrDefault(p => p.Name.Equals("Project 1"));
                var project2 = _context.Projects.FirstOrDefault(p => p.Name.Equals("Project 2"));

                List<ProjectUser> projectsUsers = new List<ProjectUser>()
                {
                    new ProjectUser{User = user1, Project = project1},
                    new ProjectUser{User = user1, Project = project2},
                };


                _context.ProjectUsers.AddRange(projectsUsers);
            }



            if (_context.ProjectComments.Count() == 0)
            {
                var user1 = await _userManager.FindByNameAsync("emlasieunhan118@gmail.com");
                var project1 = _context.Projects.Where(p => p.Name.Equals("Project 1")).FirstOrDefault();
                List<ProjectComment> projectComment = new List<ProjectComment>()

                {
                   new ProjectComment{Project= project1, Author = user1 ,NumberOfLike =1
                   ,Content = "Singleton is a creational design pattern that lets you ensure that a class has only one instance, while providing a global access point to this instance."
                   , level=0  },

                };
                _context.ProjectComments.AddRange(projectComment);
            }

            if (_context.ProjectFunctions.Count() == 0)
            {
                var projectFunctions = new List<ProjectFunction>()
                {
                    new ProjectFunction(){Id = "TASK", Name = "Project Task"},
                    new ProjectFunction(){Id = "MEMBER", Name = "Project Task"},
                    new ProjectFunction(){Id = "DELEGATION", Name = "Project Task"},
                    new ProjectFunction(){Id = "FILES", Name = "Project Task"},
                };
                _context.ProjectFunctions.AddRange(projectFunctions);
                await _context.SaveChangesAsync();
            }


            if (_context.ProjectRoles.Count() == 0)
            {
                var project1 = _context.Projects.Where(p => p.Name.Equals("Project 1")).FirstOrDefault();

                var projectRoles = new List<ProjectRole>()
                {
                    new ProjectRole(){Name = "Administrator", Project = project1},
                };
                _context.ProjectRoles.AddRange(projectRoles);
                await _context.SaveChangesAsync();
            }

            if (_context.ProjectRole_Users.Count() == 0)
            {
                var adminRole = _context.ProjectRoles.FirstOrDefault(r => r.Name.Equals("Administrator"));

                var ProjectRole_Users = new List<ProjectRole_User>()
                {
                    new ProjectRole_User(){User = await _userManager.FindByNameAsync("emlasieunhan118@gmail.com"), ProjectRole = adminRole},
                };
                _context.ProjectRole_Users.AddRange(ProjectRole_Users);
                await _context.SaveChangesAsync();
            }

            if (_context.ProjectPermissions.Count() == 0)
            {
                var adminRole = _context.ProjectRoles.FirstOrDefault(r => r.Name.Equals("Administrator"));

                var ProjectPermissions = new List<ProjectPermission>()
                {
                    new ProjectPermission(){FunctionId = "TASK", ProjectRole = adminRole, CanCreate = true, CanDelete = true, CanRead = true, CanUpdate = true},
                };
                _context.ProjectPermissions.AddRange(ProjectPermissions);
                await _context.SaveChangesAsync();
            }


            await _context.SaveChangesAsync();
        }
    }
}
