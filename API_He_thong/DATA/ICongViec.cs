using API_He_thong.Models;

namespace API_He_thong.DATA
{
    public interface ICongViec
    {
        Task<List<CongViec>> Getjob();
/*        Task<List<CongViec>> GetJobByDangCongViec(string dangCongViec);
*/        Task<CongViec> GetIdJob(int id);
        Task<bool> PostJob(CongViec s);
        Task<bool> PutJob(int id, CongViec s); 
        Task<bool> DeleteJob(int id);
    }
}
