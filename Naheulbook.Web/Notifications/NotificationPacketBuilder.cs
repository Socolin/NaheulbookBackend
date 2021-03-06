using System;
using AutoMapper;
using Naheulbook.Core.Models;
using Naheulbook.Core.Notifications;
using Naheulbook.Data.Models;
using Naheulbook.Requests.Requests;
using Naheulbook.Shared.TransientModels;
using Naheulbook.Web.Responses;
using Naheulbook.Web.Services;

namespace Naheulbook.Web.Notifications
{
    public class NotificationPacketBuilder : INotificationPacketBuilder
    {
        private enum ElementType
        {
            Character,
            Group,
            Monster,
            Loot
        }

        private readonly IHubGroupUtil _hubGroupUtil;
        private readonly IMapper _mapper;

        public NotificationPacketBuilder(IMapper mapper, IHubGroupUtil hubGroupUtil)
        {
            _mapper = mapper;
            _hubGroupUtil = hubGroupUtil;
        }

        public INotificationPacket BuildCharacterChangeEv(Character character)
        {
            return BuildCharacterChange(character.Id, "update", new {Stat = "ev", Value = character.Ev});
        }

        public INotificationPacket BuildCharacterChangeEa(Character character)
        {
            return BuildCharacterChange(character.Id, "update", new {Stat = "ea", Value = character.Ea});
        }

        public INotificationPacket BuildCharacterChangeFatePoint(Character character)
        {
            return BuildCharacterChange(character.Id, "update", new {Stat = "fatePoint", Value = character.FatePoint});
        }

        public INotificationPacket BuildCharacterChangeExperience(Character character)
        {
            return BuildCharacterChange(character.Id, "update", new {Stat = "experience", Value = character.Experience});
        }

        public INotificationPacket BuildCharacterChangeSex(Character character)
        {
            return BuildCharacterChange(character.Id, "update", new {Stat = "sex", Value = character.Sex});
        }

        public INotificationPacket BuildCharacterChangeName(Character character)
        {
            return BuildCharacterChange(character.Id, "update", new {Stat = "name", Value = character.Name});
        }

        public INotificationPacket BuildCharacterAddItem(int characterId, Item item)
        {
            return BuildCharacterChange(characterId, "addItem", _mapper.Map<ItemResponse>(item));
        }

        public INotificationPacket BuildCharacterLevelUp(int characterId, LevelUpResult levelUpResult)
        {
            return BuildCharacterChange(characterId, "levelUp", _mapper.Map<CharacterLevelUpResponse>(levelUpResult));
        }

        public INotificationPacket BuildCharacterAddJob(int characterId, Guid jobId)
        {
            return BuildCharacterChange(characterId, "addJob", new CharacterAddJobResponse {JobId = jobId});
        }

        public INotificationPacket BuildCharacterRemoveJob(int characterId, Guid jobId)
        {
            return BuildCharacterChange(characterId, "removeJob", new CharacterRemoveJobResponse {JobId = jobId});
        }

        public INotificationPacket BuildCharacterChangeColor(Character character)
        {
            return BuildCharacterGmChange(character.Id, "changeColor", character.Color);
        }

        public INotificationPacket BuildCharacterChangeTarget(Character character, TargetRequest targetInfo)
        {
            return BuildCharacterGmChange(character.Id, "changeTarget", targetInfo);
        }

        public INotificationPacket BuildCharacterChangeData(Character character, CharacterGmData gmData)
        {
            return BuildCharacterGmChange(character.Id, "updateGmData", gmData);
        }

        public INotificationPacket BuildCharacterChangeActive(Character character)
        {
            return BuildCharacterGmChange(character.Id, "active", character.IsActive);
        }

        public INotificationPacket BuildItemDataChanged(Item item)
        {
            if (item.CharacterId != null)
                return BuildCharacterChange(item.CharacterId.Value, "updateItem", _mapper.Map<ItemPartialResponse>(item));
            if (item.MonsterId != null)
                return BuildMonsterChange(item.MonsterId.Value, "updateItem", _mapper.Map<ItemPartialResponse>(item));
            if (item.LootId != null)
                return BuildLootChange(item.LootId.Value, "updateItem", _mapper.Map<ItemPartialResponse>(item));

            throw new NotSupportedException();
        }

        public INotificationPacket BuildItemModifiersChanged(Item item)
        {
            if (item.CharacterId != null)
                return BuildCharacterChange(item.CharacterId.Value, "updateItemModifiers", _mapper.Map<ItemPartialResponse>(item));
            if (item.MonsterId != null)
                return BuildMonsterChange(item.MonsterId.Value, "updateItemModifiers", _mapper.Map<ItemPartialResponse>(item));
            if (item.LootId != null)
                return BuildLootChange(item.LootId.Value, "updateItemModifiers", _mapper.Map<ItemPartialResponse>(item));

            throw new NotSupportedException();
        }

