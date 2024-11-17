using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_He_thong.Models
{
    public class UngTuyen
    {
        // Khóa chính (Primary Key)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UngTuyenId { get; set; }

        // Khóa ngoại kiểu int trỏ tới CongViec
        [Required(ErrorMessage = "CongViecId không được để trống.")]
        public int CongViecId { get; set; }

        // Navigation property cho CongViec
        [ForeignKey("CongViecId")]
        public virtual CongViec CongViec { get; set; }

        // Khóa ngoại kiểu int trỏ tới NguoiTimViec
        [Required(ErrorMessage = "NguoiTimViecId không được để trống.")]
        public int NguoiTimViecId { get; set; }

        // Navigation property cho NguoiTimViec
        [ForeignKey("NguoiTimViecId")]
        public virtual NguoiTimViec NguoiTimViec { get; set; }

        // Ngày ứng tuyển
        public DateTime NgayUngTuyen { get; set; } = DateTime.Now;

        // Đề xuất của người ứng tuyển (có thể nullable)
        [MaxLength(500, ErrorMessage = "Đề xuất không được vượt quá 500 ký tự.")]
        public string? DeXuat { get; set; }

        // Trạng thái ứng tuyển (vd: đang xem xét, đã chấp nhận, từ chối)
        [MaxLength(20)]
        public string TrangThai { get; set; } = "Đang xem xét";

        // Chấp nhận công việc hay không
        public bool ChapNhanCongViec { get; set; } = false;

        // Navigation property cho DanhGia (quan hệ một-nhiều)
        public virtual ICollection<DanhGia> DanhGias { get; set; } = new List<DanhGia>();
    }
}
