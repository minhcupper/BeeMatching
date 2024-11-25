using API_He_thong.Models;
using Microsoft.EntityFrameworkCore;

namespace API_He_thong.DATA
{
    public class NguoiXinViecService : INguoiXinViec
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
            var existingUser = await context.NguoiTimViec.FirstOrDefaultAsync(x => x.NguoiTimViecId == id);
            if (existingUser != null)
            {
                // Update fields
                existingUser.ho_ten = user.ho_ten;
                existingUser.so_dien_thoai = user.so_dien_thoai;
                existingUser.email = user.email;
                existingUser.ngay_sinh = user.ngay_sinh;
                existingUser.MoTa = user.MoTa;
                existingUser.WardId = user.WardId;
                existingUser.DistrictId = user.DistrictId;
                existingUser.ProvinceId = user.ProvinceId;
                existingUser.NgonNgu = user.NgonNgu;
                existingUser.HinhAnh = user.HinhAnh;
                existingUser.KinhNghiem = user.KinhNghiem;
                existingUser.HoatDongNgoaiKhoa = user.HoatDongNgoaiKhoa;
                existingUser.KyNangNguoiXinViecs = user.KyNangNguoiXinViecs;



                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
