using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_He_thong.Models
{
    public class TaiKhoanNguoiDung
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int tai_khoan_id { get; set; }

        // Tên đăng nhập của người dùng
        [MaxLength(50, ErrorMessage = "Tên đăng nhập không được vượt quá 50 ký tự.")]
        [Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
        public string ten_dang_nhap { get; set; }

        // Mật khẩu của người dùng
        [MaxLength(50, ErrorMessage = "Mật khẩu không được vượt quá 50 ký tự.")]
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        public string mat_khau { get; set; }

        [MaxLength(50, ErrorMessage = "Roles không được vượt quá 50 ký tự.")]
        [Required(ErrorMessage = "Roles không được để trống.")]
        public string Roles { get; set; }
        // Khóa ngoại có thể được thêm nếu cần liên kết với bảng người dùng (NguoiDung)
        // public int nguoi_dung_id { get; set; }
        // [ForeignKey("nguoi_dung_id")]
        // public virtual NguoiDung NguoiDung { get; set; }
    }
}
