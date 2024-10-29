using API_He_thong.DATA;
using API_He_thong.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_He_thong.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly IPlace uS;

        public PlacesController(IPlace us)
        {
            this.uS = us;
        }

        // GET: api/User/GetAll
        [HttpGet("GetAllDictricts")]
        public async Task<IActionResult> GetAllDictricts()
        {
            var users = await uS.Getdictrics();
            if (users == null || users.Count == 0)
            {
                return NotFound("No users found.");
            }
            return Ok(users);
        }
        // GET: api/User/GetAll
        [HttpGet("GetAllWards")]
        public async Task<IActionResult> GetAllWards()
        {
            var users = await uS.Getwards();
            if (users == null || users.Count == 0)
            {
                return NotFound("No users found.");
            }
            return Ok(users);
        }
        // GET: api/User/GetAll
        [HttpGet("GetAllprovinces")]
        public async Task<IActionResult> GetAllprovinces()
        {
            var users = await uS.Getprovinces();
            if (users == null || users.Count == 0)
            {
                return NotFound("No users found.");
            }
            return Ok(users);
        }
    }
}
