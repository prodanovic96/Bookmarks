using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookmarks.Api.Models
{
    public class UrlItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [RegularExpression(@"(http://|https://)?(www\.)?\w+\.(com|net|edu|org)",
         ErrorMessage = "Only accept http and https links.")]
        public string Link { get; set; }
    }
}
