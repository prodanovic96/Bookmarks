using Bookmarks.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookmarks.Api.Helper
{
    public interface IUrlRepository
    {
        void Add(UrlList url);
        UrlList Get(string listName);
        bool Existing(string listName);
    }
}
