using API_He_thong.Models;
using Microsoft.EntityFrameworkCore;

namespace API_He_thong.DATA
{
    public class KinhNghiemCongViecService : Ikinhnghiemcongviec
    {
        private readonly API_Context context;

        public KinhNghiemCongViecService(API_Context context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteKinhNghiem(int id)
        {
            var dl = await context.KinhNghiemCongViec.FindAsync(id);
            if (dl != null)
            {
                // Xóa người dùng
                context.KinhNghiemCongViec.Remove(dl);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<KinhNghiemCongViec> GetIdKinhNghiem(int id)
        {
            var dl = await context.KinhNghiemCongViec.FindAsync(id);
            if (dl != null)
            {
                return dl;
            }
            return null; // Or throw an exception, depending on your error handling preference
        }

        public async Task<List<KinhNghiemCongViec>> GetKinhNghiem()
        {
            return await context.KinhNghiemCongViec.ToListAsync();
        }

        public async Task<bool> PostKinhNghiem(KinhNghiemCongViec user)
        {
            await context.KinhNghiemCongViec.AddAsync(user);
            var result = await context.SaveChangesAsync();
            return result > 0; // True if at least one record was added
        }

        public async Task<bool> PutKinhNghiem(int id, KinhNghiemCongViec user)
        {
            var existingUser = await context.KinhNghiemCongViec.FirstOrDefaultAsync(x => x.KinhNghiemId == id);
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
