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
        private readonly IService _service;

        public UrlController(IService service)
        {
            _service = service;
        }

        
        [HttpGet]       //[HttpGet("getdata")]
        public IActionResult GetUrlList([FromQuery]string name)
        {
            UrlList list = _service.Get(name);
            
            if (list != null)
            {
                return Ok(list);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult PostUrlList([FromBody]UrlList url)
        {           
            if (_service.Add(url))
            { 
                return StatusCode(201);
            }
            else
            {    
                return BadRequest("URL List with name:   " + url.Title + "  already exist!!!");
            } 
        }
    }
}
