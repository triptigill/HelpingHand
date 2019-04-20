using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpingHand.Models
{
    public class ProductDetail
    {
        [Key]
        public Guid ProductId { get; set; }
        [Required]
        public string Title { get; set; }
        //[Required]
        //[Display(Name = "Book Sub Type")]
        //public string  BookSubType { get; set; }

        [Required]
        [Display(Name = "Book Type")]
        public string Booktype { get; set; }

        [Required(ErrorMessage = "Please enter Group")]
        public string Group { get; set; }
      
        [Required(ErrorMessage = "Please enter Writer")]
        public string Writer { get; set; }

        [Required(ErrorMessage = "Please enter Barcode")]
        [StringLength(50)]
        public string ProductCode { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string  Description{ get; set; }

        //public virtual Bill_details Bill_details { get; set; }
        //public int ProdSno { get; set; }

    }
    }