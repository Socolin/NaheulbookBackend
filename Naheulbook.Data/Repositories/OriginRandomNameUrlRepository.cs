#pragma warning disable 8619
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Naheulbook.Data.DbContexts;
using Naheulbook.Data.Models;

namespace Naheulbook.Data.Repositories
{
    public interface IOriginRandomNameUrlRepository : IRepository<OriginRandomNameUrl>
    {
        Task<OriginRandomNameUrl?> GetByOriginIdAndSexAsync(string sex, Guid originId);
    }

    public class OriginRandomNameUrlRepository : Repository<OriginRandomNameUrl, NaheulbookDbContext>, IOriginRandomNameUrlRepository
    {
        public OriginRandomNameUrlRepository(NaheulbookDbContext context) : base(context)
        {
        }

        public Task<OriginRandomNameUrl?> GetByOriginIdAndSexAsync(string sex, Guid originId)
        {
            return Context.OriginRandomNameUrls
                .SingleOrDefaultAsync(x => x.Sex == sex && x.OriginId == originId);
        }
    }
}