using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpingHand.Models
{
    public class Bill
    {
        [Key]
        public Guid BillId { get; set; }
        public Guid ClientId { get; set; }
        public ClientDetail clientDetail { get; set; }
       public Guid ProductId{get;set;}
        public ProductDetail productDetail { get; set; }

        [Required(ErrorMessage = "Please enter Invoice Number")]
        [Display(Name = "Invoice Number")]
        public int InvoiceNo { get; set; }

        [Required(ErrorMessage = "Please Enter Company Name")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Must be at least 4 characters long.")]
        public string Companyname { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Invoicedate { get; set; }

        [Required(ErrorMessage = "Please Enter Location")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Must be at least 4 characters long.")]
        public string Location { get; set; }

        [Range(1, 100)]
        [Required(ErrorMessage = "Please Enter Chalanno")]
        [Display(Name = "Chalan number")]
        public int Chalanno { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Must be at least 4 characters long.")]
        [Display(Name = "Customer Name")]
        public string Customername { get; set; }

        [Required(ErrorMessage = "Please Enter Address")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Must be at least 8 characters long.")]
        [Display(Name = "Customer Address1")]
        public string Customeraddress1 { get; set; }

        [Required(ErrorMessage = "Please Enter address")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Must be at least 8 characters long.")]
        [Display(Name = "Customer Address2")]
        public string Customeraddress2 { get; set; }

        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Please Enter Chequeno")]
        public int Totalamt { get; set; }

        [Required(ErrorMessage = "Please Enter Charges")]
        public int Charges { get; set; }

        [Required(ErrorMessage = "Please Enter Discount")]
        public int Discount { get; set; }

        [Required(ErrorMessage = "Please Enter GTAmt")]
        [Display(Name = "GT Amount")]
        public int GTAmt { get; set; }

        [Required(ErrorMessage = "Please Enter Serial")]
        public string Serial { get; set; }

        [Required(ErrorMessage = "Please Enter Chequeno")]
        [Display(Name = "Paid Type")]
        public string Paidtype { get; set; }

        [Required(ErrorMessage = "Please Enter Amtword")]
        [Display(Name = "Amount word")]
        public string Amtword { get; set; }

        public string Narration { get; set; }

        [Range(1, 100)]
        [Required(ErrorMessage = "Please Enter Chequeno")]
        [Display(Name = "Cheque number")]
        public string Chequeno { get; set; }

        [Required(ErrorMessage = "Please Enter Language")]
        public string Language { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Entrydate { get; set; }

        [Required(ErrorMessage = "Please Enter Discountper")]
        [Display(Name = "Discount per")]
        public int Discountper { get; set; }
    }
}