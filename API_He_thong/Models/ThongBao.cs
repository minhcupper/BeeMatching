using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_He_thong.Models
{
    public class ThongBao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int thong_bao_id { get; set; }

        public int nguoi_dung_id { get; set; }

        // Khóa ngoại liên kết với bảng NguoiDung
        [ForeignKey("nguoi_dung_id")]
        public virtual NguoiDung NguoiDung { get; set; }

        // Tiêu đề thông báo
        [MaxLength(100, ErrorMessage = "Tiêu đề không được vượt quá 100 ký tự.")]
        [Required(ErrorMessage = "Tiêu đề không được để trống.")]
        public string tieu_de { get; set; }

        // Nội dung thông báo
        [MaxLength(500, ErrorMessage = "Nội dung không được vượt quá 500 ký tự.")]
        [Required(ErrorMessage = "Nội dung không được để trống.")]
        public string noi_dung { get; set; }

        // Trạng thái thông báo (vd: đã đọc, chưa đọc)
        [MaxLength(15)]
        public string trang_thai { get; set; } = "Chưa đọc";

        // Ngày gửi thông báo
        public DateTime ngay_gui { get; set; } = DateTime.Now;
    }
}
