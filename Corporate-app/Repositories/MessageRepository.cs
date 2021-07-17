using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corporate_app.Models;
using Corporate_app.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Corporate_app.Repositories
{
    public class MessageRepository
    {
        private readonly ModelsContext context;
        public MessageRepository(ModelsContext context)
        {
            this.context = context;
        }

        public IQueryable<MessageList> GetMessages()
        {
            return context.MessageLists.OrderBy(x => x.ChatListId);
        }

        async public Task<MessageList> GetMessage(int id)
        {
            return await context.MessageLists.FirstOrDefaultAsync(u => u.MessageId == id);
        }


        async public void SaveMessage(MessageList entity)
        {
            if (entity.MessageId == default)
                await context.MessageLists.AddAsync(entity);
            else
                context.MessageLists.Update(entity);
            context.SaveChanges();
        }

        async public void SaveMessage(string nameChat, string userId, string content)
        {
            MessageList messageList = new MessageList();
            var chat = context.ChatLists.FirstOrDefault(u => u.Name == nameChat);
            var user = context.Users.FirstOrDefault(u => u.UserId == Convert.ToInt32(userId));
            messageList.ChatList = chat;
            messageList.User = user;
            messageList.Сontent = content;
            messageList.DateCreate = DateTime.Now;

            await context.MessageLists.AddAsync(messageList);
            context.SaveChanges();
        }

        public void DeleteMessage(MessageList entity)
        {
            context.MessageLists.Remove(entity);
            context.SaveChanges();
        }
    }
}
