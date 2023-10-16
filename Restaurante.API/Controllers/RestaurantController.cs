using Microsoft.AspNetCore.Mvc;

namespace Restaurante.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : ControllerBase
    {

        public RestaurantController()
        {
        }

        [HttpGet("[action]")]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
