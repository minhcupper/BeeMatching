using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeeMatchingAPP.Models
{
    public class NguoiDung
    {
        // Khóa chính (Primary Key)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int nguoi_dung_id { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
        public string? ten_dang_nhap { get; set; }


        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [DataType(DataType.Password)]
        public string mat_khau { get; set; }


        [DataType(DataType.Password)]
        [Compare("mat_khau", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp")]
        public string? ConfirmPassword { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Email không được để trống.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Roles không được để trống.")]
        public string? Roles { get; set; }

        [Required(ErrorMessage = "Otp không được để trống.")]
        public string? Otp { get; set; }

        public string? TrangThai { get; set; } = "Đang hoạt động";
        public DateTime? ngay_tao { get; set; } = DateTime.Now;

        // Navigation properties
        // Navigation properties for relationships with job seekers and businesses
        public virtual NguoiTimViec? NguoiTimViec { get; set; }
        public virtual DoanhNghiep? DoanhNghiep { get; set; }

        // Collection of notifications
        public virtual ICollection<ThongBao>? ThongBaos { get; set; } = new List<ThongBao>();

        // Navigation properties for address relationships
    }
}
