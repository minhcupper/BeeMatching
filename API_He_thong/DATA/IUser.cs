using API_He_thong.Models;

namespace API_He_thong.DATA
{
    public interface IUser
    {
        Task<List<NguoiDung>> GetUser();
        Task<NguoiDung> GetIdUser(int id);
        Task<bool> PostUser(NguoiDung user);
        Task<bool> PutUser(int id, NguoiDung user);
        Task<bool> DeleteUser(int id);    
    }
}
