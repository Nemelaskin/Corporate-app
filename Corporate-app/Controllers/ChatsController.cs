using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corporate_app.Models;
using Corporate_app.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Corporate_app.Repositories;
using Newtonsoft.Json;

namespace Corporate_app.Controllers
{
    [Authorize]
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
            await ChatsBody();
            return View();
        }

        public IActionResult TestingChat()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Chat(string actualChat)
        {
            await ChatsBody();
            ViewBag.Chat = actualChat;
            return View();
        }

        private async Task<IActionResult> ChatsBody()
        {
            var userId = Convert.ToInt32(User.Identity.Name.Split(" ")[0]);
            var chatLists = await repositoryChat.GetChatLists();
            var userlist = repositoryUser.GetUsers();
            var actualUser = await repositoryUser.GetUser(userId);
            var listUsers = from user in userlist where user.UserId != userId select user;
            var ChatListUsers = chatLists.FindAll(u => u.User.FirstOrDefault(y => y.UserId == userId) != null);
            foreach (var list in ChatListUsers) {
                list.User = null;
            }
            foreach (var list in listUsers) {
                list.ChatList = null;
            }
            string sJSONResponse = JsonConvert.SerializeObject(listUsers);

            ViewBag.ArrayUsers = sJSONResponse;
            ViewBag.List = ChatListUsers;
            ViewBag.ActualUser = actualUser;
            return Ok();
        }

    }
}
