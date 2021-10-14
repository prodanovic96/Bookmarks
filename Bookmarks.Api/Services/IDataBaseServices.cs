using Bookmarks.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookmarks.Api.Services
{
    public interface IDataBaseServices
    {
        public bool PostToDataBase(UrlList url);
        public bool ExistingInDataBase(string name);
        public UrlList GetFromDataBase(string name);
    }
}
