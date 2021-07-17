using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Corporate_app.Models.Context;
using Corporate_app.Repositories;

namespace Corporate_app.Hubs
{
    public class MessageHub : Hub
    {

        async public override Task<Task> OnConnectedAsync()
        {
            var name = Context.GetHttpContext().Request.Query["chatName"].ToString();
            await Groups.AddToGroupAsync(Context.ConnectionId, name);
            return base.OnConnectedAsync();
        }

        [Authorize]
        public async Task SendMessageForOne(string message)
        {
            var user = Context.User;
            var userName = user.Identity.Name;
            var userIdentifier = Context.UserIdentifier;
            Console.WriteLine("id:" + userIdentifier);
            string textMessage = userName + " said: " + message;
            Console.WriteLine(textMessage);
            await Clients.All.SendAsync("SendMessageForOneTest", textMessage);
        }

        [Authorize]
        public async Task SendMessageForGroup(string message, string chatName)
        {
            var user = Context.User;
            var userName = user.Identity.Name;
            string textMessage = userName + " said: " + message;
            //repositoryMessage.SaveMessage(chat, userName.Split(' ')[0], textMessage);
            await Clients.Groups(chatName).SendAsync("SendMessageForGroupTest", textMessage);
        }
        public string test()
        {
            return "test";
        }
    }
}
