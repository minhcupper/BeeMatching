using API_He_thong.Models;
using Microsoft.EntityFrameworkCore;

namespace API_He_thong.DATA
{
    public class NguoiXinViecService:INguoiXinViec
    {
        private readonly API_Context context;

        public NguoiXinViecService(API_Context context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var dl = await context.NguoiTimViec.FindAsync(id);
            if (dl != null)
            {
                // Xóa người dùng
                context.NguoiTimViec.Remove(dl);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<NguoiTimViec> GetIdUser(int id)
        {
            var dl = await context.NguoiTimViec.FindAsync(id);
            if (dl != null)
            {
                return dl;
            }
            return null; // Or throw an exception, depending on your error handling preference
        }

        public async Task<List<NguoiTimViec>> GetUser()
        {
            return await context.NguoiTimViec.ToListAsync();
        }

        public async Task<bool> PostUser(NguoiTimViec user)
        {
            await context.NguoiTimViec.AddAsync(user);
            var result = await context.SaveChangesAsync();
            return result > 0; // True if at least one record was added
        }

        public async Task<bool> PutUser(int id, NguoiTimViec user)
        {
            var existingUser = await context.NguoiTimViec.FirstOrDefaultAsync(x => x.nguoi_tim_viec_id == id);
            if (existingUser != null)
            {
                // Update fields
                existingUser.ngon_ngu = user.ngon_ngu;
                existingUser.kinh_nghiem = user.kinh_nghiem;
                existingUser.mo_ta = user.mo_ta;
                existingUser.hoat_dong_ngoai_khoa = user.hoat_dong_ngoai_khoa;
               

                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
