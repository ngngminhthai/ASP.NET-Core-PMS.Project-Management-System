using Microsoft.EntityFrameworkCore;
using PMS.Application.Services.Conversations;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.Data.Entities.ConversationAggregate;

namespace PMS.Application.Implementations.Conversations
{
    public class ConversationService : IConversationService
    {
        private readonly ManageAppDbContext context;

        public ConversationService(ManageAppDbContext context)
        {
            this.context = context;
        }



        public List<Conversation> getAllConversationByUser(string userId)
        {
            return context.ConversationUsers
                .Include(c => c.Conversation)
                .Include(c => c.User)
                .Where(c => c.UserId == userId)
                .Select(c => c.Conversation)
                .ToList();
        }

        public Conversation GetConversation(int id)
        {
            return context.Conversations.Include(c => c.Messages).FirstOrDefault(c => c.Id == id);
        }


    }
}
