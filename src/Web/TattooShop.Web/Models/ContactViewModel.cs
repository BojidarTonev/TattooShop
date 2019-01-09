using System.ComponentModel.DataAnnotations;

namespace TattooShop.Web.Models
{
    public class ContactViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required(ErrorMessage = "Provide us with an e-mail so we can contact back to you")]
        [EmailAddress]
        public string SenderEmail { get; set; }

        [Phone]
        public string SenderPhoneNumber { get; set; }

        [Required(ErrorMessage = "Trying to spam?")]
        [StringLength(700, ErrorMessage = "Enough is enough.")]
        public string Message { get; set; }
    }
}
