using PMS.Infrastructure.Interfaces;
using PMS.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data.Entities.ConversationAggregate
{
    public class Message : DomainEntity<int>, IUpdateTimeStamp
    {
        public string Text { get; set; }
        /*        public int ConversationId { get; set; }
        */
        public string SenderId { get; set; }

        public int ConversationId { get; set; }

        [ForeignKey("ConversationId")]
        public Conversation Conversation { get; set; }

        [ForeignKey("SenderId")]
        public ManageUser User { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
