using Microsoft.AspNetCore.SignalR;
using System;
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
        /*        public override async Task OnConnectedAsync()
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


                        *//*  if (MyEmail != null)
                          {

                              string connectionId = Context.ConnectionId;

                              if (!_users.ContainsKey(email))
                                  _users.Add(email, connectionId);


                          }*//*

                        await base.OnConnectedAsync();

                    }
                }
        */
        public override async Task OnConnectedAsync()
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == Context.User.Identity.Name);
                if (user != null)
                {
                    var conversations = _context.ConversationUsers.Where(c => c.UserId == user.Id).Select(c => c.ConversationId).ToList();

                    foreach (var conversationId in conversations)
                    {
                        var groupId = "g" + conversationId;
                        if (!_groups.ContainsKey(groupId))
                        {
                            _groups.Add(groupId, new List<string>());
                        }

                        _groups[groupId].Add(Context.ConnectionId);
                        await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
                    }

                    await base.OnConnectedAsync();
                }
            }
            catch (Exception e)
            {

            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == Context.User.Identity.Name);
            if (user != null)
            {
                var conversations = _context.ConversationUsers.Where(c => c.UserId == user.Id).Select(c => c.ConversationId).ToList();

                foreach (var conversationId in conversations)
                {
                    var groupId = "g" + conversationId;
                    if (_groups.ContainsKey(groupId))
                    {
                        _groups[groupId].Remove(Context.ConnectionId);
                        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
                    }
                }
            }

            await base.OnDisconnectedAsync(exception);
        }



        public async Task SendMsg(string ConversationName, string content)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == Context.User.Identity.Name);
            if (user != null)
            {
                /*var conversations = _context.ConversationUsers.Where(c => c.UserId == user.Id).Select(c => c.ConversationId).ToList();

                foreach (var conversationId in conversations)
                {
                    var groupId = "g" + conversationId;*/
                await Clients.Group(ConversationName).SendAsync("ReceiveMessage", content);
            }//


        }
    }

}

