using System.ComponentModel.DataAnnotations;

namespace Company.Moustafa.PL.Dtos
{
    public class SignInDot
    {

        [Required(ErrorMessage = "Email is Required !!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required !!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RemeberMe { get; set; }

    }
}
