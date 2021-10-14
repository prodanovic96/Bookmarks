using Bookmarks.Api.Models;
using Bookmarks.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Bookmarks.Api.Controllers;
using Bookmarks.Api.Helper;
using System.Data.Entity;

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

        public UrlList GetFromDataBase(string name)
        {
            name = name.ToLower();
            UrlList list = _dataBase.UrlLists.FirstOrDefault(m => m.Title == name);
            list.Items = _dataBase.UrlItems.Include(m => m.UrlListId).Where(m => m.UrlListId == list.Id).ToList();

            if (list != null)
            {
                _logger.LogInformation("GetFromDictionary method successfully called");

            }
            else
            {
                _logger.LogInformation("GetFromDictionary method unsuccessfully called");
            }
            return list;
        }

        public bool PostToDataBase(UrlList url)
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
                foreach (var v in url.Items)
                {
                    _dataBase.UrlItems.Add(v);
                }
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
