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



        // Tên đăng nhập, bắt buộc, tối đa 50 ký tự, không được trống
        [MaxLength(50)]
        //[Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
        public string? ten_dang_nhap { get; set; }


        [MaxLength(50)]
        [Required(ErrorMessage = "Email không được để trống.")]
        public string Email { get; set; }

        // Mật khẩu, bắt buộc, tối đa 10 ký tự, không được trống
        [MaxLength(10)]
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        public string mat_khau { get; set; }
        [MaxLength(10)]
        [Required(ErrorMessage = "Roles không được để trống.")]
        public string? Roles { get; set; }
        [MaxLength(10)]
        [Required(ErrorMessage = "Trang thai không được để trống.")]
        [DefaultValue("Đang hoạt động")]
        public string? TrangThai { get; set; } = "Đang hoạt động ";

        // Navigation properties
        // Navigation properties for relationships with job seekers and businesses
        public virtual NguoiTimViec? NguoiTimViec { get; set; }
        public virtual DoanhNghiep? DoanhNghiep { get; set; }

        // Collection of notifications
        public virtual ICollection<ThongBao>? ThongBaos { get; set; } = new List<ThongBao>();

        // Navigation properties for address relationships

    }
}
