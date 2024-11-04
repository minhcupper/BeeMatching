using System.ComponentModel.DataAnnotations;

namespace BeeMatchingAPP.Models
{
    public class User
    {
        //Tuan cong
        //Cong tuan
        //cong tuan nguyen
        [Key]
        public int nguoi_dung_id { get; set; }
        [MaxLength(20)]
        public string ten_dang_nhap { get; set; }
        [MaxLength(20)]
        public string mat_khau { get; set; }
        [MaxLength(20)]
        public string email { get; set; }
        [MaxLength(20)]
        public string ho_ten { get; set; }
        DateTime ngay_sinh;
        public string gioi_tinh { get; set; }
        [MaxLength(11)]
        public string so_dien_thoai { get; set; }
        [MaxLength(20)]
        public string dia_chi { get; set; }
        public string loai_nguoi_dung { get; set; }
        [MaxLength(20)]
        public string trang_thai { get; set; }
        DateTime ngay_tao { get; set; }
    }
}
