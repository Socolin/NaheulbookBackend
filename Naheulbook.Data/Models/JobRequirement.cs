using System;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace Naheulbook.Data.Models
{
    public class JobRequirement
    {
        public int Id { get; set; }

        public long? MinValue { get; set; }
        public long? MaxValue { get; set; }

        public string StatName { get; set; } = null!;
        public Stat Stat { get; set; } = null!;

        public Guid JobId { get; set; }
        public Job Job { get; set; } = null!;
    }
}