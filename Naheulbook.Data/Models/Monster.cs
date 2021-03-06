using System;
using System.Collections.Generic;

namespace Naheulbook.Data.Models
{
    public class Monster
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Data { get; set; }
        public DateTimeOffset? Dead { get; set; }

        public string? Modifiers { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;

        public int? LootId { get; set; }
        public Loot? Loot { get; set; }

        public int? TargetedCharacterId { get; set; }
        public Character? TargetedCharacter { get; set; }

        public int? TargetedMonsterId { get; set; }
        public Monster? TargetedMonster { get; set; }

        public ICollection<Item> Items { get; set; } = null!;
    }
}