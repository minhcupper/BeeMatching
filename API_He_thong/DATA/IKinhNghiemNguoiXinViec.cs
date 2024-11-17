using API_He_thong.Models;

namespace API_He_thong.DATA
{
    public interface IKinhNghiemNguoiXinViec
    {
        Task<List<KinhnghiemNguoiTimViec>> GetKinhNghiem();
        Task<KinhnghiemNguoiTimViec> GetIdKinhNghiem(int id);
        Task<bool> PostKinhNghiem(KinhnghiemNguoiTimViec s);
        Task<bool> PutKinhNghiem(int id, KinhnghiemNguoiTimViec s);
        Task<bool> DeleteKinhNghiem(int id);
    }
}
