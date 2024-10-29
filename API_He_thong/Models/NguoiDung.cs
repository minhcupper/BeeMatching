using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_He_thong.Models
{
    public class NguoiDung
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int nguoi_dung_id { get; set; }

        // Tên đăng nhập, bắt buộc, tối đa 50 ký tự, không được trống
        [MaxLength(50)]
        [Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
        public string ten_dang_nhap { get; set; }

        // Email, bắt buộc, tối đa 100 ký tự, không được trống
        [MaxLength(100)]
        [Required(ErrorMessage = "Email không được để trống.")]
        public string email { get; set; }

        // Mật khẩu, bắt buộc, tối đa 10 ký tự, không được trống
        [MaxLength(10)]
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        public string mat_khau { get; set; }

        // Họ tên, không bắt buộc, tối đa 100 ký tự
        [MaxLength(100)]
        public string ho_ten { get; set; }

        // Ngày sinh, có thể null
        public DateTime? ngay_sinh { get; set; }

        // Giới tính, tối đa 10 ký tự
        [MaxLength(10)]
        public string gioi_tinh { get; set; }

        // Số điện thoại, tối đa 15 ký tự
        [MaxLength(15)]
        public string so_dien_thoai { get; set; }

        // Địa chỉ, tối đa 30 ký tự
        [MaxLength(30)]
        public string dia_chi { get; set; }

        // Loại người dùng, tối đa 15 ký tự (ví dụ: "NguoiTimViec", "DoanhNghiep")
        [MaxLength(15)]
        public string loai_nguoi_dung { get; set; }

        // Trạng thái tài khoản, tối đa 15 ký tự, mặc định là "Hoạt động"
        [MaxLength(15)]
        public string trang_thai { get; set; } = "Hoạt động";

        // Ngày tạo tài khoản, mặc định là ngày hiện tại
        public DateTime ngay_tao { get; set; } = DateTime.Now;

        // Navigation properties

        // Quan hệ 1-1 với NguoiTimViec
        public virtual NguoiTimViec NguoiTimViec { get; set; }

        // Quan hệ 1-1 với DoanhNghiep
        public virtual DoanhNghiep DoanhNghiep { get; set; }

        // Quan hệ 1-nhiều với ThongBao (1 người dùng có nhiều thông báo)
        public virtual ICollection<ThongBao> ThongBaos { get; set; }
    }
}