        public INotificationPacket BuildEquipItem(Item item)
        {
            if (item.CharacterId != null)
                return BuildCharacterChange(item.CharacterId.Value, "equipItem", _mapper.Map<ItemPartialResponse>(item));
            if (item.MonsterId != null)
                return BuildMonsterChange(item.MonsterId.Value, "equipItem", _mapper.Map<ItemPartialResponse>(item));
            if (item.LootId != null)
                return BuildLootChange(item.LootId.Value, "equipItem", _mapper.Map<ItemPartialResponse>(item));

            throw new NotSupportedException();
        }

        public INotificationPacket BuildItemChangeContainer(Item item)
        {
            if (item.CharacterId != null)
                return BuildCharacterChange(item.CharacterId.Value, "changeContainer", _mapper.Map<ItemPartialResponse>(item));
            if (item.MonsterId != null)
                return BuildMonsterChange(item.MonsterId.Value, "changeContainer", _mapper.Map<ItemPartialResponse>(item));
            if (item.LootId != null)
                return BuildLootChange(item.LootId.Value, "changeContainer", _mapper.Map<ItemPartialResponse>(item));

            throw new NotSupportedException();
        }

        public INotificationPacket BuildItemUpdateModifier(Item item)
        {
            if (item.CharacterId != null)
                return BuildCharacterChange(item.CharacterId.Value, "updateItemModifiers", _mapper.Map<ItemPartialResponse>(item));
            if (item.MonsterId != null)
                return BuildMonsterChange(item.MonsterId.Value, "updateItemModifiers", _mapper.Map<ItemPartialResponse>(item));
            if (item.LootId != null)
                return BuildLootChange(item.LootId.Value, "updateItemModifiers", _mapper.Map<ItemPartialResponse>(item));

            throw new NotSupportedException();
        }

        public INotificationPacket BuildItemDeleteItem(Item item)
        {
            if (item.CharacterId != null)
                return BuildCharacterChange(item.CharacterId.Value, "deleteItem", item.Id);
            if (item.MonsterId != null)
                return BuildMonsterChange(item.MonsterId.Value, "deleteItem", item.Id);
            if (item.LootId != null)
                return BuildLootChange(item.LootId.Value, "deleteItem", item.Id);

            throw new NotSupportedException();
        }

        public INotificationPacket BuildItemTakeItem(Item item, Character character, int? remainingQuantity)
        {
            if (item.CharacterId != null)
                return BuildCharacterChange(item.CharacterId.Value, "tookItem", new {Character = _mapper.Map<NamedIdResponse>(character), remainingQuantity, OriginalItem = _mapper.Map<ItemPartialResponse>(item)});
            if (item.MonsterId != null)
                return BuildMonsterChange(item.MonsterId.Value, "tookItem", new {Character = _mapper.Map<NamedIdResponse>(character), remainingQuantity, OriginalItem = _mapper.Map<ItemPartialResponse>(item)});
            if (item.LootId != null)
                return BuildLootChange(item.LootId.Value, "tookItem", new {Character = _mapper.Map<NamedIdResponse>(character), remainingQuantity, OriginalItem = _mapper.Map<ItemPartialResponse>(item)});

            throw new NotSupportedException();
        }

        public INotificationPacket BuildCharacterSetStatBonusAd(int characterId, string stat)
        {
            return BuildCharacterChange(characterId, "statBonusAd", stat);
        }

        public INotificationPacket BuildCharacterAddModifier(int characterId, CharacterModifier characterModifier)
        {
            return BuildCharacterChange(characterId, "addModifier", _mapper.Map<ActiveStatsModifier>(characterModifier));
        }

        public INotificationPacket BuildCharacterRemoveModifier(int characterId, int characterModifierId)
        {
            return BuildCharacterChange(characterId, "removeModifier", characterModifierId);
        }

        public INotificationPacket BuildCharacterUpdateModifier(int characterId, CharacterModifier characterModifier)
        {
            return BuildCharacterChange(characterId, "updateModifier", _mapper.Map<ActiveStatsModifier>(characterModifier));
        }

        public INotificationPacket BuildCharacterGroupInvite(int characterId, GroupInvite groupInvite)
        {
            return BuildCharacterChange(characterId, "groupInvite", _mapper.Map<CharacterGroupInviteResponse>(groupInvite));
        }

        public INotificationPacket BuildCharacterCancelGroupInvite(int characterId, GroupInvite groupInvite)
        {
            return BuildCharacterChange(characterId, "cancelInvite", _mapper.Map<DeleteInviteResponse>(groupInvite));
        }

        public INotificationPacket BuildCharacterAcceptGroupInvite(int characterId, GroupInvite groupInvite)
        {
            return BuildCharacterChange(characterId, "joinGroup", _mapper.Map<CharacterGroupResponse>(groupInvite.Group));
        }

        public INotificationPacket BuildCharacterShowLoot(int characterId, Loot loot)
        {
            return BuildCharacterChange(characterId, "showLoot", _mapper.Map<LootResponse>(loot));
        }

        public INotificationPacket BuildCharacterHideLoot(int characterId, int lootId)
        {
            return BuildCharacterChange(characterId, "hideLoot", lootId);
        }

        public INotificationPacket BuildGroupCharacterInvite(int groupId, GroupInvite groupInvite)
        {
            return BuildGroupChange(groupId, "groupInvite", _mapper.Map<GroupGroupInviteResponse>(groupInvite));
        }

