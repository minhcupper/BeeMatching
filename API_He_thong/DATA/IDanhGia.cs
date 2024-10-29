using API_He_thong.Models;

namespace API_He_thong.DATA
{
    public interface IDanhGia
    {
        Task<List<DanhGia>> Getdanhgia();
        Task<DanhGia> GetIddanhgia(int id);
        Task<bool> Postdanhgia(DanhGia s);
        Task<bool> Putdanhgia(int id, DanhGia s);
        Task<bool> Deletedanhgia(int id);
    }
}
