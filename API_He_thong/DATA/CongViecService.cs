using API_He_thong.Models;
using Microsoft.EntityFrameworkCore;

namespace API_He_thong.DATA
{
    public class CongViecService
    {
        private readonly API_Context context;

        public CongViecService(API_Context context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteJob(int id)
        {
            var dl = await context.CongViec.FindAsync(id);
            if (dl != null)
            {
                // Xóa người dùng
                context.CongViec.Remove(dl);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CongViec> GetIdJob(int id)
        {
            var dl = await context.CongViec.FindAsync(id);
            if (dl != null)
            {
                return dl;
            }
            return null; // Or throw an exception, depending on your error handling preference
        }

        public async Task<List<CongViec>> GetJob()
        {
            return await context.CongViec.ToListAsync();
        }

        public async Task<bool> PostUser(CongViec user)
        {
            await context.CongViec.AddAsync(user);
            var result = await context.SaveChangesAsync();
            return result > 0; // True if at least one record was added
        }

        public async Task<bool> PutJob(int id, CongViec user)
        {
            var existingUser = await context.CongViec.FirstOrDefaultAsync(x => x.cong_viec_id == id);
            if (existingUser != null)
            {
                // Update fields
                existingUser.dia_diem_lam_viec = user.dia_diem_lam_viec;
                existingUser.mo_ta = user.mo_ta;
                existingUser.tieu_de = user.tieu_de;
                existingUser.vi_tri = user.vi_tri;
                existingUser.ky_nang_yeu_cau = user.ky_nang_yeu_cau;
                existingUser.han_nop_ho_so=user.han_nop_ho_so;
                existingUser.luong_hang_thang=user.luong_hang_thang;
                existingUser.ngay_dang = user.ngay_dang;
                existingUser.trang_thai=user.trang_thai;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
