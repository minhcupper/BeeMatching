using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeeMatchingAPP.Models
{
    public class KyNangNguoiXinViec
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KyNangId { get; set; }

        // Skill name
        [MaxLength(100)]
        [Required(ErrorMessage = "Tên kỹ năng không được để trống.")]
        public string TenKyNang { get; set; }

        // Skill description (nullable)
        public string? MoTa { get; set; }

        // Foreign key pointing to NguoiTimViec
        [Required] // Assuming each skill must belong to a job seeker
        public int NguoiTimViecId { get; set; }

        // Navigation property linking to NguoiTimViec
        [ForeignKey("NguoiTimViecId")]
        public virtual NguoiTimViec NguoiTimViec { get; set; }

        // Foreign key pointing to DanhMucKyNang
        [Required] // Assuming each skill must belong to a skill category
        public int DanhMucKyNangId { get; set; }

        // Navigation property linking to DanhMucKyNang
        [ForeignKey("DanhMucKyNangId")]
        public virtual DanhMucKyNang DanhMucKyNang { get; set; }
    }
}
