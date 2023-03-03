using PMS.Infrastructure.SharedKernel;
using System.Collections.Generic;

namespace WebApplication1.Data.Entities.ConversationAggregate
{
    public class Conversation : DomainEntity<int>
    {
        public string AdminId { get; set; }
        public ManageUser Admin { get; set; }

        //true is individual, false is groupchat
        public bool Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ConversationUser> ConversationUser { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
