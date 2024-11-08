using API_He_thong.Models;

namespace API_He_thong.DATA
{
    public interface ISkillNguoiXinViec
    {
        Task<List<KyNangNguoiXinViec>> GetSkill();
        Task<KyNangNguoiXinViec> GetIdSkill(int id);
        Task<bool> PostSkill(KyNangNguoiXinViec s);
        Task<bool> PutSkill(int id, KyNangNguoiXinViec s);
        Task<bool> DeleteSkill(int id);
    }
}
