using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpingHand.Models
{
    public class CompanyDetail
    {
        [Key]
        public Guid CompanyId { get; set; }

        [Required]
        [Display(Name ="Company Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Please enter address")]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public int PIN { get; set; }

        [Required]
        public string Telephone { get; set; }

        [Required]
        public int VAT { get; set; }
    }
}