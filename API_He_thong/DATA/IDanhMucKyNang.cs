using API_He_thong.Models;

namespace API_He_thong.DATA
{
    public interface IDanhMucKyNang
    {
        Task<List<DanhMucKyNang>> GetDanhMuc();
        Task<DanhMucKyNang> GetIdDanhMuc(int id);
        Task<bool> PostDanhMuc(DanhMucKyNang s);
        Task<bool> PutDanhMuc(int id, DanhMucKyNang s);
        Task<bool> DeleteDanhMuc(int id);
    }
}
