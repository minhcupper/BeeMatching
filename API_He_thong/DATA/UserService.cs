using API_He_thong.Models;
using Microsoft.EntityFrameworkCore;

namespace API_He_thong.DATA
{
    public class UserService : IUser
    {
        private readonly API_Context context;

        public UserService(API_Context context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var dl = await context.NguoiDung.FindAsync(id);
            if (dl != null)
            {
                // Xóa người dùng
                context.NguoiDung.Remove(dl);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<NguoiDung> GetIdUser(int id)
        {
            var dl = await context.NguoiDung.FindAsync(id);
            if (dl != null)
            {
                return dl;
            }
            return null; // Or throw an exception, depending on your error handling preference
        }

        public async Task<List<NguoiDung>> GetUser()
        {
            return await context.NguoiDung.ToListAsync();
        }

        public async Task<bool> PostUser(NguoiDung user)
        {
            await context.NguoiDung.AddAsync(user);
            var result = await context.SaveChangesAsync();
            return result > 0; // True if at least one record was added
        }

        public async Task<bool> PutUser(int id, NguoiDung user)
        {
            var existingUser = await context.NguoiDung.FirstOrDefaultAsync(x => x.nguoi_dung_id == id);
            if (existingUser != null)
            {
                // Update fields
               
                existingUser.ten_dang_nhap = user.ten_dang_nhap;
                existingUser.mat_khau = user.mat_khau;
                existingUser.Roles = user.Roles;
                existingUser.TrangThai = user.TrangThai;

                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}