using API_He_thong.Models;
using Microsoft.EntityFrameworkCore;

namespace API_He_thong.DATA
{
    public class DanhMucKyNangService:IDanhMucKyNang
    {
        private readonly API_Context context;

        public DanhMucKyNangService(API_Context context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteDanhMuc(int id)
        {
            var dl = await context.DanhMucKyNang.FindAsync(id);
            if (dl != null)
            {
                // Xóa người dùng
                context.DanhMucKyNang.Remove(dl);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<DanhMucKyNang> GetIdDanhMuc(int id)
        {
            var dl = await context.DanhMucKyNang.FindAsync(id);
            if (dl != null)
            {
                return dl;
            }
            return null; // Or throw an exception, depending on your error handling preference
        }

        public async Task<List<DanhMucKyNang>> GetDanhMuc()
        {
            return await context.DanhMucKyNang.ToListAsync();
        }

        public async Task<bool> PostDanhMuc(DanhMucKyNang user)
        {
            await context.DanhMucKyNang.AddAsync(user);
            var result = await context.SaveChangesAsync();
            return result > 0; // True if at least one record was added
        }

        public async Task<bool> PutDanhMuc(int id, DanhMucKyNang user)
        {
            var existingUser = await context.DanhMucKyNang.FirstOrDefaultAsync(x => x.DanhMucKyNangId== id);
            if (existingUser != null)
            {
                // Update fields
                existingUser.TenDanhMuc = user.TenDanhMuc;
                existingUser.MoTa = user.MoTa;
             

                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
