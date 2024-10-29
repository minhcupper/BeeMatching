using API_He_thong.Models;

namespace API_He_thong.DATA
{
    public interface IUngtuyen
    {
        Task<List<UngTuyen>> GetUser();
        Task<UngTuyen> GetIdUser(int id);
        Task<bool> PostUser(UngTuyen user);
        Task<bool> PutUser(int id, UngTuyen user);
        Task<bool> DeleteUser(int id);
    }
}
