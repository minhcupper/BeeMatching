using Microsoft.EntityFrameworkCore;
using API_He_thong.Models;

namespace API_He_thong.Repositories
{
    public class LoginRepository:ILoginRepository
    {
        private readonly API_Context _context;

        public LoginRepository(API_Context context)
        {
            _context = context;
        }

        public async Task<NguoiDung> GetUserAsync(string tai_khoan, string mat_khau)
        {
            return await _context.NguoiDung.FirstOrDefaultAsync(u => u.ten_dang_nhap == tai_khoan && u.mat_khau == mat_khau);
        }
    }
}
