using System;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace Naheulbook.Data.Models
{
    public class ItemTemplateRequirement
    {
        public int Id { get; set; }
        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }

        public Guid ItemTemplateId { get; set; }
        public ItemTemplate ItemTemplate { get; set; } = null!;

        public string StatName { get; set; } = null!;
        public Stat Stat { get; set; } = null!;
    }
}