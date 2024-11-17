using API_He_thong.DATA;
using API_He_thong.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_He_thong.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //
    public class KinhNghiemNguoiXinViecController : ControllerBase
    {
        private readonly IKinhNghiemNguoiXinViec uS;

        public KinhNghiemNguoiXinViecController(IKinhNghiemNguoiXinViec us)
        {
            this.uS = us;
        }

        // GET: api/User/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllKinhNghiem()
        {
            var users = await uS.GetKinhNghiem();
            if (users == null || users.Count == 0)
            {
                return NotFound("No users found.");
            }
            return Ok(users);
        }

        // GET: api/User/GetById/5
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetKinhNghiemById(int id)
        {
            var user = await uS.GetIdKinhNghiem(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(user);
        }

        // POST: api/User/Create
        [HttpPost("Create")]
        public async Task<IActionResult> PostUser(KinhnghiemNguoiTimViec user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user data.");
            }

            var result = await uS.PostKinhNghiem(user);
            if (result)
            {
                return Ok("User created successfully.");
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Error creating user.");
        }

        // DELETE: api/User/Delete/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteKinhNghiemById(int id)
        {
            var result = await uS.DeleteKinhNghiem(id);
            if (result)
            {
                return Ok($"User with ID {id} deleted successfully.");
            }
            return NotFound($"User with ID {id} not found.");
        }

        // PUT: api/User/Edit/5
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> EditKinhNghiemById(int id, KinhnghiemNguoiTimViec user)
        {
            if (user == null || id != user.KinhNghiemId)
            {
                return BadRequest("Invalid user data.");
            }

            var result = await uS.PutKinhNghiem(id, user);
            if (result)
            {
                return Ok("Kinh Nghiem updated successfully.");
            }
            return NotFound($"Kinh Nghiem with ID {id} not found.");
        }
    }
}
