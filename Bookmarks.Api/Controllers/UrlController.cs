using Bookmarks.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Bookmarks.Api.Helper;
using Bookmarks.Api.Repository;

namespace Bookmarks.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UrlController : ControllerBase
    { 
        private readonly ILogger<UrlController> _logger;
        private readonly IDataBaseRepository _dataBase;
        private IStringHelper _helper;

        public UrlController(ILogger<UrlController> logger, IDataBaseRepository dataBase, IStringHelper helper)
        {
            _logger = logger;
            _dataBase = dataBase;
            _helper = helper;
        }

        
        [HttpGet]       //[HttpGet("getdata")]
        public IActionResult GetUrlList([FromQuery]string name)
        {
            name = name.ToLower();

            if (_dataBase.Contain(name))
            {
                _logger.LogInformation("GetUrlList method successfully called");
                return Ok(_dataBase.GetFromDataBase(name));       
            }
            else
            {
                _logger.LogInformation("GetUrlList method unsuccessfully called");
                return BadRequest();              
            }      
        }

        [HttpPost]
        public IActionResult PostUrlList([FromBody]UrlList url)
        {
            string name = url.Title.ToLower();
            bool valideLink = true;

            if (url.Title == string.Empty)
            {
                url.Title = _helper.RandomString(7);

                while(_dataBase.Contain(url.Title)) 
                {
                    url.Title = _helper.RandomString(7);
                } 
            }

            for(int i = 0; i < url.List.Count; i++)
            {
                if (!_helper.UrlValidation(url.List[i].Link))
                {
                    valideLink = false;
                }
            }

            if (valideLink)
            {
                if (_dataBase.AddToDataBase(name, url))
                {
                    _logger.LogInformation("PostUrlList method successfully called");
                    return Ok();
                }
                else
                {
                    _logger.LogInformation("PostUrlList method unsuccessfully called");
                    return BadRequest("URL List with name:   " + name + "  already exist!!!");
                }
            }
            else
            {
                _logger.LogInformation("URL link not valide");
                return BadRequest("URL link is not valide!!!");
            }    
        }
    }
}
