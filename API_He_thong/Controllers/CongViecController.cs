﻿using API_He_thong.DATA;
using API_He_thong.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_He_thong.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CongViecController : ControllerBase
    {
        private readonly ICongViec uS;

        public CongViecController(ICongViec us)
        {
            this.uS = us;
        }

        // GET: api/User/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await uS.Getjob();
            if (users == null || users.Count == 0)
            {
                return NotFound("No users found.");
            }
            return Ok(users);
        }

        // GET: api/User/GetById/05
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await uS.GetIdJob(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(user);
        }

        /*[HttpGet("GetByDangCongViec/{dangCongViec}")]
        public async Task<IActionResult> GetJobByDangCongViec(string dangCongViec)
        {
            var listCongViec = await GetJobByDangCongViec(dangCongViec);
            if(listCongViec == null)
            {
                return NotFound();
            }
            return Ok(listCongViec);
        }*/

        // POST: api/User/Create
        [HttpPost("Create")]
        public async Task<IActionResult> PostUser(CongViec user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user data.");
            }

            var result = await uS.PostJob(user);
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
            var result = await uS.DeleteJob(id);
            if (result)
            {
                return Ok($"User with ID {id} deleted successfully.");
            }
            return NotFound($"User with ID {id} not found.");
        }

        // PUT: api/User/Edit/5
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> EditUserById(int id, CongViec user)
        {
            if (user == null || id != user.CongViecId)
            {
                return BadRequest("Invalid user data.");
            }

            var result = await uS.PutJob(id, user);
            if (result)
            {
                return Ok("User updated successfully.");
            }
            return NotFound($"User with ID {id} not found.");
        }

        
    }
}
