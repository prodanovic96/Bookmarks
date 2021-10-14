using Bookmarks.Api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;

namespace Bookmarks.Api.Repository
{
    public class DataBaseRepository : IDataBaseRepository
    {
        private Dictionary<string, UrlList> collection;

        public DataBaseRepository()
        {
            if (collection == null)
            {
                collection = new Dictionary<string, UrlList>(); // nema potrebe za proverom da li je null jer ce zbog Singleton-a uci ovde samo jednom
            }
        }

        public bool AddToDataBase(string n,UrlList list)
        {
            if (!Contain(n))
            {
                collection.Add(n, list);
                return true;
            }
            return false;
        }

        public UrlList GetFromDataBase(string s)
        {
            if (Contain(s))
            {
                return collection[s];
            }
            return null;
        }

        public bool Contain(string s)
        {
            return collection.ContainsKey(s);
        } 
    }
}
