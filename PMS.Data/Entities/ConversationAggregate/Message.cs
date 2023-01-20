using PMS.Infrastructure.SharedKernel;

namespace WebApplication1.Data.Entities.ConversationAggregate
{
    public class Message : DomainEntity<int>
    {
        public string Text { get; set; }
        /*        public int ConversationId { get; set; }
        */
        public string SenderId { get; set; }
        public Conversation Conversation { get; set; }
        public ManageUser User { get; set; }

    }
}
