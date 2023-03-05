using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PMS.Application.Services.Conversations;
using System.Diagnostics;
using System.Linq;
using WebApplication1.Data;

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


        [Test]
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



    }
}
