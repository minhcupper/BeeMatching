using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_He_thong.Models
{
    public class PhanQuyen
    { // Khóa chính (Primary Key)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhanQuyenId { get; set; }

        // Loại người dùng, tối đa 20 ký tự
        [MaxLength(20, ErrorMessage = "Loại người dùng không được vượt quá 20 ký tự.")]
        [Required(ErrorMessage = "Loại người dùng không được để trống.")]
        public string LoaiNguoiDung { get; set; }

        // Navigation property cho quan hệ một-nhiều với NguoiDung
        public virtual ICollection<NguoiDung> NguoiDungs { get; set; } = new List<NguoiDung>();
    }
}