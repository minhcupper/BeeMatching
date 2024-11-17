using API_He_thong.Models;
using Microsoft.EntityFrameworkCore;

namespace API_He_thong.DATA
{
    public class KinhNghiemNguoiXinViecService : IKinhNghiemNguoiXinViec
    {
        private readonly API_Context context;

        public KinhNghiemNguoiXinViecService(API_Context context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteKinhNghiem(int id)
        {
            var dl = await context.KinhnghiemNguoiTimViec.FindAsync(id);
            if (dl != null)
            {
                // Xóa người dùng
                context.KinhnghiemNguoiTimViec.Remove(dl);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<KinhnghiemNguoiTimViec> GetIdKinhNghiem(int id)
        {
            var dl = await context.KinhnghiemNguoiTimViec.FindAsync(id);
            if (dl != null)
            {
                return dl;
            }
            return null; // Or throw an exception, depending on your error handling preference
        }

        public async Task<List<KinhnghiemNguoiTimViec>> GetKinhNghiem()
        {
            return await context.KinhnghiemNguoiTimViec.ToListAsync();
        }

        public async Task<bool> PostKinhNghiem(KinhnghiemNguoiTimViec user)
        {
            await context.KinhnghiemNguoiTimViec.AddAsync(user);
            var result = await context.SaveChangesAsync();
            return result > 0; // True if at least one record was added
        }

        public async Task<bool> PutKinhNghiem(int id, KinhnghiemNguoiTimViec user)
        {
            var existingUser = await context.KinhnghiemNguoiTimViec.FirstOrDefaultAsync(x => x.KinhNghiemId == id);
            if (existingUser != null)
            {
                // Update fields
                existingUser.MoTa = user.MoTa;
                existingUser.TenKinhNghiem = user.TenKinhNghiem;

                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
