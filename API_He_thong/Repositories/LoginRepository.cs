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

        public async Task<NguoiDung> GetUserAsync(string tai_khoan)
        {
            return await _context.NguoiDung.FirstOrDefaultAsync(u => u.Email == tai_khoan );
        }
        public async Task<List<string>> GetUserRoleAsync(int nguoi_dung_id)
        {
            // Retrieve the user by their ID
            var user = await _context.NguoiDung
                                      .FirstOrDefaultAsync(u => u.nguoi_dung_id == nguoi_dung_id);

            // Check if user exists and roles field is not empty
            if (user != null && !string.IsNullOrEmpty(user.Roles))
            {
                // Split the Roles string by commas (assuming roles are comma-separated)
                var roles = user.Roles.Split(',').ToList();
                return roles;
            }

            // Return an empty list if no roles found
            return new List<string>();
        }
    }
}
