using Bookmarks.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookmarks.Api.Services
{
    public interface IDataBaseServices
    {
        bool Add(UrlList url);
        UrlList Get(string name);
        bool Existing(string name);
    }
}
