using PMS.Application.Services.Conversations;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.Data.Entities.ConversationAggregate;

namespace PMS.Controllers
{
    public class ConversationController : BaseController
    {
        private readonly IConversationService conversationService;
        private readonly ManageAppDbContext context;

        public ConversationController(IConversationService conversationService, ManageAppDbContext context)
        {
            this.conversationService = conversationService;
            this.context = context;
        }

        public List<Message> GetMessageOfConversation(int id)
        {
            return context.Messages.Where(m => m.ConversationId == id).ToList();
        }
        public void AddMessage(int conversationId, string text, string senderId)
        {
            context.Messages.Add(new Message { ConversationId = conversationId, Text = text, SenderId = senderId });
            context.SaveChanges();
        }
        public List<Conversation> ConversationsOfUser(string id)
        {
            return conversationService.getAllConversationByUser(id);
        }

    }
}
