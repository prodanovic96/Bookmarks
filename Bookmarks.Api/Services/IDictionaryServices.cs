using Bookmarks.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookmarks.Api.Services
{
    public interface IDictionaryServices
    {
        public bool PostToDictionary(UrlList url);
        public bool GetFromDictionary(string name);
        public UrlList ExistingInDictionary(string name);
    }
}
