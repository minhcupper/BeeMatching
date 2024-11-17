using API_He_thong.DATA;
using API_He_thong.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_He_thong.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DanhGiaController : ControllerBase
    {
        private readonly IDanhGia uS;

        public DanhGiaController(IDanhGia us)
        {
            this.uS = us;
        }

        // GET: api/User/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await uS.Getdanhgia();
            if (users == null || users.Count == 0)
            {
                return NotFound("No users found.");
            }
            return Ok(users);
        }

        // GET: api/User/GetById/5
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await uS.GetIddanhgia(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(user);
        }

        // POST: api/User/Create
        [HttpPost("Create")]
        public async Task<IActionResult> PostUser(DanhGia user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user data.");
            }

            var result = await uS.Postdanhgia(user);
            if (result)
            {
                return Ok("User created successfully.");
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Error creating user.");
        }

        // DELETE: api/User/Delete/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            var result = await uS.Deletedanhgia(id);
            if (result)
            {
                return Ok($"User with ID {id} deleted successfully.");
            }
            return NotFound($"User with ID {id} not found.");
        }

        // PUT: api/User/Edit/5
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> EditUserById(int id, DanhGia user)
        {
            if (user == null || id != user.DanhGiaId)
            {
                return BadRequest("Invalid user data.");
            }

            var result = await uS.Putdanhgia(id, user);
            if (result)
            {
                return Ok("User updated successfully.");
            }
            return NotFound($"User with ID {id} not found.");
        }
    }
}
