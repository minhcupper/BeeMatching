using API_He_thong.Models;

namespace API_He_thong.DATA
{
    public interface Ikinhnghiemcongviec
    {
        Task<List<KinhNghiemCongViec>> GetKinhNghiem();
        Task<KinhNghiemCongViec> GetIdKinhNghiem(int id);
        Task<bool> PostKinhNghiem(KinhNghiemCongViec s);
        Task<bool> PutKinhNghiem(int id, KinhNghiemCongViec s);
        Task<bool> DeleteKinhNghiem(int id);
    }
}
