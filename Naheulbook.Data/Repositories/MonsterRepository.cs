using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Naheulbook.Data.DbContexts;
using Naheulbook.Data.Models;

namespace Naheulbook.Data.Repositories
{
    public interface IMonsterRepository : IRepository<Monster>
    {
        Task<List<Monster>> GetByGroupIdWithInventoryAsync(int groupId);
        Task<List<Monster>> GetDeadMonstersByGroupIdAsync(int groupId, int startIndex, int count);
    }

    public class MonsterRepository : Repository<Monster, NaheulbookDbContext>, IMonsterRepository
    {
        public MonsterRepository(NaheulbookDbContext context)
            : base(context)
        {
        }

        public Task<List<Monster>> GetByGroupIdWithInventoryAsync(int groupId)
        {
            return Context.Monsters
                .Include(m => m.Items)
                .ThenInclude(i => i.ItemTemplate)
                .ThenInclude(i => i.UnSkills)
                .Include(m => m.Items)
                .ThenInclude(i => i.ItemTemplate)
                .ThenInclude(i => i.Skills)
                .Include(m => m.Items)
                .ThenInclude(i => i.ItemTemplate)
                .ThenInclude(i => i.Modifiers)
                .Include(m => m.Items)
                .ThenInclude(i => i.ItemTemplate)
                .ThenInclude(i => i.SkillModifiers)
                .Include(m => m.Items)
                .ThenInclude(i => i.ItemTemplate)
                .ThenInclude(i => i.Requirements)
                .Include(m => m.Items)
                .ThenInclude(i => i.ItemTemplate)
                .ThenInclude(i => i.Slots)
                .ThenInclude(i => i.Slot)
                .Include(m => m.Items)
                .ThenInclude(i => i.ItemTemplate)
                .ThenInclude(i => i.Modifiers)
                .Where(g => g.GroupId == groupId && g.Dead == null)
                .ToListAsync();
        }

        public Task<List<Monster>> GetDeadMonstersByGroupIdAsync(int groupId, int startIndex, int count)
        {
            return Context.Monsters
                .Where(g => g.GroupId == groupId && g.Dead != null)
                .Skip(startIndex)
                .Take(count)
                .ToListAsync();
        }
    }
}