using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookmarks.Api.Models
{
    public class UrlList
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<UrlItem> List { get; set; }
    }
}
