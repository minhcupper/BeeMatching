using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_He_thong.Models
{
    public class CongViec
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cong_viec_id { get; set; }

        // Khóa ngoại liên kết đến bảng DoanhNghiep
        public int doanh_nghiep_id { get; set; }

        // Navigation property để liên kết đến DoanhNghiep
        [ForeignKey("doanh_nghiep_id")]
        public virtual DoanhNghiep DoanhNghiep { get; set; }

        // Tiêu đề công việc
        [MaxLength(200)]
        [Required(ErrorMessage = "Tiêu đề công việc không được để trống.")]
        public string tieu_de { get; set; }

        // Mô tả công việc
        public string mo_ta { get; set; }

        // Kỹ năng yêu cầu
        public string ky_nang_yeu_cau { get; set; }

        // Lương hàng tháng
        [Range(0, double.MaxValue, ErrorMessage = "Lương hàng tháng phải lớn hơn hoặc bằng 0.")]
        public decimal luong_hang_thang { get; set; }

        // Vị trí
        [MaxLength(100)]
        public string vi_tri { get; set; }

        // Địa điểm làm việc
        [MaxLength(200)]
        public string dia_diem_lam_viec { get; set; }

        // Trạng thái công việc
        [MaxLength(50)]
        [DefaultValue("Đang tuyển dụng")]
        public string trang_thai { get; set; } = "Đang tuyển dụng";

        // Ngày đăng tin
        [DefaultValue(typeof(DateTime), "")]
        public DateTime ngay_dang { get; set; } = DateTime.Now;

        // Hạn nộp hồ sơ
        public DateTime? han_nop_ho_so { get; set; }

        // Navigation property cho các ứng tuyển
        public virtual ICollection<UngTuyen> UngTuyens { get; set; }
    }
}
