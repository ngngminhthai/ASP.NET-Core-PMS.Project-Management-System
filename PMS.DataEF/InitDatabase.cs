using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMS.Data.Entities;
using PMS.Data.Entities.ProjectAggregate;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Entities.ConversationAggregate;
using WebApplication1.Data.Entities.ProjectAggregate;
using WebApplication1.Data.Entities.UserAggregate;

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
                    await _context.Products.AddAsync(new Product { Name = $"Product {i}", Price = i * 10, TestProperty = "123" });
                }
            }

            if (_context.Conversations.Count() == 0)
            {
                var user117 = await _userManager.FindByNameAsync("emlasieunhan117@gmail.com");
                var user118 = await _userManager.FindByNameAsync("emlasieunhan118@gmail.com");
                var user119 = await _userManager.FindByNameAsync("emlasieunhan119@gmail.com");


                //117 to 118
                //118 to 119
                /* List<ConversationUser> conversationsUsers1 = new List<ConversationUser>()
                 {
                     new ConversationUser{UserId = user117.Id},
                     new ConversationUser{UserId = user118.Id},
                 };

                 List<ConversationUser> conversationsUsers2 = new List<ConversationUser>()
                 {
                     new ConversationUser{UserId = user118.Id},
                     new ConversationUser{UserId = user119.Id},
                 };*/

                List<Message> messages = new List<Message>()
                {
                    new Message{SenderId = user118.Id, Text = "Xin chao toi la 118"},
                    new Message{SenderId = user119.Id, Text = "Xin chao toi la 119"},
                };


                List<Conversation> conversations = new List<Conversation>
                {
                    new Conversation{AdminId = user117.Id,
                        ConversationUser = new List<ConversationUser>()
                        {
                            new ConversationUser{UserId = user117.Id},
                            new ConversationUser{UserId = user118.Id},
                        },
                        Messages = new List<Message>()
                        {
                            new Message{SenderId = user117.Id, Text = "Xin chao toi la 117"},
                            new Message{SenderId = user118.Id, Text = "Xin chao toi la 118"},
                        },
                        Description = "Group for project PRN221",
                        Name = "PRN221",
                        Type = true
                    },
                    new Conversation{AdminId = user118.Id,
                        ConversationUser =  new List<ConversationUser>()
                        {
                            new ConversationUser{UserId = user118.Id},
                            new ConversationUser{UserId = user119.Id},
                        },
                        Messages = new List<Message>()
                        {
                            new Message{SenderId = user118.Id, Text = "Xin chao toi la 118"},
                            new Message{SenderId = user119.Id, Text = "Xin chao toi la 119"},
                        },
                        Description = "Group for project PRN221",
                        Name = "PRM392",
                        Type = false
                    }
                };

                _context.Conversations.AddRange(conversations);

            }


            if (_context.Functions.Count() == 0)
            {
                List<Function> funcs = new List<Function>
                {
                    new Function(){Id = "user", Name = "USER", URL = "/admin/user", Status = Status.Active},
                    new Function(){Id = "project", Name = "PROJECT", URL = "project", Status = Status.Active},
                    new Function(){Id = "role", Name = "ROLE", URL = "", Status = Status.Active}
                };
                await _context.Functions.AddRangeAsync(funcs);
                await _context.SaveChangesAsync();
            }


            if (_context.Permissions.Count() == 0)
            {
                var adminRole = await _roleManager.FindByNameAsync("Admin");
                var userRole = await _roleManager.FindByNameAsync("User");

                var functionUser = _context.Functions.Find("user");
                var functionProject = _context.Functions.Find("project");
                var functionRole = _context.Functions.Find("role");

                List<Permission> funcs = new List<Permission>
                {
                    new Permission(){RoleId = adminRole.Id.ToString(),CanCreate = true, CanRead = true, CanUpdate = true, CanDelete = true, Function = functionUser},
                    new Permission(){RoleId = adminRole.Id.ToString(),CanCreate = true, CanRead = true, CanUpdate = true, CanDelete = true, Function = functionProject},
                    new Permission(){RoleId = adminRole.Id.ToString(),CanCreate = true, CanRead = true, CanUpdate = true, CanDelete = true, Function = functionRole},
                    new Permission(){RoleId = userRole.Id.ToString(),CanCreate = true, CanRead = true, CanUpdate = true, CanDelete = true, Function = functionProject},
                };
                await _context.Permissions.AddRangeAsync(funcs);
                await _context.SaveChangesAsync();
            }



            if (_context.Projects.Count() == 0)
            {
                List<Tag> tags = new List<Tag> {
                    new Tag {TagName= "IT" },
                    new Tag {TagName= "Marketing" },
                    new Tag {TagName= "Finance" },
                    new Tag {TagName= "Photo" },
                };
                var user1 = await _userManager.FindByNameAsync("emlasieunhan118@gmail.com");
                var user2 = await _userManager.FindByNameAsync("emlasieunhan117@gmail.com");


                List<Project> projects = new List<Project>()

                {
                   new Project{Name= "Singleton", Description = "Singleton is a creational design pattern that lets you ensure that a class has only one instance, while providing a global access point to this instance.",
                       Creator = user1, ProjectUploadedFiles =  new List<ProjectUploadedFile>()
                   {
                       new ProjectUploadedFile{File = "20230207135547660godocker.jpg"},
                       new ProjectUploadedFile{File = "20230207182543323dienthoai.jpg"},
                   },
                    Tag = tags[0]
                   },
                   new Project { Name = "vip", Description = "asdad asdasd adadsd adasdasd adasd asdasdas", Creator = user2 , Tag = tags[0]},
                   new Project { Name = "adu", Description = "adu adu adu adu adu adu adu adu adu adu", Creator = user2,Tag = tags[1] },
                   new Project { Name = "promax", Description = "aa aaa aaa aaa aaa aaa aaa aaaaa aaaaa  aaaaa a aa aaaa aa aaa", Creator = user2 , Tag = tags[0]},
                   new Project { Name = "Lmao", Description = "queeaaaaa", Creator = user2, Tag = tags[0] }
                };
                List<KanbanColume> kanbanColumes = new List<KanbanColume>
                {
                    new KanbanColume{NameColume = "To do", project = projects[0]},
                         new KanbanColume { NameColume = "Doing", project = projects[0] },
                        new KanbanColume { NameColume = "Done", project = projects[0] },
                     new KanbanColume { NameColume = "Test", project = projects[0] },
                     new KanbanColume { NameColume = "To do", project = projects[1] },
                     new KanbanColume { NameColume = "Doing", project = projects[1] },
                new KanbanColume { NameColume = "Done", project = projects[1] },
                new KanbanColume { NameColume = "Bug", project = projects[1] },
                     };
                List<ProjectTask> projects1Tasks = new List<ProjectTask>
                {
                    new ProjectTask{Name = "Project Task 1", Description = "Project Task 1", PriorityValue = 1, WorkingStatusValue = 1,Project = projects[0],KanbanColume= kanbanColumes[0] },
                    new ProjectTask{Name = "Project Task 2", Description = "Project Task 2", PriorityValue = 2, WorkingStatusValue = 2, Project = projects[0],KanbanColume= kanbanColumes[0] },
                    new ProjectTask{Name = "Project Task 3", Description = "Project Task 3", PriorityValue = 3, WorkingStatusValue = 1, Project = projects[0],KanbanColume= kanbanColumes[0] },
                    new ProjectTask{Name = "Project Task 4", Description = "Project Task 4", PriorityValue = 3, WorkingStatusValue = 1, Project = projects[0],KanbanColume= kanbanColumes[0] },
                    new ProjectTask{Name = "Project Task 5", Description = "Project Task 5", PriorityValue = 3, WorkingStatusValue = 1,Project = projects[0],KanbanColume= kanbanColumes[0] },
                };

                List<ProjectUser> projectsUsers = new List<ProjectUser>()
                {
                    new ProjectUser{User = user1, Project = projects[0]},
                    new ProjectUser{User = user1, Project = projects[0]},
                };
                List<ProjectTask_User> projectTask_Users = new List<ProjectTask_User>
                {
                    new ProjectTask_User{ProjectTask = projects1Tasks[0], User= user1},

                };



                await _context.Tags.AddRangeAsync(tags);
                await _context.Projects.AddRangeAsync(projects);
                await _context.kanbanColumes.AddRangeAsync(kanbanColumes);
                await _context.ProjectTasks.AddRangeAsync(projects1Tasks);
                await _context.projectTask_Users.AddRangeAsync(projectTask_Users);
                await _context.ProjectUsers.AddRangeAsync(projectsUsers);
          
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
                List<ProjectComment> repChild = new List<ProjectComment>
                {
                    new ProjectComment{ ParentID = 5, Content ="rep child thuan 0" , Author = user1, level = 2, NumberOfLike = 0},
                    //new ProjectComment{ ParentID = 5, Content ="rep child aaaaaaaa thuan 0 " , Author = user2 , level = 2, NumberOfLike = 0},
                    new ProjectComment{ ParentID = 6, Content ="rep child vipro1" , Author = user1 , level = 2, NumberOfLike = 0}
                };
                await _context.ProjectComments.AddRangeAsync(projectComment);
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
                await _context.ProjectFunctions.AddRangeAsync(projectFunctions);
                await _context.SaveChangesAsync();
            }


            if (_context.ProjectRoles.Count() == 0)
            {
                var project1 = await _context.Projects.Where(p => p.Name.Equals("Project 1")).FirstOrDefaultAsync();

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