using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corporate_app.Models;
using Corporate_app.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Corporate_app.Repositories
{
    public class PositionRepository
    {
        private readonly ModelsContext context;
        public PositionRepository(ModelsContext context)
        {
            this.context = context;
        }

        public IQueryable<Position> GetPositions()
        {
            return context.Positions.OrderBy(x => x.PositionId);
        }

        async public Task<Position> GetPosition(int id)
        {
            return await context.Positions.FirstOrDefaultAsync(u => u.PositionId == id);
        }

        async public void SavePosition(Position entity)
        {
            if (entity.PositionId == default)
                await context.Positions.AddAsync(entity);
            else
                context.Positions.Update(entity);
            await context.SaveChangesAsync();
        }

        async public void DeletePosition(Position entity)
        {
            context.Positions.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
