using System;
using Naheulbook.Core.Models;
using Naheulbook.Data.Models;
using Naheulbook.Shared.TransientModels;
using Naheulbook.Shared.Utils;

namespace Naheulbook.Core.Factories
{
    public interface IItemFactory
    {
        Item CreateItem(ItemOwnerType ownerType, int ownerId, ItemTemplate itemTemplate, ItemData itemData);
        Item CreateItem(ItemTemplate itemTemplate, ItemData itemData);
    }

    public class ItemFactory : IItemFactory
    {
        private readonly IJsonUtil _jsonUtil;

        public ItemFactory(IJsonUtil jsonUtil)
        {
            _jsonUtil = jsonUtil;
        }

        public Item CreateItem(ItemOwnerType ownerType, int ownerId, ItemTemplate itemTemplate, ItemData itemData)
        {
            var item = CreateItem(itemTemplate, itemData);

            switch (ownerType)
            {
                case ItemOwnerType.Character:
                    item.CharacterId = ownerId;
                    break;
                case ItemOwnerType.Loot:
                    item.LootId = ownerId;
                    break;
                case ItemOwnerType.Monster:
                    item.MonsterId = ownerId;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ownerType), ownerType, null);
            }

            return item;
        }

        public Item CreateItem(ItemTemplate itemTemplate, ItemData itemData)
        {
            var itemTemplateData = _jsonUtil.DeserializeOrCreate<PartialItemTemplateData>(itemTemplate.Data);

            if (itemTemplateData.Charge.HasValue)
                itemData.Charge = itemTemplateData.Charge.Value;
            if (itemTemplateData.Icon != null)
                itemData.Icon = itemTemplateData.Icon;
            if (itemTemplateData.Lifetime != null)
                itemData.Lifetime = itemTemplateData.Lifetime;
            if (itemTemplateData.Quantifiable == true && itemData.Quantity == null)
                itemData.Quantity = 1;
            if (string.IsNullOrEmpty(itemData.Name))
            {
                if (itemData.NotIdentified == true && !string.IsNullOrEmpty(itemTemplateData.NotIdentifiedName))
                    itemData.Name = itemTemplateData.NotIdentifiedName;
                else
                    itemData.Name = itemTemplate.Name;
            }

            var item = new Item
            {
                Data = _jsonUtil.SerializeNonNull(itemData),
                ItemTemplateId = itemTemplate.Id
            };
            return item;
        }
    }
}