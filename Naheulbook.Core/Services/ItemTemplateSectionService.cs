using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Naheulbook.Core.Models;
using Naheulbook.Core.Utils;
using Naheulbook.Data.Factories;
using Naheulbook.Data.Models;
using Naheulbook.Requests.Requests;

namespace Naheulbook.Core.Services
{
    public interface IItemTemplateSectionService
    {
        Task<IList<ItemTemplateSection>> GetAllSectionsAsync();
        Task<ItemTemplateSection> CreateItemTemplateSectionAsync(NaheulbookExecutionContext executionContext, CreateItemTemplateSectionRequest request);
        Task<List<ItemTemplate>> GetItemTemplatesBySectionAsync(int sectionId, int? currentUserId);
    }

    public class ItemTemplateSectionService : IItemTemplateSectionService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IAuthorizationUtil _authorizationUtil;
        private readonly IItemTemplateUtil _itemTemplateUtil;

        public ItemTemplateSectionService(
            IUnitOfWorkFactory unitOfWorkFactory,
            IAuthorizationUtil authorizationUtil,
            IItemTemplateUtil itemTemplateUtil
        )
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _authorizationUtil = authorizationUtil;
            _itemTemplateUtil = itemTemplateUtil;
        }

        public async Task<IList<ItemTemplateSection>> GetAllSectionsAsync()
        {
            using (var uow = _unitOfWorkFactory.CreateUnitOfWork())
            {
                return await uow.ItemTemplateSections.GetAllWithCategoriesAsync();
            }
        }

        public async Task<ItemTemplateSection> CreateItemTemplateSectionAsync(NaheulbookExecutionContext executionContext, CreateItemTemplateSectionRequest request)
        {
            await _authorizationUtil.EnsureAdminAccessAsync(executionContext);

            var itemTemplateSection = new ItemTemplateSection
            {
                Name = request.Name,
                Special = string.Join(",", request.Specials),
                Note = request.Note,
                Icon = request.Icon,
                SubCategories = new List<ItemTemplateSubCategory>()
            };

            using (var uow = _unitOfWorkFactory.CreateUnitOfWork())
            {
                uow.ItemTemplateSections.Add(itemTemplateSection);
                await uow.SaveChangesAsync();
            }

            return itemTemplateSection;
        }

        public async Task<List<ItemTemplate>> GetItemTemplatesBySectionAsync(int sectionId, int? currentUserId)
        {
            using (var uow = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var itemTemplates = await uow.ItemTemplates.GetWithModifiersWithRequirementsWithSkillsWithSkillModifiersWithSlotsWithUnSkillsBySectionIdAsync(sectionId);
                return _itemTemplateUtil.FilterItemTemplatesBySource(itemTemplates, currentUserId, true).ToList();
            }
        }
    }
}