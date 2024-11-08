using API_He_thong.Models;

namespace API_He_thong.DATA
{
    public interface ISkillCongViec
    {
        Task<List<KyNangCongViec>> GetSkill();
        Task<KyNangCongViec> GetIdSkill(int id);
        Task<bool> PostSkill(KyNangCongViec s);
        Task<bool> PutSkill(int id, KyNangCongViec s);
        Task<bool> DeleteSkill(int id);
    }
}
