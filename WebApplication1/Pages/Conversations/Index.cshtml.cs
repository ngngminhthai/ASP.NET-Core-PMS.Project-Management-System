using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PMS.Application.Services.Conversations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Entities.ConversationAggregate;

namespace PMS.Pages.Conversations
{
    public class IndexModel : PageModel
    {
        private readonly IConversationService conversationService;
        private readonly ManageAppDbContext context;
        private readonly IAuthorizationService authorizationService;
        private readonly UserManager<ManageUser> userManager;

        public IndexModel(IConversationService conversationService, ManageAppDbContext context, IAuthorizationService authorizationService)
        {
            this.conversationService = conversationService;
            this.context = context;
            this.authorizationService = authorizationService;
        }

        [BindProperty]
        public List<Conversation> Conversations { get; set; }

        [BindProperty]
        public Conversation currentConversation { get; set; }
        [BindProperty]
        public string currentUserId { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            /* var result = await authorizationService.AuthorizeAsync(User, new Payload { Resource = "conversation", ConversationRequirement = new ConversationRequirement { ConversationId = (int)id, Action = "View" } }, Operations.Update);
             if (result.Succeeded == false)
                 return Unauthorized();*/

            var username = User.Identity.Name;
            var user = context.Users.FirstOrDefault(u => u.UserName == username);
            currentUserId = user.Id;
            Conversations = conversationService.getAllConversationByUser(user.Id);


            if (id != null)
            {
                currentConversation = conversationService.GetConversation((int)id);
            }
            else
            {

                if (currentConversation == null && Conversations.Count > 0)
                {
                    currentConversation = Conversations[0];
                    currentConversation.Messages = GetMessages(currentConversation.Id);
                }
            }
            return Page();
        }

        public List<Message> GetMessages(int id)
        {
            return context.Messages.Where(c => c.ConversationId == id).ToList();
        }

        public void GetConversationById(int id)
        {
            currentConversation = conversationService.GetConversation(id);
            currentConversation.Messages = GetMessages(currentConversation.Id);

        }
    }
}
