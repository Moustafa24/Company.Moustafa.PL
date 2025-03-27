using System.ComponentModel.DataAnnotations;

namespace Company.Moustafa.PL.Dtos
{
    public class ForgetPasswordDto
    {

        [Required(ErrorMessage = "Email is Required !!")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
