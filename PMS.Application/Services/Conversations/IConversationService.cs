using System.Collections.Generic;
using WebApplication1.Data.Entities.ConversationAggregate;

namespace PMS.Application.Services.Conversations
{
    public interface IConversationService
    {
        public List<Conversation> getAllConversationByUser(string userId);
        public Conversation GetConversation(int id);
    }
}
