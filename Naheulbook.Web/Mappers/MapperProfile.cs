using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Naheulbook.Data.Models;
using Naheulbook.Shared.TransientModels;
using Naheulbook.Web.Responses;
using Newtonsoft.Json.Linq;

namespace Naheulbook.Web.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Character, CreateCharacterResponse>();
            CreateMap<Character, CharacterSummaryResponse>()
                .ForMember(x => x.JobNames, opt => opt.MapFrom(c => c.Jobs.Select(x => x.Job.Name)))
                .ForMember(x => x.OriginName, opt => opt.MapFrom(c => c.Origin.Name));
            CreateMap<Character, CharacterResponse>()
                .ForMember(x => x.Stats, opt => opt.MapFrom(c => new CharacterResponse.BasicStats {Ad = c.Ad, Cou = c.Cou, Cha = c.Cha, Fo = c.Fo, Int = c.Int}))
                .ForMember(x => x.Items, opt => opt.Ignore())
                .ForMember(x => x.Invites, opt => opt.Ignore())
                .ForMember(x => x.SkillIds, opt => opt.MapFrom(c => c.Skills.Select(x => x.SkillId)))
                .ForMember(x => x.JobIds, opt => opt.MapFrom(c => c.Jobs.Select(x => x.JobId)))
                .ForMember(x => x.Specialities, opt => opt.MapFrom(c => c.Specialities.Select(x => x.Speciality)));
            CreateMap<CharacterModifier, ActiveStatsModifier>()
                .ForMember(x => x.Active, opt => opt.MapFrom(x => x.IsActive));
            CreateMap<CharacterModifierValue, StatModifier>()
                .ForMember(x => x.Stat, opt => opt.MapFrom(c => c.StatName))
                .ForMember(x => x.Special, opt => opt.Ignore());

            CreateMap<Effect, EffectResponse>();
            CreateMap<EffectType, EffectTypeResponse>();
            CreateMap<EffectCategory, EffectCategoryResponse>();
            CreateMap<EffectModifier, StatModifierResponse>()
                .ForMember(m => m.Stat, opt => opt.MapFrom(se => se.StatName))
                .ForMember(m => m.Special, opt => opt.Ignore());

            CreateMap<God, GodResponse>();

            CreateMap<Group, GroupResponse>()
                .ForMember(m => m.Data, opt => opt.MapFrom(im => MapperHelpers.FromJson<JObject>(im.Data)))
                .ForMember(m => m.Invites, opt => opt.Ignore())
                .ForMember(m => m.Invited, opt => opt.Ignore())
                .ForMember(m => m.Characters, opt => opt.Ignore());
            CreateMap<Group, NamedIdResponse>();
            CreateMap<Group, GroupSummaryResponse>()
                .ForMember(m => m.CharacterCount, opt => opt.MapFrom(g => g.Characters.Count));

            CreateMap<Item, ItemResponse>()
                .ForMember(m => m.Data, opt => opt.MapFrom(x => MapperHelpers.FromJson<JObject>(x.Data)));

            CreateMap<ItemTemplate, ItemTemplateResponse>()
                .ForMember(m => m.Data, opt => opt.MapFrom(i => MapperHelpers.FromJson<JObject>(i.Data)))
                .ForMember(m => m.Slots, opt => opt.MapFrom(i => i.Slots.Select(x => x.Slot)));
            CreateMap<ItemTemplateModifier, ItemTemplateModifierResponse>()
                .ForMember(m => m.JobId, opt => opt.MapFrom(im => im.RequireJobId))
                .ForMember(m => m.OriginId, opt => opt.MapFrom(im => im.RequireOriginId))
                .ForMember(m => m.Special, opt => opt.MapFrom(im => MapperHelpers.FromCommaSeparatedList(im.Special)))
                .ForMember(m => m.Stat, opt => opt.MapFrom(im => im.StatName));
            CreateMap<ItemTemplateRequirement, ItemTemplateRequirementResponse>()
                .ForMember(m => m.Stat, opt => opt.MapFrom(ir => ir.StatName))
                .ForMember(m => m.Min, opt => opt.MapFrom(ir => ir.MinValue))
                .ForMember(m => m.Max, opt => opt.MapFrom(ir => ir.MaxValue));
            CreateMap<ItemTemplateSkillModifier, ItemTemplateSkillModifierResponse>();
            CreateMap<ItemTemplateSlot, IdResponse>()
                .ForMember(m => m.Id, opt => opt.MapFrom(i => i.SlotId));
            CreateMap<ItemTemplateSkill, IdResponse>()
                .ForMember(m => m.Id, opt => opt.MapFrom(i => i.SkillId));
            CreateMap<ItemTemplateUnSkill, IdResponse>()
                .ForMember(m => m.Id, opt => opt.MapFrom(i => i.SkillId));
            CreateMap<ItemTemplateSection, ItemTemplateSectionResponse>()
                .ForMember(m => m.Specials, opt => opt.MapFrom(i => MapperHelpers.FromCommaSeparatedList(i.Special)));
            CreateMap<ItemTemplateCategory, ItemTemplateCategoryResponse>();
            CreateMap<Slot, ItemSlotResponse>();

            CreateMap<Job, JobResponse>()
                .ForMember(m => m.Requirements, opt => opt.MapFrom(j => j.Requirements.OrderBy(r => r.Id)))
                .ForMember(m => m.OriginsWhitelist, opt => opt.MapFrom(j => j.OriginWhitelist.OrderBy(w => w.Id)))
                .ForMember(m => m.OriginsBlacklist, opt => opt.MapFrom(j => j.OriginBlacklist.OrderBy(b => b.Id)))
                .ForMember(m => m.Flags, opt => opt.MapFrom(s => MapperHelpers.FromJson<List<FlagResponse>>(s.Flags)))
                .ForMember(m => m.AvailableSkillIds, opt => opt.MapFrom(j => j.Skills.Where(s => !s.Default).OrderBy(s => s.SkillId).Select(s => s.SkillId)))
                .ForMember(m => m.SkillIds, opt => opt.MapFrom(j => j.Skills.Where(s => s.Default).OrderBy(s => s.SkillId).Select(s => s.SkillId)));
            CreateMap<JobBonus, DescribedFlagResponse>()
                .ForMember(m => m.Flags, opt => opt.MapFrom(b => MapperHelpers.FromJson<List<FlagResponse>>(b.Flags)));
            CreateMap<JobOriginBlacklist, NamedIdResponse>()
                .ForMember(m => m.Id, opt => opt.MapFrom(b => b.Origin.Id))
                .ForMember(m => m.Name, opt => opt.MapFrom(b => b.Origin.Name));
            CreateMap<JobOriginWhitelist, NamedIdResponse>()
                .ForMember(m => m.Id, opt => opt.MapFrom(w => w.Origin.Id))
                .ForMember(m => m.Name, opt => opt.MapFrom(w => w.Origin.Name));
            CreateMap<JobRequirement, StatRequirementResponse>()
                .ForMember(m => m.Stat, opt => opt.MapFrom(r => r.StatName))
                .ForMember(m => m.Min, opt => opt.MapFrom(r => r.MinValue))
                .ForMember(m => m.Max, opt => opt.MapFrom(r => r.MaxValue));
            CreateMap<JobRestrict, DescribedFlagResponse>()
                .ForMember(m => m.Description, opt => opt.MapFrom(r => r.Text))
                .ForMember(m => m.Flags, opt => opt.MapFrom(s => MapperHelpers.FromJson<List<FlagResponse>>(s.Flags)));

            CreateMap<Location, LocationResponse>()
                .ForMember(m => m.Children, opt => opt.Ignore())
                .ForMember(m => m.Data, opt => opt.MapFrom(im => MapperHelpers.FromJson<JObject>(im.Data)));

            CreateMap<Loot, LootResponse>()
                .ForMember(m => m.Items, opt => opt.Ignore())
                .ForMember(m => m.Monsters, opt => opt.Ignore());

            CreateMap<Origin, OriginResponse>()
                .ForMember(m => m.Requirements, opt => opt.MapFrom(o => o.Requirements.OrderBy(r => r.Id)))
                .ForMember(m => m.Flags, opt => opt.MapFrom(o => MapperHelpers.FromJson<List<FlagResponse>>(o.Flags)))
                .ForMember(m => m.AvailableSkillIds, opt => opt.MapFrom(o => o.Skills.Where(s => !s.Default).OrderBy(s => s.SkillId).Select(s => s.SkillId)))
                .ForMember(m => m.SkillIds, opt => opt.MapFrom(o => o.Skills.Where(s => s.Default).OrderBy(s => s.SkillId).Select(s => s.SkillId)));
            CreateMap<OriginInfo, OriginInformationResponse>();
            CreateMap<OriginRequirement, OriginRequirementResponse>()
                .ForMember(m => m.Stat, opt => opt.MapFrom(r => r.StatName))
                .ForMember(m => m.Min, opt => opt.MapFrom(r => r.MinValue))
                .ForMember(m => m.Max, opt => opt.MapFrom(r => r.MaxValue));
            CreateMap<OriginBonus, DescribedFlagResponse>()
                .ForMember(m => m.Flags, opt => opt.MapFrom(b => MapperHelpers.FromJson<List<FlagResponse>>(b.Flags)));
            CreateMap<OriginRestrict, DescribedFlagResponse>()
                .ForMember(m => m.Description, opt => opt.MapFrom(r => r.Text))
                .ForMember(m => m.Flags, opt => opt.MapFrom(r => MapperHelpers.FromJson<List<FlagResponse>>(r.Flags)));

            CreateMap<Monster, MonsterResponse>()
                .ForMember(m => m.Data, opt => opt.MapFrom(b => MapperHelpers.FromJson<JObject>(b.Data)))
                .ForMember(m => m.Modifiers, opt => opt.MapFrom(b => MapperHelpers.FromJson<IList<ActiveStatsModifier>>(b.Modifiers)))
                .ForMember(m => m.Items, opt => opt.Ignore());

            CreateMap<MonsterTemplate, MonsterTemplateResponse>()
                .ForMember(x => x.SimpleInventory, opt => opt.MapFrom(m => m.Items))
                .ForMember(x => x.LocationIds, opt => opt.MapFrom(m => m.Locations.Select(x => x.LocationId)))
                .ForMember(m => m.Data, opt => opt.MapFrom(b => MapperHelpers.FromJson<JObject>(b.Data)));
            CreateMap<MonsterTemplateSimpleInventory, MonsterTemplateResponse.MonsterSimpleInventoryResponse>();
            CreateMap<MonsterType, MonsterTypeResponse>()
                .ForMember(m => m.Categories, opt => opt.MapFrom(c => c.Categories.OrderBy(ca => ca.Id)));
            CreateMap<MonsterCategory, MonsterCategoryResponse>();
            CreateMap<MonsterTrait, MonsterTraitResponse>()
                .ForMember(m => m.Levels, opt => opt.MapFrom(b => MapperHelpers.FromJson<List<string>>(b.Levels)));

            CreateMap<SkillEffect, SkillEffectResponse>()
                .ForMember(m => m.Type, opt => opt.MapFrom(x => "ADD"))
                .ForMember(m => m.Stat, opt => opt.MapFrom(s => s.StatName));
            CreateMap<Skill, SkillResponse>()
                .ForMember(m => m.Flags, opt => opt.MapFrom(s => MapperHelpers.FromJson<List<FlagResponse>>(s.Flags)))
                .ForMember(m => m.Effects, opt => opt.MapFrom(s => s.SkillEffects))
                .ForMember(m => m.Stat, opt => opt.MapFrom(s => MapperHelpers.FromCommaSeparatedStringArray(s.Stat)));

            CreateMap<Speciality, SpecialityResponse>()
                .ForMember(m => m.Modifiers, opt => opt.MapFrom(s => s.Modifiers.OrderBy(m => m.Id)))
                .ForMember(m => m.Flags, opt => opt.MapFrom(s => MapperHelpers.FromJson<List<FlagResponse>>(s.Flags)))
                .ForMember(m => m.Specials, opt => opt.MapFrom(s => s.Specials.OrderBy(p => p.Id)));
            CreateMap<SpecialitySpecial, SpecialitySpecialResponse>()
                .ForMember(m => m.Flags, opt => opt.MapFrom(s => MapperHelpers.FromJson<List<FlagResponse>>(s.Flags)));
            CreateMap<SpecialityModifier, StatModifierResponse>()
                .ForMember(m => m.Type, opt => opt.MapFrom(x => "ADD"))
                .ForMember(m => m.Special, opt => opt.Ignore());

            CreateMap<string, LapCountDecrement>().ConvertUsing(c => c == null ? null : MapperHelpers.FromJson<LapCountDecrement>(c));

            CreateMap<User, UserInfoResponse>()
                .ForMember(m => m.LinkedWithFb, opt => opt.MapFrom(u => u.FbId != null))
                .ForMember(m => m.LinkedWithTwitter, opt => opt.MapFrom(u => u.TwitterId != null))
                .ForMember(m => m.LinkedWithGoogle, opt => opt.MapFrom(u => u.TwitterId != null))
                .ForMember(m => m.LinkedWithMicrosoft, opt => opt.MapFrom(u => u.MicrosoftId != null))
                ;
        }
    }
}