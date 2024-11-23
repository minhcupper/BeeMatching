using API_He_thong.Models;

namespace API_He_thong.Repositories
{
    public interface ILoginRepository
    {
        Task<NguoiDung> GetUserAsync(string tai_khoan);
        Task<List<string>> GetUserRoleAsync(int nguoi_dung_id);
     
    }
}
