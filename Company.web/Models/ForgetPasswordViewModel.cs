using System.ComponentModel.DataAnnotations;

namespace Company.web.Models
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage ="Invalid Format")]
        public string Email { get; set; }
    }
}
