using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API_He_thong.Models;
using API_He_thong.Repositories;

namespace API_He_thong.DATA
{
    public class LoginService:ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IConfiguration _configuration;

        public LoginService(ILoginRepository loginRepository, IConfiguration configuration)
        {
           _loginRepository = loginRepository;
            _configuration = configuration;
        }

        public async Task<NguoiDung> AuthenticateAsync(string tai_khoan, string mat_khau)
        {
            return await _loginRepository.GetUserAsync(tai_khoan, mat_khau);
        }

        public string GenerateJwtToken(NguoiDung nguoiDung)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, nguoiDung.ten_dang_nhap),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
            new Claim("Id", nguoiDung.nguoi_dung_id.ToString() ?? string.Empty),
            new Claim("Username", nguoiDung.ten_dang_nhap ?? string.Empty)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"] ?? "unknown",
                _configuration["Jwt:Audience"] ?? "unknown",
                claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["Jwt:DurationInMinutes"] ?? "60")),
                signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
