using API_He_thong.Models;

namespace API_He_thong.Repositories
{
    public interface ILoginRepository
    {
        Task<NguoiDung> GetUserAsync(string tai_khoan, string mat_khau);
    }
}
