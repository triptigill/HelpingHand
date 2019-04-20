using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpingHand.Models
{
    public class ClientDetail
    {
        [Key]
        public Guid ClientId { get; set; }
        [Required]
        [Display(Name = "Customer Name")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Please Enter address")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Must be at least 8 characters long.")]
        [Display(Name = "Customer Address1")]
        public string Address1 { get; set; }

        [Required(ErrorMessage = "Please Enter address")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Must be at least 8 characters long.")]
        [Display(Name = "Client Address2")]
        public string Address2 { get; set; }

        [Required]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Mobile { get; set; }

        [Display(Name = " Client Email ")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter a valid e - mail adress")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
        public string Email { get; set; }
    }
}