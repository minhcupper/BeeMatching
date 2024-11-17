using API_He_thong.Models;
using Microsoft.EntityFrameworkCore;

namespace API_He_thong.DATA
{
    public class KyNangCongViecSerVice:ISkillCongViec
    {
        private readonly API_Context context;

        public KyNangCongViecSerVice(API_Context context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteSkill(int id)
        {
            var dl = await context.KyNangCongViec.FindAsync(id);
            if (dl != null)
            {
                // Xóa người dùng
                context.KyNangCongViec.Remove(dl);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<KyNangCongViec> GetIdSkill(int id)
        {
            var dl = await context.KyNangCongViec.FindAsync(id);
            if (dl != null)
            {
                return dl;
            }
            return null; // Or throw an exception, depending on your error handling preference
        }

        public async Task<List<KyNangCongViec>> GetSkill()
        {
            return await context.KyNangCongViec.ToListAsync();
        }

        public async Task<bool> PostSkill(KyNangCongViec user)
        {
            await context.KyNangCongViec.AddAsync(user);
            var result = await context.SaveChangesAsync();
            return result > 0; // True if at least one record was added
        }

        public async Task<bool> PutSkill(int id, KyNangCongViec user)
        {
            var existingUser = await context.KyNangCongViec.FirstOrDefaultAsync(x => x.KyNangId == id);
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

