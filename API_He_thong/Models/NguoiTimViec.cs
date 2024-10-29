using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_He_thong.Models
{
    public class NguoiTimViec
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int nguoi_tim_viec_id { get; set; }

        // Khóa ngoại trỏ đến bảng NguoiDung
        [Required]
        public int nguoi_dung_id { get; set; }

        // Navigation property trỏ tới NguoiDung
        [ForeignKey("nguoi_dung_id")]
        public virtual NguoiDung NguoiDung { get; set; }

        // Mô tả người tìm việc, bắt buộc, giới hạn tối đa 500 ký tự
        [Required(ErrorMessage = "Mô tả không được để trống.")]
        [MaxLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
        public string mo_ta { get; set; }

        // Ngôn ngữ, bắt buộc, giới hạn tối đa 200 ký tự
        [Required(ErrorMessage = "Ngôn ngữ không được để trống.")]
        [MaxLength(200, ErrorMessage = "Ngôn ngữ không được vượt quá 200 ký tự.")]
        public string ngon_ngu { get; set; }

        // Kinh nghiệm, không bắt buộc nhưng giới hạn tối đa 300 ký tự
        [MaxLength(300, ErrorMessage = "Kinh nghiệm không được vượt quá 300 ký tự.")]
        public string kinh_nghiem { get; set; }

        // Hoạt động ngoại khóa, không bắt buộc, giới hạn tối đa 300 ký tự
        [MaxLength(300, ErrorMessage = "Hoạt động ngoại khóa không được vượt quá 300 ký tự.")]
        public string hoat_dong_ngoai_khoa { get; set; }

        // Navigation property trỏ tới bảng KyNang (quan hệ một-nhiều)
        public virtual ICollection<KyNang> KyNangs { get; set; }

        // Navigation property trỏ tới bảng UngTuyen (quan hệ một-nhiều)
        public virtual ICollection<UngTuyen> UngTuyens { get; set; }
    }
}
