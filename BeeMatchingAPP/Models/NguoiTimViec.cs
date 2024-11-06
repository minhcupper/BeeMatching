using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeeMatchingAPP.Models
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

        // Job seeker description
        [Required(ErrorMessage = "Mô tả không được để trống.")]
        [MaxLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
        public string MoTa { get; set; }

        // Language skills
        [Required(ErrorMessage = "Ngôn ngữ không được để trống.")]
        [MaxLength(200, ErrorMessage = "Ngôn ngữ không được vượt quá 200 ký tự.")]
        public string NgonNgu { get; set; }

        // Experience (optional)
        [MaxLength(300, ErrorMessage = "Kinh nghiệm không được vượt quá 300 ký tự.")]
        public string? KinhNghiem { get; set; }

        // Extracurricular activities (optional)
        [MaxLength(300, ErrorMessage = "Hoạt động ngoại khóa không được vượt quá 300 ký tự.")]
        public string? HoatDongNgoaiKhoa { get; set; }

        // Navigation property to UngTuyen (one-to-many relationship)
        public virtual ICollection<UngTuyen> UngTuyens { get; set; } = new List<UngTuyen>();
    }
}
