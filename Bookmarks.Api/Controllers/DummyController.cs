using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Bookmarks.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DummyController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetSomeDummyData()
        {
            return await Task.Run(() =>
            {
                var result = new
                {
                    Name = "John",
                    Surname = "Wick",
                    Age = 28
                };

                return Ok(result);
            });
           
        }
    }
}
