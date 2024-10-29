using API_He_thong.Models;

namespace API_He_thong.DATA
{
    public interface ISkill
    {
        Task<List<KyNang>> GetSkill();
        Task<KyNang> GetIdSkill(int id);
        Task<bool> PostSkill(KyNang s);
        Task<bool> PutSkill(int id, KyNang s);
        Task<bool> DeleteSkill(int id);
    }
}
