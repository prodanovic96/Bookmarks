using System.Collections.Generic;

namespace Bookmarks.Api.Models
{
    public class UrlList
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<UrlItem> Items { get; set; }
        public int Id { get; set; }
    }
}
