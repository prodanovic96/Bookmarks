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
using Bookmarks.Api.Services;

namespace Bookmarks.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UrlController : ControllerBase
    {
        private readonly IDictionaryServices _dictionaryService;
        private readonly IDataBaseServices _dataBaseServices;
        public UrlController(IDictionaryServices dictionaryService, IDataBaseServices dataBaseServices)
        {
            _dictionaryService = dictionaryService;
            _dataBaseServices = dataBaseServices;
        }
 
        [HttpGet]
        public IActionResult GetUrlList([FromQuery]string name)
        {
            name = name.ToLower();
            if (_dataBaseServices.GetFromDataBase(name))
            {
                _dictionaryService.GetFromDictionary(name);
                return Ok(_dataBaseServices.ExistingInDataBase(name));
            }
            return BadRequest("URL List with name: " + name + "  don't exist!!!");
        }

        [HttpPost]
        public IActionResult PostUrlList([FromBody]UrlList url)
        {
            if (_dataBaseServices.PostToDataBase(url))
            {
                _dictionaryService.PostToDictionary(url);
                return Ok();
            }
            else
            {
                return BadRequest("URL List with name: " + url.Title + "  already exist!!!");
            }
        }
    }
}
