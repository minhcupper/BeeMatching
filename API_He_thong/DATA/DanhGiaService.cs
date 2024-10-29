using API_He_thong.Models;
using Microsoft.EntityFrameworkCore;

namespace API_He_thong.DATA
{
    public class DanhGiaService:IDanhGia
    {
        private readonly API_Context context;

        public DanhGiaService(API_Context context)
        {
            this.context = context;
        }

        public async Task<bool> Deletedanhgia(int id)
        {
            var dl = await context.DanhGia.FindAsync(id);
            if (dl != null)
            {
                // Xóa người dùng
                context.DanhGia.Remove(dl);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<DanhGia> GetIddanhgia(int id)
        {
            var dl = await context.DanhGia.FindAsync(id);
            if (dl != null)
            {
                return dl;
            }
            return null; // Or throw an exception, depending on your error handling preference
        }

        public async Task<List<DanhGia>> Getdanhgia()
        {
            return await context.DanhGia.ToListAsync();
        }

        public async Task<bool> Postdanhgia(DanhGia user)
        {
            await context.DanhGia.AddAsync(user);
            var result = await context.SaveChangesAsync();
            return result > 0; // True if at least one record was added
        }

        public async Task<bool> Putdanhgia(int id, DanhGia user)
        {
            var existingUser = await context.DanhGia.FirstOrDefaultAsync(x => x.danh_gia_id == id);
            if (existingUser != null)
            {
                // Update fields
                existingUser.ngay_danh_gia = user.ngay_danh_gia;
                existingUser.diem_danh_gia = user.diem_danh_gia;
                existingUser.noi_dung_danh_gia = user.noi_dung_danh_gia;
            
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
