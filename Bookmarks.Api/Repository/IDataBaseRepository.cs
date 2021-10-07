using Bookmarks.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookmarks.Api.Repository
{
    public interface IDataBaseRepository
    {
        bool AddToDataBase(string n,UrlList list);
        UrlList GetFromDataBase(string s);
        bool Contain(string s);
    }
}
