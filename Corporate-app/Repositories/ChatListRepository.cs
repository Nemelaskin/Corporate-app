using System.Linq;
using System.Threading.Tasks;
using Corporate_app.Models;
using Corporate_app.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Corporate_app.Repositories
{
    public class ChatListRepository
    {
        private readonly ModelsContext context;
        public ChatListRepository(ModelsContext context)
        {
            this.context = context;
        }

        public IQueryable<ChatList> GetChatLists()
        {
            return context.ChatLists.OrderBy(x => x.ChatListId);
        }

        async public Task<ChatList> GetChatList(int id)
        {
            return await context.ChatLists.FirstOrDefaultAsync(u => u.ChatListId == id);
        }

        async public void SaveChatList(ChatList entity)
        {
            if (entity.ChatListId == default)
                await context.ChatLists.AddAsync(entity);
            else
                context.ChatLists.Update(entity);
            await context.SaveChangesAsync();
        }

        async public void DeleteChatList(ChatList entity)
        {
            context.ChatLists.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
