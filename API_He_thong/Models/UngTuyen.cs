using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_He_thong.Models
{
    public class UngTuyen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ung_tuyen_id { get; set; }

        // Khóa ngoại kiểu int trỏ tới CongViec
        public int cong_viec_id { get; set; }

        // Navigation property cho CongViec
        [ForeignKey("cong_viec_id")]
        public virtual CongViec CongViec { get; set; }

        // Khóa ngoại kiểu int trỏ tới NguoiTimViec
        public int nguoi_tim_viec_id { get; set; }

        // Navigation property cho NguoiTimViec
        [ForeignKey("nguoi_tim_viec_id")]
        public virtual NguoiTimViec NguoiTimViec { get; set; }

        public DateTime ngay_ung_tuyen { get; set; } = DateTime.Now;

        public string de_xuat { get; set; }

        public string trang_thai { get; set; } = "Đang xem xét";

        public bool chap_nhan_cong_viec { get; set; } = false;

        // Navigation property cho DanhGia (quan hệ một-nhiều)
        public virtual ICollection<DanhGia> DanhGias { get; set; }
    }
}
