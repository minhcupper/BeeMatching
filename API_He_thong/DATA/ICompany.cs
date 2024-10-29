using API_He_thong.Models;

namespace API_He_thong.DATA
{
    public interface ICompany
    {
        Task<List<DoanhNghiep>> GetCompany();
        Task<DoanhNghiep> GetIdCompany(int id);
        Task<bool> PostCompany(DoanhNghiep s);
        Task<bool> PutCompany(int id, DoanhNghiep s);
        Task<bool> DeleteCompany(int id);
    }
}
