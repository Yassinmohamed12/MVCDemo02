using System.ComponentModel.DataAnnotations;

namespace Company.web.Models
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password is Required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,}$", ErrorMessage = "The password must be at least 6 characters long and contain at least one lowercase letter, one uppercase letter, one digit, and one non-alphanumeric character.")]

        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required")]
        [Compare(nameof(Password), ErrorMessage = "Confirm Password does not match password")]
        public string ConfirmPassowrd { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }
    }
}
