using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Corporate_app.Models;
using Corporate_app.Models.Context;
using Corporate_app.Repositories;
using Newtonsoft.Json;

namespace Corporate_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageListsController : ControllerBase
    {
        private readonly ModelsContext context;
        private readonly MessageRepository repository;
        public MessageListsController(ModelsContext context, MessageRepository repository)
        {
            this.context = context;
            this.repository = repository;
        }

        [HttpGet]
        [Route("GetChatMessageLists")]
        public async Task<string> GetChatMessageLists(string nameChat)
        {
            var listChat = await context.ChatLists.FirstOrDefaultAsync(u => u.Name == nameChat);
            var listMessage = await repository.GetMessages().ToListAsync();
            var messageHistory = listMessage.FindAll(u => u.ChatListId == listChat.ChatListId);
            foreach (var list in messageHistory) {
                list.ChatList = null;
                list.User = null;
            }
            string JSONResponse = JsonConvert.SerializeObject(messageHistory);

            return JSONResponse;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageList>>> GetMessageLists()
        {
            return await repository.GetMessages().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MessageList>> GetMessageList(int id)
        {
            var messageList = await repository.GetMessage(id);

            if (messageList == null) {
                return NotFound();
            }

            return messageList;
        }

        [HttpPost]
        [Route("PostMessageList")]
        public IActionResult PostMessageList(CreateMessageRequestModel messageModel)
        {
            var user = User.Identity.Name.Split(" ");
            try {
                repository.SaveMessage(messageModel.nameChat, user[0].ToString(), user[1] + " " + user[2] + ": " + messageModel.content);
                return Ok();
            }
            catch {
                return NotFound();
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessageList(int id)
        {
            MessageList message = await repository.GetMessage(id);
            repository.DeleteMessage(message);
            return NoContent();
        }
    }
}
