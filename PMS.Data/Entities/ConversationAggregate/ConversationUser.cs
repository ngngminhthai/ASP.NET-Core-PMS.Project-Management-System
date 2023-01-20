namespace WebApplication1.Data.Entities.ConversationAggregate
{
    public class ConversationUser
    {
        public string UserId { get; set; }
        public ManageUser User { get; set; }
        public int ConversationId { get; set; }
        public Conversation Conversation { get; set; }
    }
}
