using API_He_thong.Models;
using Microsoft.EntityFrameworkCore;

namespace API_He_thong.DATA
{
    public class UngTuyenService:IUngtuyen
    {
        private readonly API_Context context;

        public UngTuyenService(API_Context context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var dl = await context.UngTuyen.FindAsync(id);
            if (dl != null)
            {
                // Xóa người dùng
                context.UngTuyen.Remove(dl);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<UngTuyen> GetIdUser(int id)
        {
            var dl = await context.UngTuyen.FindAsync(id);
            if (dl != null)
            {
                return dl;
            }
            return null; // Or throw an exception, depending on your error handling preference
        }

        public async Task<List<UngTuyen>> GetUser()
        {
            return await context.UngTuyen.ToListAsync();
        }

        public async Task<bool> PostUser(UngTuyen user)
        {
            await context.UngTuyen.AddAsync(user);
            var result = await context.SaveChangesAsync();
            return result > 0; // True if at least one record was added
        }

        public async Task<bool> PutUser(int id, UngTuyen user)
        {
            var existingUser = await context.UngTuyen.FirstOrDefaultAsync(x => x.ung_tuyen_id == id);
            if (existingUser != null)
            {
                // Update fields
                existingUser.ngay_ung_tuyen = user.ngay_ung_tuyen;
                existingUser.chap_nhan_cong_viec = user.chap_nhan_cong_viec;
                existingUser.de_xuat= user.de_xuat;
                existingUser.trang_thai = user.trang_thai;
              
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
