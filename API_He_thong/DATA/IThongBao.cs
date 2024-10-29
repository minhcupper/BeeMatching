using API_He_thong.Models;

namespace API_He_thong.DATA
{
    public interface IThongBao
    {
        Task<List<ThongBao>> GetUser();
        Task<ThongBao> GetIdUser(int id);
        Task<bool> PostUser(ThongBao user);
        Task<bool> PutUser(int id, ThongBao user);
        Task<bool> DeleteUser(int id);
    }
}
