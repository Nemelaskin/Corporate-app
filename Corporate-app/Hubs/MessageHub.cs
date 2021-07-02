using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Corporate_app.Hubs
{
    public class MessageHub : Hub
    {
        [Authorize]
        public async Task SendMessageForOne(string message)
        {
            var user = Context.User;
            var userName = user.Identity.Name;
            var userIdentifier = Context.UserIdentifier;
            Console.WriteLine("id:"+ userIdentifier);
            string textMessage = userName + " said: " + message;
            Console.WriteLine(textMessage);
            await Clients.All.SendAsync("SendMessageForOneTest", textMessage);

        }

    }
}
