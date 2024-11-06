using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeeMatchingAPP.Models
{
    public class CongViec
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CongViecId { get; set; }

        // Khóa ngoại liên kết đến bảng DoanhNghiep
        [Required]
        public int DoanhNghiepId { get; set; }

        // Navigation property để liên kết đến DoanhNghiep
        [ForeignKey("DoanhNghiepId")]
        public virtual DoanhNghiep DoanhNghiep { get; set; }

        // Tiêu đề công việc
        [MaxLength(200)]
        [Required(ErrorMessage = "Tiêu đề công việc không được để trống.")]
        public string TieuDe { get; set; }

        // Mô tả công việc (nullable)
        public string? MoTa { get; set; }

        // Lương hàng tháng
        [Range(0, double.MaxValue, ErrorMessage = "Lương hàng tháng phải lớn hơn hoặc bằng 0.")]
        public decimal LuongHangThang { get; set; }

        // Vị trí
        [MaxLength(100)]
        public string? ViTri { get; set; }

        // Trạng thái công việc
        [MaxLength(50)]
        [DefaultValue("Đang tuyển dụng")]
        public string TrangThai { get; set; } = "Đang tuyển dụng";

        // Ngày đăng tin
        public DateTime NgayDang { get; set; } = DateTime.Now;

        // Hạn nộp hồ sơ
        public DateTime? HanNopHoSo { get; set; }

        // Navigation property cho các ứng tuyển
        public virtual ICollection<UngTuyen> UngTuyens { get; set; } = new List<UngTuyen>();

        // Navigation properties cho các địa điểm
        // Navigation properties for address relationships
        public string? DistrictId { get; set; } // Foreign key for districts
        public string? WardId { get; set; } // Foreign key for wards
        public string? ProvinceId { get; set; } // Foreign key for provinces

        // Navigation properties cho địa chỉ
        [ForeignKey("DistrictId")]
        public virtual districts Districts { get; set; }

        [ForeignKey("WardId")]
        public virtual wards Wards { get; set; }

        [ForeignKey("ProvinceId")]
        public virtual provinces Provinces { get; set; }
        // Collection for skills related to the job
        public virtual ICollection<KyNangCongViec> KyNangCongViecs { get; set; } = new List<KyNangCongViec>();
    }
}
