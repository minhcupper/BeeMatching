using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeeMatchingAPP.Models
{
    public class DanhMucKyNang
    {
        // Khóa chính (Primary Key)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DanhMucKyNangId { get; set; }

        // Tên danh mục kỹ năng
        [MaxLength(50)]
        [Required(ErrorMessage = "Tên danh mục không được để trống.")]
        public string TenDanhMuc { get; set; }

        // Mô tả danh mục kỹ năng
        [MaxLength(255)]
        public string? MoTa { get; set; } // Made nullable to allow for empty descriptions

        // Navigation property: Liên kết với bảng KyNangNguoiXinViec
        public virtual ICollection<KyNangNguoiXinViec> KyNangNguoiXinViecs { get; set; } = new List<KyNangNguoiXinViec>();

        // Navigation property: Liên kết với bảng KyNangCongViec
        public virtual ICollection<KyNangCongViec> KyNangCongViecs { get; set; } = new List<KyNangCongViec>();
    }
}
