using PMS.Infrastructure.SharedKernel;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Data.Entities.ConversationAggregate;

namespace PMS.Data.Entities.ConversationAggregate
{
    public class ConversationUploadedFile : DomainEntity<int>
    {
        public string File { get; set; }
        public int ConversationId { get; set; }

        [ForeignKey("ConversationId")]
        public Conversation Conversation { get; set; }
    }
}
