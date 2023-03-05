using PMS.Application.Services.Conversations;
using System;
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

        public List<Message> GetMessageOfConversation(int id, int skipCount)
        {
            var messages = context.Messages
                .Where(m => m.ConversationId == id)
                .OrderByDescending(m => m.DateCreated)
                .Skip(skipCount)
                .Take(5)
                .OrderBy(m => m.DateCreated)
                .ToList();

            return messages;
            //return context.Messages.Where(m => m.ConversationId == id).ToList();
        }
        public Message AddMessage(int conversationId, string text, string senderId)
        {
            var message = new Message { ConversationId = conversationId, Text = text, SenderId = senderId, DateCreated = DateTime.Now };
            try
            {
                context.Messages.Add(message);
                context.SaveChanges();
                return message;
            }
            catch (Exception e)
            {

            }
            return null;
        }
        public List<Conversation> ConversationsOfUser(string id)
        {
            return conversationService.getAllConversationByUser(id);
        }


    }
}