        public INotificationPacket BuildGroupCancelGroupInvite(int groupId, GroupInvite groupInvite)
        {
            return BuildGroupChange(groupId, "cancelInvite", _mapper.Map<DeleteInviteResponse>(groupInvite));
        }

        public INotificationPacket BuildGroupAcceptGroupInvite(int groupId, GroupInvite groupInvite)
        {
            return BuildGroupChange(groupId, "joinCharacter", groupInvite.CharacterId);
        }

        public INotificationPacket BuildGroupChangeGroupData(int groupId, GroupData groupData)
        {
            return BuildGroupChange(groupId, "changeData", groupData);
        }

        public INotificationPacket BuildGroupAddLoot(int groupId, Loot loot)
        {
            return BuildGroupChange(groupId, "addLoot", _mapper.Map<LootResponse>(loot));
        }

        public INotificationPacket BuildGroupDeleteLoot(int groupId, int lootId)
        {
            return BuildGroupChange(groupId, "deleteLoot", lootId);
        }

        public INotificationPacket BuildGroupChangeConfig(in int groupId, GroupConfig groupConfig)
        {
            return BuildGroupChange(groupId, "changeConfig", groupConfig);
        }

        public INotificationPacket BuildGroupAddMonster(int groupId, Monster monster)
        {
            return BuildGroupChange(groupId, "addMonster", _mapper.Map<MonsterResponse>(monster));
        }

        public INotificationPacket BuildGroupKillMonster(int groupId, int monsterId)
        {
            return BuildGroupChange(groupId, "killMonster", monsterId);
        }

        public INotificationPacket BuildLootUpdateVisibility(int lootId, bool visibleForPlayer)
        {
            return BuildLootChange(lootId, "updateVisibility", visibleForPlayer);
        }

        public INotificationPacket BuildLootAddMonster(int lootId, Monster monster)
        {
            return BuildLootChange(lootId, "addMonster", _mapper.Map<MonsterResponse>(monster));
        }

        public INotificationPacket BuildLootAddItem(int lootId, Item item)
        {
            return BuildLootChange(lootId, "addItem", _mapper.Map<ItemResponse>(item));
        }

        public INotificationPacket BuildMonsterAddModifier(int monsterId, ActiveStatsModifier modifier)
        {
            return BuildMonsterChange(monsterId, "addModifier", modifier);
        }

        public INotificationPacket BuildMonsterUpdateModifier(int monsterId, ActiveStatsModifier modifier)
        {
            return BuildMonsterChange(monsterId, "updateModifier", modifier);
        }

        public INotificationPacket BuildMonsterRemoveModifier(int monsterId, int modifierId)
        {
            return BuildMonsterChange(monsterId, "removeModifier", modifierId);
        }

        public INotificationPacket BuildMonsterAddItem(int monsterId, Item item)
        {
            return BuildMonsterChange(monsterId, "addItem", _mapper.Map<ItemResponse>(item));
        }

        public INotificationPacket BuildMonsterUpdateData(int monsterId, MonsterData monsterData)
        {
            return BuildMonsterChange(monsterId, "changeData", monsterData);
        }

        public INotificationPacket BuildMonsterChangeTarget(int monsterId, TargetRequest targetInfo)
        {
            return BuildMonsterChange(monsterId, "changeTarget", targetInfo);
        }

        public INotificationPacket BuildMonsterChangeName(int monsterId, string name)
        {
            return BuildMonsterChange(monsterId, "changeName", name);
        }

        private INotificationPacket BuildCharacterChange(int characterId, string action, object data)
        {
            return new NotificationPacket(
                _hubGroupUtil.GetCharacterGroupName(characterId),
                GetPacket(ElementType.Character, characterId, action, data)
            );
        }

        private INotificationPacket BuildCharacterGmChange(int characterId, string action, object data)
        {
            return new NotificationPacket
            (
                _hubGroupUtil.GetGmCharacterGroupName(characterId),
                GetPacket(ElementType.Character, characterId, action, data)
            );
        }

        private INotificationPacket BuildGroupChange(int groupId, string action, object data)
        {
            return new NotificationPacket
            (
                _hubGroupUtil.GetGroupGroupName(groupId),
                GetPacket(ElementType.Group, groupId, action, data)
            );
        }

        private INotificationPacket BuildLootChange(int lootId, string action, object data)
        {
            return new NotificationPacket
            (
                _hubGroupUtil.GetLootGroupName(lootId),
                GetPacket(ElementType.Loot, lootId, action, data)
            );
        }

        private INotificationPacket BuildMonsterChange(int monsterId, string action, object data)
        {
            return new NotificationPacket
            (
                _hubGroupUtil.GetMonsterGroupName(monsterId),
                GetPacket(ElementType.Monster, monsterId, action, data)
            );
        }

        private static NotificationPacketPayload GetPacket(ElementType elementType, int elementId, string opcode, object data)
        {
            return new NotificationPacketPayload
            (
                elementId,
                elementType.ToString().ToLowerInvariant(),
                opcode,
                data
            );
        }
    }
}