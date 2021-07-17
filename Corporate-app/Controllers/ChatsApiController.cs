using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Corporate_app.Repositories;
using Corporate_app.Models;
using Corporate_app.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Corporate_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsApiController : ControllerBase
    {
        private ChatListRepository repository;
        private ModelsContext context;
        public ChatsApiController(ChatListRepository repository, ModelsContext context)
        {
            this.repository = repository;
            this.context = context;
        }

        [Route("CheckChat")]
        [HttpPost]
        public async Task<IActionResult> CheckChat(CreateChatRequestModel requestModel)
        {
            if (requestModel.IdUser == 0) {
                var chat = await context.ChatLists.FirstOrDefaultAsync(u => u.Name == requestModel.NameChat);
                if (chat != null) {
                    return Ok(chat);
                }
                else {
                    return NotFound();
                }
            }
            else {
                var ActualUserId = Convert.ToInt32(User.Identity.Name.Split(" ")[0]);
                var user1 = await context.Users.FirstOrDefaultAsync(u => u.UserId == ActualUserId);
                var user2 = await context.Users.FirstOrDefaultAsync(u => u.UserId == requestModel.IdUser);
                var chat = await context.ChatLists.FirstOrDefaultAsync(u => u.Name == user1.Name + " " + user1.SurName + " --- " + user2.Name + " " + user2.SurName
                || u.Name == user2.Name + " " + user2.SurName + " --- " + user1.Name + " " + user1.SurName);
                if (chat != null) {
                    return Ok(chat);
                }
                else {
                    return NotFound();
                }
            }
            
        }

        [HttpPost]
        [Route("CreateChat")]
        public async Task<IActionResult> CreateChat(CreateChatRequestModel requestModel)
        {
            ChatList chatList = new ChatList();
            var ActualUserId = Convert.ToInt32(User.Identity.Name.Split(" ")[0]);
            var user1 = await context.Users.FirstOrDefaultAsync(u => u.UserId == ActualUserId);
            var user2 = await context.Users.FirstOrDefaultAsync(u => u.UserId == requestModel.IdUser);
            chatList.Name = user1.Name + " " + user1.SurName + " --- " + user2.Name + " " + user2.SurName;
            List<User> users = new List<User>();
            users.Add(user1);
            users.Add(user2);
            chatList.User = users;
            if (ModelState.IsValid) {
                repository.SaveChatList(chatList);

                return Ok();
            }
            return BadRequest();
        }
    }
}
