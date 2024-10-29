using API_He_thong.Models;
using Microsoft.EntityFrameworkCore;

namespace API_He_thong.DATA
{
    public class SkillService:ISkill
    {
        private readonly API_Context context;

        public SkillService(API_Context context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteSkill(int id)
        {
            var dl = await context.KyNang.FindAsync(id);
            if (dl != null)
            {
                // Xóa người dùng
                context.KyNang.Remove(dl);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<KyNang> GetIdSkill(int id)
        {
            var dl = await context.KyNang.FindAsync(id);
            if (dl != null)
            {
                return dl;
            }
            return null; // Or throw an exception, depending on your error handling preference
        }

        public async Task<List<KyNang>> GetSkill()
        {
            return await context.KyNang.ToListAsync();
        }

        public async Task<bool> PostSkill(KyNang user)
        {
            await context.KyNang.AddAsync(user);
            var result = await context.SaveChangesAsync();
            return result > 0; // True if at least one record was added
        }

        public async Task<bool> PutSkill(int id, KyNang user)
        {
            var existingUser = await context.KyNang.FirstOrDefaultAsync(x => x.ky_nang_id == id);
            if (existingUser != null)
            {
                // Update fields
                existingUser.ten_ky_nang = user.ten_ky_nang;
                existingUser.mo_ta = user.mo_ta;
         
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
