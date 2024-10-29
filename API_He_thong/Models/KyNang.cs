using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_He_thong.Models
{
    public class KyNang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ky_nang_id { get; set; }

        // Khóa ngoại trỏ tới NguoiTimViec
        public int nguoi_tim_viec_id { get; set; }

        // Khóa ngoại trỏ tới DanhMucKyNang
        public int danh_muc_ky_nang_id { get; set; }

        // Thiết lập khóa ngoại và liên kết tới bảng NguoiTimViec
        [ForeignKey("nguoi_tim_viec_id")]
        public virtual NguoiTimViec NguoiTimViec { get; set; }

        // Thiết lập khóa ngoại và liên kết tới bảng DanhMucKyNang
        [ForeignKey("danh_muc_ky_nang_id")]
        public virtual DanhMucKyNang DanhMucKyNang { get; set; }

        // Tên kỹ năng
        [MaxLength(50)]
        [Required(ErrorMessage = "Tên kỹ năng không được để trống.")]
        public string ten_ky_nang { get; set; }

        // Mô tả kỹ năng
        [MaxLength(255)]
        public string mo_ta { get; set; }
    }
}
