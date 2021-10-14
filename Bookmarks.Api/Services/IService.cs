using Bookmarks.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookmarks.Api.Services
{
    public interface IService
    {
        public bool Add(UrlList url);
        public UrlList Get(string name);
        public bool Exists(string name);
    }
}
