using Bookmarks.Api.Models;
using Bookmarks.Api.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bookmarks.Api.Helper
{
    public class UrlRepository : IUrlRepository
    {
        private DataBase _dbcontext;

        public UrlRepository(DataBase dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public void Add(UrlList url)
        {
            _dbcontext.UrlLists.Add(url);
            _dbcontext.SaveChanges();
        }

        public UrlList Get(string listName)
        {
            var result = _dbcontext.UrlLists
               .Include(list => list.Items)
               .Where(list => list.Title == listName)
               .FirstOrDefault();

            return result;
        }

        public bool Existing(string listName)
        {
            bool result = _dbcontext.UrlLists.Any(list => list.Title == listName);
            
            return result;
        }
    }
}
