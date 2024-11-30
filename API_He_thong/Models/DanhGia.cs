using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_He_thong.Models
{
    public class DanhGia
    {
        // Khóa chính (Primary Key)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DanhGiaId { get; set; }

        // Khóa ngoại liên kết đến bảng UngTuyen
        [Required]
        public int UngTuyenId { get; set; }

        // Navigation property để liên kết đến UngTuyen
        [ForeignKey("UngTuyenId")]
        public virtual UngTuyen UngTuyen { get; set; }

        // Nội dung đánh giá
        [MaxLength(500, ErrorMessage = "Nội dung đánh giá không được vượt quá 500 ký tự.")]
        [Required(ErrorMessage = "Nội dung đánh giá không được để trống.")]
        public string NoiDungDanhGia { get; set; }

        // Điểm đánh giá (giả sử điểm từ 1 đến 5)
        [Range(1, 5, ErrorMessage = "Điểm đánh giá phải nằm trong khoảng từ 1 đến 5.")]
        public int DiemDanhGia { get; set; }

        // Ngày đánh giá
        public DateTime NgayDanhGia { get; set; } = DateTime.Now;
        public int DoanhNghiepId { get; set; }
    }

}
