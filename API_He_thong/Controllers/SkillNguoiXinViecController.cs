using API_He_thong.DATA;
using API_He_thong.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_He_thong.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillNguoiXinViec uS;

        public SkillController(ISkillNguoiXinViec us)
        {
            this.uS = us;
        }

        // GET: api/User/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCompanys()
        {
            var users = await uS.GetSkill();
            if (users == null || users.Count == 0)
            {
                return NotFound("No users found.");
            }
            return Ok(users);
        }

        // GET: api/User/GetById/5
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            var user = await uS.GetIdSkill(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(user);
        }

        // POST: api/User/Create
        [HttpPost("Create")]
        public async Task<IActionResult> PostUser(KyNangNguoiXinViec user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user data.");
            }

            var result = await uS.PostSkill(user);
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
            var result = await uS.DeleteSkill(id);
            if (result)
            {
                return Ok($"Company with ID {id} deleted successfully.");
            }
            return NotFound($"Company with ID {id} not found.");
        }

        // PUT: api/User/Edit/5
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> EditUserById(int id, KyNangNguoiXinViec user)
        {
            if (user == null || id != user.KyNangId)
            {
                return BadRequest("Invalid user data.");
            }

            var result = await uS.PutSkill(id, user);
            if (result)
            {
                return Ok("User updated successfully.");
            }
            return NotFound($"User with ID {id} not found.");
        }
    }
}
