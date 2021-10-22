using Bookmarks.Api.Models;
using Microsoft.Extensions.Logging;
using Bookmarks.Api.Controllers;
using Bookmarks.Api.Helper;

namespace Bookmarks.Api.Services
{
    public class DataBaseServices : IDataBaseServices
    {
        private readonly IUrlRepository _urlRepository;
        private readonly ILogger<UrlController> _logger;
        private IStringHelper _helper;
        private const int titleLength = 7;

        public DataBaseServices(IUrlRepository urlRepository, ILogger<UrlController> logger, IStringHelper helper)
        {
            _urlRepository = urlRepository;
            _logger = logger;
            _helper = helper;
        }

        public UrlList Get(string name)
        {
            name = name.ToLower();
            
            UrlList list = _urlRepository.Get(name);

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

        public bool Add(UrlList url)
        {
            if (url.Title == string.Empty)
            {
                url.Title = _helper.RandomString(titleLength);

                while (_urlRepository.Existing(url.Title))
                {
                    url.Title = _helper.RandomString(titleLength);
                }

                _logger.LogInformation("Empty field title set to random string!");
            }

            url.Title = url.Title.ToLower();
            
            if (!_urlRepository.Existing(url.Title))
            {
                _urlRepository.Add(url);
                _logger.LogInformation("PostToDataBase method successfully called");
                return true;
            }

            _logger.LogInformation("PostToDataBase method unsuccessfully called");
            return false;
        }
        
        public bool Existing(string name)
        {
            return _urlRepository.Existing(name);
        }
    }
}
