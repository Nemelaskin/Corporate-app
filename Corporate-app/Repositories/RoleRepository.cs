using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corporate_app.Models;
using Corporate_app.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Corporate_app.Repositories
{
    public class RoleRepository
    {
        private readonly ModelsContext context;
        public RoleRepository(ModelsContext context)
        {
            this.context = context;
        }

        public IQueryable<Role> GetRoles()
        {
            return context.Roles.OrderBy(x => x.RoleId);
        }

        async public Task<Role> GetRole(int id)
        {
            return await context.Roles.FirstOrDefaultAsync(u => u.RoleId == id);
        }

        async public void SaveRole(Role entity)
        {
            if (entity.RoleId == default)
                await context.Roles.AddAsync(entity);
            else
                context.Roles.Update(entity);
            await context.SaveChangesAsync();
        }

        async public void DeleteRole(Role entity)
        {
            context.Roles.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
