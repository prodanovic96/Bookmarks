using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bookmarks.Api.Models
{
    public class UrlItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public int Id { get; set; }
        [ForeignKey("Standard")]
        public int UrlListId { get; set; }
    }
}
