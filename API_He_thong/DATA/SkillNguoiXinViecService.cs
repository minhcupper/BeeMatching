using API_He_thong.Models;
using Microsoft.EntityFrameworkCore;

namespace API_He_thong.DATA
{
    public class SkillNguoiXinViecService:ISkillNguoiXinViec
    {
        private readonly API_Context context;

        public SkillNguoiXinViecService(API_Context context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteSkill(int id)
        {
            var dl = await context.KyNangNguoiXinViec.FindAsync(id);
            if (dl != null)
            {
                // Xóa người dùng
                context.KyNangNguoiXinViec.Remove(dl);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<KyNangNguoiXinViec> GetIdSkill(int id)
        {
            var dl = await context.KyNangNguoiXinViec.FindAsync(id);
            if (dl != null)
            {
                return dl;
            }
            return null; // Or throw an exception, depending on your error handling preference
        }

        public async Task<List<KyNangNguoiXinViec>> GetSkill()
        {
            return await context.KyNangNguoiXinViec.ToListAsync();
        }

        public async Task<bool> PostSkill(KyNangNguoiXinViec user)
        {
            await context.KyNangNguoiXinViec.AddAsync(user);
            var result = await context.SaveChangesAsync();
            return result > 0; // True if at least one record was added
        }

        public async Task<bool> PutSkill(int id, KyNangNguoiXinViec user)
        {
            var existingUser = await context.KyNangNguoiXinViec.FirstOrDefaultAsync(x => x.KyNangId == id);
            if (existingUser != null)
            {
                // Update fields
                existingUser.MoTa = user.MoTa;
                existingUser.DanhMucKyNang = user.DanhMucKyNang;
                existingUser.KyNangId = user.KyNangId;
                existingUser.TenKyNang = user.TenKyNang;
     

                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
