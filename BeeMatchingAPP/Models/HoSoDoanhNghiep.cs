﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeeMatchingAPP.Models
{
    public class HoSoDoanhNghiep
    {
        // Khóa chính (Primary Key)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoanhNghiepId { get; set; }

        // Hình ảnh của doanh nghiệp (nullable)
        public string? HinhAnh { get; set; }

        // Khóa ngoại liên kết đến NguoiDung
        [Required] // Assuming each business must have an associated user
        public int NguoiDungId { get; set; }

        // Navigation property để liên kết đến NguoiDung
        [ForeignKey("NguoiDungId")]
        public virtual NguoiDung? NguoiDung { get; set; }

        // Tên công ty
        [MaxLength(100)]
        [Required(ErrorMessage = "Tên công ty không được để trống.")]
        public string TenCongTy { get; set; }

        // Mô tả công ty (có thể nullable)
        public string? MoTa { get; set; }
        // Email, bắt buộc, tối đa 100 ký tự, không được trống
        [MaxLength(100)]
        [Required(ErrorMessage = "Email không được để trống.")]
        public string email { get; set; }

        // Địa chỉ công ty (có thể nullable)
        [MaxLength(200)]
        public string? DiaChi { get; set; }

        // Navigation property cho các công việc của doanh nghiệp
        public virtual ICollection<CongViec>? CongViecs { get; set; } = new List<CongViec>();

        public string? DistrictId { get; set; } // Foreign key for districts
        public string? DistrictName { get; set; }
        public string? WardId { get; set; } // Foreign key for wards
        public string? WardName { get; set; }
        public string? ProvinceId { get; set; } // Foreign key for provinces
        public string? ProvinceName { get; set; }

        // Navigation properties cho địa chỉ
        [ForeignKey("DistrictId")]
        public virtual districts? Districts { get; set; }

        [ForeignKey("WardId")]
        public virtual wards? Wards { get; set; }

        [ForeignKey("ProvinceId")]
        public virtual provinces? Provinces { get; set; }
        public string? TrangThai { get; set; } = "Đang hoạt động ";

    }
}