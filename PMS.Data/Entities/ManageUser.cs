using Microsoft.AspNetCore.Identity;
using PMS.Data.Entities.ProjectAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Data.Entities.ConversationAggregate;
using WebApplication1.Data.Entities.ProjectAggregate;
namespace WebApplication1.Data.Entities
{
    public class ManageUser : IdentityUser
    {
        [Key]
        override public string Id { get; set; }

        public string DisPlayName { get; set; }

        public DateTime BirthDay { get; set; }

        public string ImageProfile { get; set; }

        public virtual ICollection<ConversationUser> ConversationUser { get; set; }
        public ICollection<ProjectUser> ProjectUsers { get; set; }
        public ICollection<ProjectTask_User> ProjectTask_Users { get; set; }

    }
}
