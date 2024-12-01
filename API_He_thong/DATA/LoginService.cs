using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API_He_thong.Models;
using API_He_thong.Repositories;
using Microsoft.AspNetCore.Identity;

namespace API_He_thong.DATA
{
    public class LoginService:ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<NguoiDung> _passwordHasher;

        public LoginService(ILoginRepository loginRepository, IConfiguration configuration, IPasswordHasher<NguoiDung> passwordHasher)
        {
            _loginRepository = loginRepository;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }
        public async Task<NguoiDung?> AuthenticateAsync(string Email, string mat_khau)
        {
            // Kiểm tra nếu tài khoản hoặc mật khẩu rỗng
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(mat_khau))
            {
                Console.WriteLine("Authentication failed: Missing username or password.");
                return null;
            }

            // Lấy thông tin người dùng từ cơ sở dữ liệu
            var user = await _loginRepository.GetUserAsync(Email);

            if (user == null)
            {
                // Nếu không tìm thấy người dùng
                Console.WriteLine($"Authentication failed: User '{Email}' not found.");
                return null;
            }

            // So sánh mật khẩu nhập vào với mật khẩu lưu trong cơ sở dữ liệu
            if (user.mat_khau != mat_khau)
            {
                // Mật khẩu không chính xác
                Console.WriteLine($"Authentication failed: Incorrect password for user '{Email}'.");
                return null;

            }
            if (user.Email != Email)
            {
                // Mật khẩu không chính xác
                Console.WriteLine($"Authentication failed: Incorrect email for user '{Email}'.");
                return null;

            }


            // Lấy vai trò người dùng từ cơ sở dữ liệu
            var roles = await _loginRepository.GetUserRoleAsync(user.nguoi_dung_id);

            if (roles != null && roles.Any())
            {
                // Gán vai trò đầu tiên cho người dùng (nếu có nhiều vai trò)
                user.Roles = roles.First();
            }
            else
            {
                // Nếu không tìm thấy vai trò
                Console.WriteLine($"Authentication warning: No roles found for user '{Email}'.");
            }

            // Đăng nhập thành công, trả về thông tin người dùng
            return user;
        }
        public string GenerateJwtToken(NguoiDung nguoiDung)
        {
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, nguoiDung.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
        new Claim("Id", nguoiDung.nguoi_dung_id.ToString() ?? string.Empty),
        new Claim("Email", nguoiDung.Email ?? string.Empty)
    };

            // Thêm vai trò vào claim (nếu có)
            if (!string.IsNullOrEmpty(nguoiDung.Roles))
            {
                claims.Add(new Claim(ClaimTypes.Role, nguoiDung.Roles));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"] ?? "unknown",
                _configuration["Jwt:Audience"] ?? "unknown",
                claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["Jwt:DurationInMinutes"] ?? "60")),
                signingCredentials: signIn
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
