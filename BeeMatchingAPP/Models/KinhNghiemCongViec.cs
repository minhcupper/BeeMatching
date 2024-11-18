using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeeMatchingAPP.Models
{
    public class KinhNghiemCongViec
    {
        // Khóa chính
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KinhNghiemId { get; set; }

        // Khóa ngoại liên kết đến công việc
        [Required]
        public int CongViecId { get; set; }

        // Tên yêu cầu kinh nghiệm
        [Required(ErrorMessage = "Tên yêu cầu kinh nghiệm không được để trống.")]
        [MaxLength(100)]
        public string TenKinhNghiem { get; set; }

        // Mô tả chi tiết về yêu cầu kinh nghiệm
        [MaxLength(500)]
        public string? MoTa { get; set; }

        // Mức độ kinh nghiệm yêu cầu (số năm)
        [Required(ErrorMessage = "Mức độ kinh nghiệm yêu cầu không được để trống.")]
        [Range(0, 50, ErrorMessage = "Số năm kinh nghiệm phải từ 0 đến 50.")]
        public int SoNamKinhNghiem { get; set; }

        // Navigation property để liên kết đến bảng CongViec
        [ForeignKey("CongViecId")]
        public virtual CongViec CongViec { get; set; }
    }
}
