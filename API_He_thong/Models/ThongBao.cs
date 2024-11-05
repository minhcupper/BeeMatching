using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_He_thong.Models
{
    public class ThongBao
    {
        // Khóa chính (Primary Key)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ThongBaoId { get; set; }

        // Foreign key for NguoiDung
        [Required] // Assuming each notification must be associated with a user
        public int NguoiDungId { get; set; }

        // Navigation property to NguoiDung
        [ForeignKey("NguoiDungId")]
        public virtual NguoiDung NguoiDung { get; set; }

        // Tiêu đề thông báo
        [MaxLength(100, ErrorMessage = "Tiêu đề không được vượt quá 100 ký tự.")]
        [Required(ErrorMessage = "Tiêu đề không được để trống.")]
        public string TieuDe { get; set; }

        // Nội dung thông báo
        [MaxLength(500, ErrorMessage = "Nội dung không được vượt quá 500 ký tự.")]
        [Required(ErrorMessage = "Nội dung không được để trống.")]
        public string NoiDung { get; set; }

        // Trạng thái thông báo (vd: đã đọc, chưa đọc)
        [MaxLength(15)]
        public string TrangThai { get; set; } = "Chưa đọc";

        // Ngày gửi thông báo
        public DateTime NgayGui { get; set; } = DateTime.Now;
    }
}
