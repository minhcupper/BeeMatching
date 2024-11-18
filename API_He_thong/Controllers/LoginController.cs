using API_He_thong.DATA;
using API_He_thong.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace API_He_thong.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NguoiDung _userdata)
        {
            if (_userdata != null && _userdata.ten_dang_nhap != null && _userdata.mat_khau != null)
            {
                var user = await _loginService.AuthenticateAsync(_userdata.ten_dang_nhap, _userdata.mat_khau);
                if (user != null)
                {
                    var token = _loginService.GenerateJwtToken(user);
                    Console.WriteLine($"Generated Token: {token}");
                    return Ok(token);
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
