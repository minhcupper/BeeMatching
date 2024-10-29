using API_He_thong.Models;

namespace API_He_thong.DATA
{
    public interface INguoiXinViec
    {
        Task<List<NguoiTimViec>> GetUser();
        Task<NguoiTimViec> GetIdUser(int id);
        Task<bool> PostUser(NguoiTimViec s);
        Task<bool> PutUser(int id, NguoiTimViec s);
        Task<bool> DeleteUser(int id);
    }
}
