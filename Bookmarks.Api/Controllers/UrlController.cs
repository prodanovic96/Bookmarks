using Bookmarks.Api.Models;
using Microsoft.AspNetCore.Mvc;
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
 
        [HttpGet("get")]
        public IActionResult GetUrlList([FromQuery]string name)
        {
            name = name.ToLower();

            if (_dataBaseServices.Get(name) != null)
            {
                return Ok(_dataBaseServices.Get(name));
            }
            return NotFound("URL List with name: " + name + "  don't exist!!!");
        }

        [HttpGet("namereserved")]
        public IActionResult IsNameReserved([FromQuery] string name)
        {
            name = name.ToLower();

            bool result = _dataBaseServices.Existing(name);
            
            return Ok(result);
            //return NotFound("URL List with name: " + name + " already  exist!!!");
        }

        [HttpPost]
        public IActionResult PostUrlList([FromBody]UrlList url)
        {  
            if (_dataBaseServices.Add(url))
            {
                return Created("",url);
            }
            else
            {
                return BadRequest("URL List with name: " + url.Title + "  already exist!!!");
            }
        }
    }
}
