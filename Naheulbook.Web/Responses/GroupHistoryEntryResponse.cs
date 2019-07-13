using Newtonsoft.Json.Linq;

namespace Naheulbook.Web.Responses
{
    public class GroupHistoryEntryResponse : IHistoryEntryResponse
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public JObject Data { get; set; }
        public string Date { get; set; }
        public bool Gm { get; set; }
        public string Info { get; set; }
        public bool IsGroup => true;
    }
}