using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corporate_app.Models;
using Corporate_app.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Corporate_app.Repositories;

namespace Corporate_app.Controllers
{
    public class ChatsController : Controller
    {
        private ModelsContext context;
        private readonly ChatListRepository repositoryChat;
        private readonly UsersRepository repositoryUser;
        public ChatsController(ModelsContext context, ChatListRepository repositoryChat, UsersRepository repositoryUser)
        {
            this.context = context;
            this.repositoryChat = repositoryChat;
            this.repositoryUser = repositoryUser;
        }
        public async Task<IActionResult> AllChats()
        {
            var userId = Convert.ToInt32(User.Identity.Name.Split(" ")[0]);
            //var usr = await repositoryUser.GetUser(userId);
            //List<ChatList> chatLists = new List<ChatList>();
            var chatLists = repositoryChat.GetChatLists();
            //var d = chatLists.FirstOrDefault(u => u.User.FirstOrDefault(y => y.UserId == userId) != null);
            return View();
        }
        public IActionResult TestingChat()
        {
            return View();
        }
    }
}
