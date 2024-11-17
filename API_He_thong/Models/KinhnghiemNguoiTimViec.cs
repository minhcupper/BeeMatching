using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_He_thong.Models
{
    public class KinhnghiemNguoiTimViec
    {
        // Composite key for kyNangId and CongViecId
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KinhNghiemId { get; set; }
        // Property to store the name of the skill
        public int NguoiTimViecId  { get; set; }
        [Required]
        [MaxLength(100)]
        public string TenKinhNghiem { get; set; }

        // Optional description of the skill
        public string? MoTa { get; set; }

    // Foreign key for the skill category
    // Navigation property for CongViec
    [ForeignKey("NguoiTimViecId")]
    public virtual NguoiTimViec NguoiTimViec { get; set; }

}
}
