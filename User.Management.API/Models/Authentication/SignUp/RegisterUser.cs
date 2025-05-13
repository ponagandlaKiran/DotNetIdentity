using System.ComponentModel.DataAnnotations;

namespace User.Management.API.Models.Authentication.SignUp
{
    public class RegisterUser
    {
        [Required(ErrorMessage ="UserName Is Mandatory")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email Is Mandatory")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Mandatory")]
        public string Password { get; set; }
    }
}
