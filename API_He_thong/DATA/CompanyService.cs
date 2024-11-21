using API_He_thong.Models;
using Microsoft.EntityFrameworkCore;

namespace API_He_thong.DATA
{
    public class CompanyService:ICompany
    {
        private readonly API_Context context;

        public CompanyService(API_Context context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteCompany(int id)
        {
            var dl = await context.DoanhNghiep.FindAsync(id);
            if (dl != null)
            {
                // Xóa người dùng
                context.DoanhNghiep.Remove(dl);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<DoanhNghiep> GetIdCompany(int id)
        {
            var dl = await context.DoanhNghiep.FindAsync(id);
            if (dl != null)
            {
                return dl;
            }
            return null; // Or throw an exception, depending on your error handling preference
        }

        public async Task<List<DoanhNghiep>> GetCompany()
        {
            return await context.DoanhNghiep.ToListAsync();
        }

        public async Task<bool> PostCompany(DoanhNghiep user)
        {
            await context.DoanhNghiep.AddAsync(user);
            var result = await context.SaveChangesAsync();
            return result > 0; // True if at least one record was added
        }

        public async Task<bool> PutCompany(int id, DoanhNghiep user)
        {
            var existingUser = await context.DoanhNghiep.FirstOrDefaultAsync(x => x.DoanhNghiepId == id);
            if (existingUser != null)
            {
                // Update fields
                existingUser.WardId = user.WardId;
                existingUser.MoTa = user.MoTa;
                existingUser.DiaChi = user.DiaChi;
                existingUser.DistrictId = user.DistrictId;
                existingUser.ProvinceId = user.ProvinceId;
                existingUser.HinhAnh = user.HinhAnh;
                existingUser.TenCongTy = user.TenCongTy;
                existingUser.email = user.email;
                existingUser.TrangThai = user.TrangThai;
                
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
