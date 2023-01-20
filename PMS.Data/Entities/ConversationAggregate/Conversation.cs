using PMS.Infrastructure.SharedKernel;
using System.Collections.Generic;

namespace WebApplication1.Data.Entities.ConversationAggregate
{
    public class Conversation : DomainEntity<int>
    {
        public string AdminId { get; set; }
        public ManageUser Admin { get; set; }
        public ICollection<ConversationUser> ConversationUser { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
