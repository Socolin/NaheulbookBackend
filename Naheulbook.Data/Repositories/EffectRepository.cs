using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Naheulbook.Data.DbContexts;
using Naheulbook.Data.Models;

namespace Naheulbook.Data.Repositories
{
    public interface IEffectRepository : IRepository<Effect>
    {
        Task<ICollection<EffectType>> GetCategoriesAsync();
        Task<ICollection<Effect>> GetByCategoryWithModifiersAsync(long categoryId);
        Task<Effect> GetWithModifiersAsync(int effectId);
        Task<List<Effect>> SearchByNameAsync(string partialName, int maxResult);
    }

    public class EffectRepository : Repository<Effect, NaheulbookDbContext>, IEffectRepository
    {
        public EffectRepository(NaheulbookDbContext naheulbookDbContext)
            : base(naheulbookDbContext)
        {
        }

        public async Task<ICollection<EffectType>> GetCategoriesAsync()
        {
            return await Context.EffectTypes
                .Include(e => e.Categories)
                .ToListAsync();
        }

        public async Task<ICollection<Effect>> GetByCategoryWithModifiersAsync(long categoryId)
        {
            return await Context.Effects
                .Where(e => e.CategoryId == categoryId)
                .Include(e => e.Modifiers)
                .ToListAsync();
        }

        public Task<Effect> GetWithModifiersAsync(int effectId)
        {
            return Context.Effects
                .Include(e => e.Modifiers)
                .FirstAsync(e => e.Id == effectId);
        }

        public Task<List<Effect>> SearchByNameAsync(string partialName, int maxResult)
        {
            return Context.Effects
                .Include(e => e.Modifiers)
                .Where(e => e.Name.ToUpper().Contains(partialName.ToUpper()))
                .Take(maxResult)
                .ToListAsync();
        }
    }
}