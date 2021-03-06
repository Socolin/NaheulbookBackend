using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Naheulbook.Core.Exceptions;
using Naheulbook.Core.Models;
using Naheulbook.Core.Utils;
using Naheulbook.Data.Factories;
using Naheulbook.Data.Models;
using Naheulbook.Requests.Requests;

namespace Naheulbook.Core.Services
{
    public interface IEffectService
    {
        Task<Effect> GetEffectAsync(int effectId);
        Task<ICollection<EffectType>> GetEffectSubCategoriesAsync();
        Task<ICollection<Effect>> GetEffectsBySubCategoryAsync(long subCategoryId);
        Task<EffectType> CreateEffectTypeAsync(NaheulbookExecutionContext executionContext, CreateEffectTypeRequest request);
        Task<EffectSubCategory> CreateEffectSubCategoryAsync(NaheulbookExecutionContext executionContext, CreateEffectSubCategoryRequest request);
        Task<Effect> CreateEffectAsync(NaheulbookExecutionContext executionContext, int subCategoryId, CreateEffectRequest request);
        Task<Effect> EditEffectAsync(NaheulbookExecutionContext executionContext, int effectId, EditEffectRequest request);
        Task<List<Effect>> SearchEffectsAsync(string filter);
    }

    public class EffectService : IEffectService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IAuthorizationUtil _authorizationUtil;

        public EffectService(IUnitOfWorkFactory unitOfWorkFactory, IAuthorizationUtil authorizationUtil)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _authorizationUtil = authorizationUtil;
        }

        public async Task<Effect> GetEffectAsync(int effectId)
        {
            using (var uow = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var effect = await uow.Effects.GetWithModifiersAsync(effectId);
                if (effect == null)
                    throw new EffectNotFoundException();

                return effect;
            }
        }

        public async Task<ICollection<EffectType>> GetEffectSubCategoriesAsync()
        {
            using (var uow = _unitOfWorkFactory.CreateUnitOfWork())
            {
                return await uow.Effects.GetCategoriesAsync();
            }
        }

        public async Task<ICollection<Effect>> GetEffectsBySubCategoryAsync(long subCategoryId)
        {
            using (var uow = _unitOfWorkFactory.CreateUnitOfWork())
            {
                return await uow.Effects.GetBySubCategoryWithModifiersAsync(subCategoryId);
            }
        }

        public async Task<EffectType> CreateEffectTypeAsync(NaheulbookExecutionContext executionContext, CreateEffectTypeRequest request)
        {
            await _authorizationUtil.EnsureAdminAccessAsync(executionContext);

            var effectType = new EffectType
            {
                Name = request.Name
            };

            using (var uow = _unitOfWorkFactory.CreateUnitOfWork())
            {
                uow.EffectTypes.Add(effectType);
                await uow.SaveChangesAsync();
            }

            return effectType;
        }

        public async Task<EffectSubCategory> CreateEffectSubCategoryAsync(NaheulbookExecutionContext executionContext, CreateEffectSubCategoryRequest request)
        {
            await _authorizationUtil.EnsureAdminAccessAsync(executionContext);

            var effectSubCategory = new EffectSubCategory
            {
                Name = request.Name,
                TypeId = request.TypeId,
                DiceSize = request.DiceSize,
                DiceCount = request.DiceCount,
                Note = request.Note,
                Effects = new List<Effect>()
            };

            using (var uow = _unitOfWorkFactory.CreateUnitOfWork())
            {
                uow.EffectSubCategories.Add(effectSubCategory);
                await uow.SaveChangesAsync();
            }

            return effectSubCategory;
        }

        public async Task<Effect> CreateEffectAsync(NaheulbookExecutionContext executionContext, int subCategoryId, CreateEffectRequest request)
        {
            await _authorizationUtil.EnsureAdminAccessAsync(executionContext);

            var effect = new Effect
            {
                Name = request.Name,
                SubCategoryId = subCategoryId,
                Description = request.Description,
                Dice = request.Dice,
                TimeDuration = request.TimeDuration,
                CombatCount = request.CombatCount,
                Duration = request.Duration,
                LapCount = request.LapCount,
                DurationType = request.DurationType,
                Modifiers = request.Modifiers.Select(s => new EffectModifier
                {
                    StatName = s.Stat, Type = s.Type, Value = s.Value
                }).ToList()
            };

            using (var uow = _unitOfWorkFactory.CreateUnitOfWork())
            {
                uow.Effects.Add(effect);
                await uow.SaveChangesAsync();
            }

            return effect;
        }

        public async Task<Effect> EditEffectAsync(NaheulbookExecutionContext executionContext, int effectId, EditEffectRequest request)
        {
            await _authorizationUtil.EnsureAdminAccessAsync(executionContext);

            using (var uow = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var effect = await uow.Effects.GetWithModifiersAsync(effectId);
                if (effect == null)
                    throw new EffectNotFoundException();

                effect.Name = request.Name;
                effect.SubCategoryId = request.SubCategoryId;
                effect.Description = request.Description;
                effect.Dice = request.Dice;
                effect.TimeDuration = request.TimeDuration;
                effect.CombatCount = request.CombatCount;
                effect.Duration = request.Duration;
                effect.LapCount = request.LapCount;
                effect.DurationType = request.DurationType;
                effect.Modifiers = request.Modifiers.Select(s => new EffectModifier
                {
                    StatName = s.Stat, Type = s.Type, Value = s.Value
                }).ToList();

                await uow.SaveChangesAsync();

                return effect;
            }
        }

        public async Task<List<Effect>> SearchEffectsAsync(string filter)
        {
            if (string.IsNullOrEmpty(filter))
                return new List<Effect>();
            using (var uow = _unitOfWorkFactory.CreateUnitOfWork())
            {
                return await uow.Effects.SearchByNameAsync(filter, 10);
            }
        }
    }
}