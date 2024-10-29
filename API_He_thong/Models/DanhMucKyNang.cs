using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_He_thong.Models
{
    public class DanhMucKyNang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int danh_muc_ky_nang_id { get; set; }

        // Tên danh mục kỹ năng
        [MaxLength(50)]
        [Required(ErrorMessage = "Tên danh mục không được để trống.")]
        public string ten_danh_muc { get; set; }

        // Mô tả danh mục kỹ năng
        [MaxLength(255)]
        public string mo_ta { get; set; }

        // Navigation property: Liên kết với bảng KyNang
        public virtual ICollection<KyNang> KyNangs { get; set; }
    }
}
