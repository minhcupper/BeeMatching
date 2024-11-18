using System.ComponentModel.DataAnnotations;

namespace API_He_thong.Models
{
    public class wards
    {
        [Key]
        [MaxLength(20)] // Giới hạn độ dài cho mã tỉnh/thành phố
        public string code { get; set; }

        // Tên tỉnh/thành phố
        [Required] // Trường bắt buộc, tương đương với NOT NULL
        [MaxLength(255)] // Giới hạn độ dài của tên
        public string name { get; set; }

        // Tên tiếng Anh của tỉnh/thành phố (có thể để trống)
        [MaxLength(255)] // Giới hạn độ dài của tên tiếng Anh
        public string name_en { get; set; }

        // Tên đầy đủ của tỉnh/thành phố
        [Required] // Trường bắt buộc
        [MaxLength(255)] // Giới hạn độ dài của tên đầy đủ
        public string full_name { get; set; }

        // Tên đầy đủ bằng tiếng Anh (có thể để trống)
        [MaxLength(255)] // Giới hạn độ dài của tên đầy đủ bằng tiếng Anh
        public string full_name_en { get; set; }

        // Tên mã (có thể để trống)
        [MaxLength(255)] // Giới hạn độ dài của mã code name
        public string code_name { get; set; }

        // ID đơn vị hành chính (có thể để trống)
        public int? administrative_unit_id { get; set; }

        // ID vùng hành chính (có thể để trống)
        public int? administrative_region_Id { get; set; }
        // Navigation property to relate districts with NguoiDung
        public virtual ICollection<NguoiTimViec> NguoiTimViecs { get; set; } = new List<NguoiTimViec>();
        public virtual ICollection<CongViec> CongViecs { get; set; } = new List<CongViec>();
        public virtual ICollection<DoanhNghiep> doanhNghieps { get; set; } = new List<DoanhNghiep>();
    }
}
