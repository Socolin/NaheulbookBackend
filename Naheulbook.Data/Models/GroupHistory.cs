using System;

namespace Naheulbook.Data.Models
{
    public class GroupHistory
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string Data { get; set; }
        public DateTime Date { get; set; }
        public bool Gm { get; set; }
        public string Info { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}