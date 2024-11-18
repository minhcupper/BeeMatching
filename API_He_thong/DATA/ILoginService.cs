using API_He_thong;
using API_He_thong.Models;

namespace API_He_thong.DATA
{
    public interface ILoginService
    {
        Task<NguoiDung> AuthenticateAsync(string tai_khoan, string mat_khau);
        string GenerateJwtToken(NguoiDung nguoiDung);
    }
}
