using Bookmarks.Api.Controllers;
using Bookmarks.Api.Helper;
using Bookmarks.Api.Models;
using Bookmarks.Api.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookmarks.Api.Services
{
    public class Service : IService
    {
        private readonly ILogger<UrlController> _logger;
        private readonly IDataBaseRepository _dataBase;
        private IStringHelper _helper;
        private const int titleLength = 7;

        public Service(ILogger<UrlController> logger, IDataBaseRepository dataBase, IStringHelper helper) 
        {
            _logger = logger;
            _dataBase = dataBase;
            _helper = helper;
        }

        public bool Add(UrlList url)
        {
            string name = url.Title.ToLower();

            if (url.Title == string.Empty)
            {
                url.Title = _helper.RandomString(titleLength);

                while (_dataBase.Contain(url.Title))
                {
                    url.Title = _helper.RandomString(titleLength);
                }

                _logger.LogInformation("Empty field title set to random string!");

            }

            if (_dataBase.AddToDataBase(name, url))
            {
                _logger.LogInformation("PostUrlList method successfully called");
                return true;
            }
            else
            {
                _logger.LogInformation("PostUrlList method unsuccessfully called");
                return false;
            }
        }

        public UrlList Get(string name)
        {
            name = name.ToLower();
            if (this.Exists(name))
            {
                return _dataBase.GetFromDataBase(name);
            }
            return null;
        }

        public bool Exists(string name)
        {
            
            name = name.ToLower();
            
            if (_dataBase.Contain(name))
            {

                if (_dataBase.GetFromDataBase(name) != null)
                {
                    _logger.LogInformation("GetUrlList method successfully called");
                    return true;
                }
                else
                {
                    _logger.LogInformation("Existing Item have value null");
                    return false;
                }
            }

            _logger.LogInformation("GetUrlList method unsuccessfully called");
            return false;

        }
    }
}
