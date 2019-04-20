using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpingHand.Models
{
    public class BillDetails
    {
        [Key]
        public Guid BillDetailId { get; set; }
        //[Required(ErrorMessage = "Please enter ProdSno")]
        //[Display(Name = "Product Serial number")]
        //public int ProdSno { get; set; }
        public Guid BillId { get; set; }
        public Bill Bill { get; set; }

        [Required(ErrorMessage = " enter Quantity")]
        [Display(Name = "Quantity")]
        [Range(1, 100)]
        public int Qty { get; set; }

        [Required(ErrorMessage = "Please enter Rate")]
        public double Rate { get; set; }

        [Required(ErrorMessage = "Please enter Ammount")]
        public double Ammount { get; set; }

        [Required(ErrorMessage = "Please enter Barcode")]
          public string Barcode { get; set; }

        //[Required(ErrorMessage = "Please enter ProdSubType")]
        //[Display(Name = "Product Sub Type")]
        //[StringLength(50)]
        //public string ProdSubType { get; set; }
       
    }
}