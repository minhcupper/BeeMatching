using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_He_thong.Models
{
    public class DoanhNghiep
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int doanh_nghiep_id { get; set; }

        // Khóa ngoại liên kết đến NguoiDung
        public int nguoi_dung_id { get; set; }

        // Navigation property để liên kết đến NguoiDung
        [ForeignKey("nguoi_dung_id")]
        public virtual NguoiDung NguoiDung { get; set; }

        // Tên công ty
        [MaxLength(100)]
        [Required(ErrorMessage = "Tên công ty không được để trống.")]
        public string ten_cong_ty { get; set; }

        // Mô tả công ty
        public string mo_ta { get; set; }

        // Địa chỉ công ty
        [MaxLength(200)]
        public string dia_chi { get; set; }

        // Navigation property cho các công việc của doanh nghiệp
        public virtual ICollection<CongViec> CongViecs { get; set; } = new List<CongViec>();
    }
}
