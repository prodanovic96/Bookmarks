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
        private readonly IDataBaseServices _dataBaseServices;
        public UrlController(IDataBaseServices dataBaseServices)
        {
            _dataBaseServices = dataBaseServices;
        }
 
        [HttpGet]
        public IActionResult GetUrlList([FromQuery]string name)
        {
            name = name.ToLower();
            if (_dataBaseServices.GetFromDataBase(name) != null)
            {
                return Ok(_dataBaseServices.GetFromDataBase(name));
            }
            return NotFound("URL List with name: " + name + "  don't exist!!!");
        }

        [HttpPost]
        public IActionResult PostUrlList([FromBody]UrlList url)
        {
            if (_dataBaseServices.PostToDataBase(url))
            { 
                return StatusCode(201);
            }
            else
            {
                return BadRequest("URL List with name: " + url.Title + "  already exist!!!");
            }
        }
    }
}
