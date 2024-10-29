using API_He_thong.Models;
using Microsoft.EntityFrameworkCore;

namespace API_He_thong.DATA
{
    public class ThongBaoService:IThongBao
    {

        private readonly API_Context context;

        public ThongBaoService(API_Context context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var dl = await context.ThongBao.FindAsync(id);
            if (dl != null)
            {
                // Xóa người dùng
                context.ThongBao.Remove(dl);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<ThongBao> GetIdUser(int id)
        {
            var dl = await context.ThongBao.FindAsync(id);
            if (dl != null)
            {
                return dl;
            }
            return null; // Or throw an exception, depending on your error handling preference
        }

        public async Task<List<ThongBao>> GetUser()
        {
            return await context.ThongBao.ToListAsync();
        }

        public async Task<bool> PostUser(ThongBao user)
        {
            await context.ThongBao.AddAsync(user);
            var result = await context.SaveChangesAsync();
            return result > 0; // True if at least one record was added
        }

        public async Task<bool> PutUser(int id, ThongBao user)
        {
            var existingUser = await context.ThongBao.FirstOrDefaultAsync(x => x.thong_bao_id == id);
            if (existingUser != null)
            {
                // Update fields
                existingUser.ngay_gui = user.ngay_gui;
                existingUser.noi_dung = user.noi_dung;
                existingUser.tieu_de=user.tieu_de;
                existingUser.trang_thai = user.trang_thai;

                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
