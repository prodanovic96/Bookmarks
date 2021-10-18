using Bookmarks.Api.Models;
using Bookmarks.Api.Repository;
using Microsoft.Extensions.Logging;
using Bookmarks.Api.Controllers;
using Bookmarks.Api.Helper;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bookmarks.Api.Services
{
    public class DataBaseServices : IDataBaseServices
    {
        private readonly DataBase _dataBase;
        private readonly ILogger<UrlController> _logger;
        private IStringHelper _helper;
        private const int titleLength = 7;

        public DataBaseServices(DataBase dataBase, ILogger<UrlController> logger, IStringHelper helper)
        {
            _dataBase = dataBase;
            _logger = logger;
            _helper = helper;
        }

        public UrlList Get(string name)
        {
            name = name.ToLower();
            var result = _dataBase.UrlLists
                .Include(list => list.Items)
                .Where(list => list.Title == name)
                .FirstOrDefault();
            
            if (result != null)
            {
                _logger.LogInformation("GetFromDictionary method successfully called");
            }
            else
            {
                _logger.LogInformation("GetFromDictionary method unsuccessfully called");
            }
            return result;
        }

        public bool Add(UrlList url)
        {
            url.Title = url.Title.ToLower();

            if (url.Title == string.Empty)
            {
                url.Title = _helper.RandomString(titleLength);

                while (_dataBase.UrlLists.FirstOrDefault(m => m.Title == url.Title) != null)  
                {
                    url.Title = _helper.RandomString(titleLength);
                }
                _logger.LogInformation("Empty field title set to random string!");
            }

            if (_dataBase.UrlLists.FirstOrDefault(m => m.Title == url.Title) == null)
            {               
                _dataBase.UrlLists.Add(url);
                _dataBase.SaveChanges();

                _logger.LogInformation("PostToDataBase method successfully called");
                return true;
            }

            _logger.LogInformation("PostToDataBase method unsuccessfully called");
            return false;
        }

    }
}
