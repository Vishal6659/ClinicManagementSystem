using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username can not be null")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password can not be null")]
        public string Password { get; set; }
    }
}
