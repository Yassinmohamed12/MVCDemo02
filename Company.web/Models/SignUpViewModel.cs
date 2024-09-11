using System.ComponentModel.DataAnnotations;

namespace Company.web.Models
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "First Name Is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Format of Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,}$", ErrorMessage = "The password must be at least 6 characters long and contain at least one lowercase letter, one uppercase letter, one digit, and one non-alphanumeric character.")]

        public string Password { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [Compare(nameof(Password),ErrorMessage ="Confirm Password does not match password")]
        public string ConfirmPassowrd { get; set; }

        [Required(ErrorMessage = "Required to Agree")]
        public bool IsAgree { get; set; }
    }
}
