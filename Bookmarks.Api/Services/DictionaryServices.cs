
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
    public class DictionaryServices : IDictionaryServices
    {
        private readonly ILogger<UrlController> _logger;
        private readonly IDataBaseRepository _dataDictionary;
        private IStringHelper _helper;
        private const int titleLength = 7;

        public DictionaryServices(ILogger<UrlController> logger, IDataBaseRepository dataDictionary, IStringHelper helper)
        {
            _logger = logger;
            _dataDictionary = dataDictionary;
            _helper = helper;
        }

        public bool PostToDictionary(UrlList url)
        {
            string name = url.Title.ToLower();

            if (url.Title == string.Empty)
            {
                url.Title = _helper.RandomString(titleLength);

                while (_dataDictionary.Contain(url.Title))
                {
                    url.Title = _helper.RandomString(titleLength);
                }

                _logger.LogInformation("Empty field title set to random string!");

            }

            if (_dataDictionary.AddToDataBase(name, url))
            {
                _logger.LogInformation("PostToDictionary method successfully called");
                return true;
            }
            else
            {
                _logger.LogInformation("PostToDictionary method unsuccessfully called");
                return false;
            }
        }

        public bool GetFromDictionary(string name)
        {
            name = name.ToLower();
            if (_dataDictionary.Contain(name))
            {
                if (ExistingInDictionary(name) != null)
                {
                    _logger.LogInformation("GetFromDictionary method successfully called");
                    return true;
                }
                else
                {
                    _logger.LogInformation("Existing Item have value null");
                    return false;
                }
            }
            else
            {
                _logger.LogInformation("GetFromDictionary method unsuccessfully called");
                return false;
            }
        }

        public UrlList ExistingInDictionary(string name)
        {
            return _dataDictionary.GetFromDataBase(name);
        }
    
    }
}
