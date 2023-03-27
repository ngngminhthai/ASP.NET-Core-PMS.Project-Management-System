using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PMS.Application.Services.Conversations;
using PMS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Entities.ProjectAggregate;

namespace Test.Conversations
{

    [TestFixture]
    public class ConversationController
    {
        private Mock<IConversationService> _conversationServiceMock;
        private ManageAppDbContext context;

        [SetUp]
        public void Setup()
        {
            _conversationServiceMock = new Mock<IConversationService>();
            var options = new DbContextOptionsBuilder<ManageAppDbContext>()
                .UseSqlServer("Server=localhost\\SQLEXPRESS; database=SignalRCRUD; Integrated security=true; TrustServerCertificate=true")
                .Options;
            context = new ManageAppDbContext(options);

        }


        public void GetMessageOfConversation()
        {
            int id = 3;
            int skipCount = 0;

            var a = context.Messages
                .Where(m => m.ConversationId == id)
                .OrderByDescending(m => m.DateCreated)
                .Skip(skipCount)
                .Take(5)
                .OrderBy(m => m.DateCreated)
                .ToList();


            foreach (var item in a)
            {
                Debug.WriteLine(item.Id);
            }

        }

        public void UpdateProduc()
        {
            var user = new ManageUser { Id = "3d3cce63-d843-482c-aa23-9d09bf1f7542", Email = "emlasieunhan117@gmail.com" };
            context.Update(user);
            context.SaveChanges();

            var product = new Product { Id = 1, Price = 999 };
            context.Update(product);
            context.SaveChanges();
        }

        [Test]
        public void Seed()
        {
            var p = new Project { Name = "TestCPM" };
            context.Projects.Add(p);
            context.SaveChanges();

            var kanbanColume = new KanbanColume { NameColume = "To Do", ProjectId = p.Id };
            context.kanbanColumes.Add(kanbanColume);
            context.SaveChanges();


            var taskA = new ProjectTask { Name = "A", StartDate = DateTime.Parse("1/1/2022"), EndDate = DateTime.Parse("1/8/2022"), Project = p, ParentId = 0, KanbanColumeID = kanbanColume.Id };
            var taskB = new ProjectTask { Name = "B", StartDate = DateTime.Parse("1/1/2022"), EndDate = DateTime.Parse("1/10/2022"), Project = p, ParentId = 0, KanbanColumeID = kanbanColume.Id };
            var taskC = new ProjectTask { Name = "C", StartDate = DateTime.Parse("1/1/2022"), EndDate = DateTime.Parse("1/13/2022"), Project = p, ParentId = 0, KanbanColumeID = kanbanColume.Id };
            var taskD = new ProjectTask { Name = "D", StartDate = DateTime.Parse("1/1/2022"), EndDate = DateTime.Parse("1/9/2022"), Project = p, ParentId = 0, KanbanColumeID = kanbanColume.Id };
            var taskE = new ProjectTask { Name = "E", StartDate = DateTime.Parse("1/1/2022"), EndDate = DateTime.Parse("1/10/2022"), Project = p, ParentId = 0, KanbanColumeID = kanbanColume.Id };
            var taskF = new ProjectTask { Name = "F", StartDate = DateTime.Parse("1/1/2022"), EndDate = DateTime.Parse("1/7/2022"), Project = p, ParentId = 0, KanbanColumeID = kanbanColume.Id };
            var taskG = new ProjectTask { Name = "G", StartDate = DateTime.Parse("1/1/2022"), EndDate = DateTime.Parse("1/6/2022"), Project = p, ParentId = 0, KanbanColumeID = kanbanColume.Id };

            taskC.DependentTasks.Add(taskA);
            taskF.DependentTasks.Add(taskC);
            taskD.DependentTasks.Add(taskA);
            taskD.DependentTasks.Add(taskB);
            taskE.DependentTasks.Add(taskD);
            taskF.DependentTasks.Add(taskE);
            taskG.DependentTasks.Add(taskE);

            List<ProjectTask> pt = new List<ProjectTask>();
            pt.Add(taskA);
            pt.Add(taskB);
            pt.Add(taskC);
            pt.Add(taskD);
            pt.Add(taskE);
            pt.Add(taskF);
            pt.Add(taskG);

            context.ProjectTasks.AddRange(pt);
            context.SaveChanges();

            foreach (var item in pt)
            {
                item.UpdateDependencies();
            }
            context.SaveChanges();





            p.CalculateCriticalPath();
            context.Projects.Add(p);
            context.SaveChanges();
        }



    }
}
