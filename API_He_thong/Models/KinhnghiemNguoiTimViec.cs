using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_He_thong.Models
{
    public class KinhnghiemNguoiTimViec
    {
        // Khóa chính
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KinhNghiemId { get; set; }

        // Thuộc tính liên kết đến NguoiTimViec (Foreign Key)
        [Required]
        public int NguoiTimViecId { get; set; }

        // Tên kinh nghiệm
        [Required(ErrorMessage = "Tên kinh nghiệm không được để trống.")]
        [MaxLength(100)]
        public string TenKinhNghiem { get; set; }

        // Mô tả kinh nghiệm (nullable)
        public string? MoTa { get; set; }

        // Navigation property liên kết với NguoiTimViec
        [ForeignKey("NguoiTimViecId")]
        public virtual NguoiTimViec NguoiTimViec { get; set; }

        // Ngày bắt đầu của kinh nghiệm
        [Required]
        public DateTime NgayBatDau { get; set; }

        // Ngày kết thúc của kinh nghiệm (nullable nếu kinh nghiệm hiện tại vẫn đang tiếp tục)
        public DateTime? NgayKetThuc { get; set; }

        // Tên công ty hoặc nơi làm việc
        [MaxLength(150)]
        public string? CongTy { get; set; }
    }
}

