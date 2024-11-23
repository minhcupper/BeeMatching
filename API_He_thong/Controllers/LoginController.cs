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
            // Kiểm tra xem dữ liệu người dùng có hợp lệ không
            if (_userdata == null || string.IsNullOrEmpty(_userdata.ten_dang_nhap) || string.IsNullOrEmpty(_userdata.mat_khau))
            {
                return BadRequest(new { error = "Dữ liệu người dùng là bắt buộc." });
            }

            // Xác thực người dùng với tài khoản và mật khẩu
            var user = await _loginService.AuthenticateAsync(_userdata.ten_dang_nhap, _userdata.mat_khau);

            if (user != null)
            {
                // Kiểm tra và gán vai trò người dùng
                if (string.IsNullOrEmpty(user.Roles))
                {
                    return BadRequest(new { error = "Không tìm thấy vai trò người dùng." });
                }

                // Tạo JWT token cho người dùng
                var token = _loginService.GenerateJwtToken(user);

                // Trả về token, id, username và vai trò
                return Ok(new
                {
                    nguoi_dung_id = user.nguoi_dung_id, // Sửa từ 'id' thành 'nguoi_dung_id'
                    token = token,
                    username = user.ten_dang_nhap,
                    roles = user.Roles
                });
            }
            else
            {
                return BadRequest(new { error = "Thông tin đăng nhập không hợp lệ" });
            }
        }

    }
}
