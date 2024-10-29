using System.ComponentModel.DataAnnotations;

namespace API_He_thong.Models
{
    public class districts 
    {
        // Mã quận/huyện (Primary Key)
        [Key]
        [MaxLength(20)] // Giới hạn độ dài cho mã quận/huyện
        public string code { get; set; }

        // Tên quận/huyện
        [Required] // Trường bắt buộc, tương đương với NOT NULL
        [MaxLength(255)] // Giới hạn độ dài của tên
        public string name { get; set; }

        // Tên tiếng Anh của quận/huyện (có thể để trống)
        [MaxLength(255)] // Giới hạn độ dài của tên tiếng Anh
        public string name_en { get; set; }

        // Tên đầy đủ của quận/huyện (có thể để trống)
        [MaxLength(255)] // Giới hạn độ dài của tên đầy đủ
        public string full_name { get; set; }

        // Tên đầy đủ bằng tiếng Anh (có thể để trống)
        [MaxLength(255)] // Giới hạn độ dài của tên đầy đủ bằng tiếng Anh
        public string full_name_en { get; set; }

        // Tên mã (có thể để trống)
        [MaxLength(255)] // Giới hạn độ dài của mã code name
        public string code_name { get; set; }

        // Mã tỉnh/thành phố (có thể để trống)
        [MaxLength(20)] // Giới hạn độ dài của mã tỉnh/thành phố
        public string province_code { get; set; }

        // ID đơn vị hành chính (có thể để trống)
        public int? administrative_unit_Id { get; set; }
    }
}
