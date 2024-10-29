using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_He_thong.Models
{
    public class DanhGia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int danh_gia_id { get; set; }

        // Khóa ngoại liên kết đến bảng UngTuyen
        public int ung_tuyen_id { get; set; }

        // Navigation property để liên kết đến UngTuyen
        [ForeignKey("ung_tuyen_id")]
        public virtual UngTuyen UngTuyen { get; set; }

        // Nội dung đánh giá
        [MaxLength(500)]
        [Required(ErrorMessage = "Nội dung đánh giá không được để trống.")]
        public string noi_dung_danh_gia { get; set; }

        // Điểm đánh giá (giả sử điểm từ 1 đến 5)
        [Range(1, 5, ErrorMessage = "Điểm đánh giá phải nằm trong khoảng từ 1 đến 5.")]
        public int diem_danh_gia { get; set; }

        // Ngày đánh giá
        public DateTime ngay_danh_gia { get; set; } = DateTime.Now;
    }
}
