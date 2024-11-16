using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_He_thong.Models
{
    public class NguoiTimViec
    {
        // Primary Key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NguoiTimViecId { get; set; }

        // Profile image (optional)
        public string? HinhAnh { get; set; }

        // Foreign key to NguoiDung table
        [Required] // Assuming each job seeker must have an associated user
        public int NguoiDungId { get; set; }

        // Navigation property to NguoiDung
        [ForeignKey("NguoiDungId")]
        public virtual NguoiDung NguoiDung { get; set; }

        // Navigation property linking to KyNangNguoiXinViec
        public virtual ICollection<KyNangNguoiXinViec> KyNangNguoiXinViecs { get; set; } = new List<KyNangNguoiXinViec>();
        public virtual ICollection<KinhnghiemNguoiTimViec> KinhnghiemNguoiTimViecs { get; set; } = new List<KinhnghiemNguoiTimViec>();
        // Job seeker description
        [Required(ErrorMessage = "Mô tả không được để trống.")]
        [MaxLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
        public string MoTa { get; set; }

        // Language skills
        [Required(ErrorMessage = "Ngôn ngữ không được để trống.")]
        [MaxLength(200, ErrorMessage = "Ngôn ngữ không được vượt quá 200 ký tự.")]
        public string NgonNgu { get; set; }
        // Email, bắt buộc, tối đa 100 ký tự, không được trống
        [MaxLength(100)]
        [Required(ErrorMessage = "Email không được để trống.")]
        public string email { get; set; }
        // Experience (optional)
        [MaxLength(300, ErrorMessage = "Kinh nghiệm không được vượt quá 300 ký tự.")]
        public string? KinhNghiem { get; set; }
        // Họ tên, không bắt buộc, tối đa 100 ký tự
        [MaxLength(100)]
        public string? ho_ten { get; set; }

        // Ngày sinh, có thể null
        public DateTime? ngay_sinh { get; set; }

        // Giới tính, tối đa 10 ký tự
        [MaxLength(10)]
        public string? gioi_tinh { get; set; }

        // Số điện thoại, tối đa 15 ký tự
        [MaxLength(15)]
        public string? so_dien_thoai { get; set; }

        // Địa chỉ, tối đa 30 ký tự
        [MaxLength(30)]
        public string? dia_chi_nha { get; set; }
        public string? DistrictId { get; set; } // Foreign key for districts
        public string? WardId { get; set; } // Foreign key for wards
        public string? ProvinceId { get; set; } // Foreign key for provinces

        // Navigation properties cho địa chỉ
        [ForeignKey("DistrictId")]
        public virtual districts? Districts { get; set; }

        [ForeignKey("WardId")]
        public virtual wards? Wards { get; set; }

        [ForeignKey("ProvinceId")]
        public virtual provinces? Provinces { get; set; }

        // Loại người dùng, tối đa 15 ký tự
        // Trạng thái tài khoản, tối đa 15 ký tự, mặc định là "Hoạt động"
        [MaxLength(15)]
        public string trang_thai { get; set; } = "Hoạt động";

        // Ngày tạo tài khoản, mặc định là ngày hiện tại
        public DateTime? ngay_tao { get; set; } = DateTime.Now;

        // Extracurricular activities (optional)
        [MaxLength(300, ErrorMessage = "Hoạt động ngoại khóa không được vượt quá 300 ký tự.")]
        public string? HoatDongNgoaiKhoa { get; set; }

        // Navigation property to UngTuyen (one-to-many relationship)
        public virtual ICollection<UngTuyen> UngTuyens { get; set; } = new List<UngTuyen>();
        
    }
}
