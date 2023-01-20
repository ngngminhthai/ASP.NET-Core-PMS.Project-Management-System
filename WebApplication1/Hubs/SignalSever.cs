using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;

namespace WebApplication1.Hubs
{
    public class SignalSever : Hub
    {

        private static readonly Dictionary<string, List<string>> _groups = new Dictionary<string, List<string>>();

        private readonly ManageAppDbContext _context;

        public SignalSever(ManageAppDbContext context)
        {
            _context = context;
        }
        public override async Task OnConnectedAsync()
        {


            //string email = "emlasieunhan118@gmail.com";
            var user = _context.Users.Where(u => u.UserName == Context.User.Identity.Name).FirstOrDefault();
            if (user != null)
            {
                var conversation = _context.ConversationUsers.Where(c => c.UserId == user.Id).ToList().FirstOrDefault();
                System.Console.WriteLine(conversation);


                if (!_groups.ContainsKey("g" + conversation.ConversationId))
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, "g" + conversation.ConversationId);
                    _groups.Add("g" + conversation.ConversationId, new List<string>
                {
                    Context.ConnectionId
                });

                }
                else
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, "g" + conversation.ConversationId);
                    _groups["g" + conversation.ConversationId].Add(Context.ConnectionId);
                }


                /*  if (MyEmail != null)
                  {

                      string connectionId = Context.ConnectionId;

                      if (!_users.ContainsKey(email))
                          _users.Add(email, connectionId);


                  }*/

                await base.OnConnectedAsync();
            }
        }


        public async Task SendMsg(string ConversationName)
        {
            // Get the connection ID for the target user
            /* string connectionId = _users.GetValueOrDefault("emlasieunhan118@gmail.com");
             System.Console.WriteLine(Context.User.Identity.Name);*/
            // Send the message to the target user
            var user = _context.Users.Where(u => u.UserName == Context.User.Identity.Name).FirstOrDefault();
            var conversation = _context.ConversationUsers.Where(c => c.UserId == user.Id).ToList().FirstOrDefault();

            await Clients.Group("g" + conversation.ConversationId).SendAsync("ReceiveMessage", "Chao Moi Nguoi Trong Group");

        }
    }
}
