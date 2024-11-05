using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeeMatchingAPP.Models
{
    public class Company
    {
        public int doanh_nghiep_id { get; set; }
        public string? HinhAnh { get; set; }
   
        public int nguoi_dung_id { get; set; }

     


      
        public string ten_cong_ty { get; set; }

  
        public string mo_ta { get; set; }

      
        public string dia_chi { get; set; }

       
    }
}
