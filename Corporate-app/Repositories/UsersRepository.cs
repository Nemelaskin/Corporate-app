using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corporate_app.Models.Context;
using Corporate_app.Models;
using Microsoft.EntityFrameworkCore;

namespace Corporate_app.Repositories
{
    public class UsersRepository
    {
        private readonly ModelsContext context;
        public UsersRepository(ModelsContext context)
        {
            this.context = context;
        }

        public IQueryable<User> GetUsers()
        {
            return context.Users.OrderBy(x => x.UserId);
        }

        async public Task<User> GetUser(int id)
        {
            return await context.Users.Include(u => u.Position).Include(u => u.Role).FirstOrDefaultAsync(u => u.UserId == id);
        }

        async public void SaveUser(User entity)
        {
            if (entity.UserId == default)
                await context.Users.AddAsync(entity);
            else
                 context.Users.Update(entity);
            await context.SaveChangesAsync();
        }

        async public void DeleteUser(User entity)
        {
            context.Users.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
