using System.ComponentModel.DataAnnotations;

namespace BeeMatchingAPP.Models
{
    public class wards
    {
        // Mã phường/xã (Primary Key)
        [Key]
        [MaxLength(20)] // Giới hạn độ dài cho mã phường/xã
        public string code { get; set; }

        // Tên phường/xã
        [Required] // Trường bắt buộc, tương đương với NOT NULL
        [MaxLength(255)] // Giới hạn độ dài của tên phường/xã
        public string name { get; set; }

        // Tên tiếng Anh của phường/xã (có thể để trống)
        [MaxLength(255)] // Giới hạn độ dài của tên tiếng Anh
        public string name_en { get; set; }

        // Tên đầy đủ của phường/xã
        [MaxLength(255)] // Giới hạn độ dài của tên đầy đủ
        public string full_name { get; set; }

        // Tên đầy đủ bằng tiếng Anh (có thể để trống)
        [MaxLength(255)] // Giới hạn độ dài của tên đầy đủ bằng tiếng Anh
        public string full_name_en { get; set; }

        // Tên mã (có thể để trống)
        [MaxLength(255)] // Giới hạn độ dài của code name
        public string code_name { get; set; }

        // Mã quận/huyện (có thể để trống)
        [MaxLength(20)] // Giới hạn độ dài của mã quận/huyện
        public string district_code { get; set; }

        // ID đơn vị hành chính (có thể để trống)
        public int? administrative_unit_id { get; set; }
        // Navigation property to relate districts with NguoiDung
        public virtual ICollection<NguoiDung> NguoiDungs { get; set; } = new List<NguoiDung>();
        public virtual ICollection<CongViec> CongViecs { get; set; } = new List<CongViec>();
        public virtual ICollection<DoanhNghiep> doanhNghieps { get; set; } = new List<DoanhNghiep>();
    }
}
