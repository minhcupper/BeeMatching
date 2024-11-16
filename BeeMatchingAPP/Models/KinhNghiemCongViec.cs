using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeeMatchingAPP.Models
{
    public class KinhNghiemCongViec
    {
        // Composite key for kyNangId and CongViecId
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KinhNghiemId { get; set; }
        // Property to store the name of the skill
        public int CongViecId { get; set; }
        [Required]
        [MaxLength(100)]
        public string TenKinhNghiem { get; set; }

        // Optional description of the skill
        public string? MoTa { get; set; }

        // Foreign key for the skill category
        // Navigation property for CongViec
        [ForeignKey("CongViecId")]
        public virtual CongViec? CongViec { get; set; }
    }
}
