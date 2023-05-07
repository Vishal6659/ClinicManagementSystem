using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Firstname can not be null")]

        public String Firstame { get; set; }
        public String? Middlename { get; set; }
        public String? Lastname { get; set; }
        [Required(ErrorMessage = "Age can not be null")]

        public string Age { get; set; }
        [Required(ErrorMessage = "Mobile number can not be null")]

        public string Mobilenumber { get; set; }
        public String? Email { get; set; }
        [Required(ErrorMessage = "Address can not be null")]

        public String Address { get; set; }
        public String? Officaname { get; set; }
        [Required(ErrorMessage = "Landmark can not be null")]

        public String Landmark { get; set; }
        [Required(ErrorMessage = "City can not be null")]

        public String City { get; set; }
        [Required(ErrorMessage = "State can not be null")]

        public String State { get; set; }
        public String? Country { get; set; }
        [Required(ErrorMessage = "Gender can not be null")]

        public String Gender { get; set; }
        [Required(ErrorMessage = "Username can not be null")]

        public String Username { get; set; }
        [Required(ErrorMessage = "Password can not be null")]

        public String Password { get; set; }
        [Required(ErrorMessage = "Confirm Password can not be null")]

        public String Confirmpassword { get; set; }
    }

    public class ResponseModel : RegistrationModel
    {
        public int Id { get; set; }
    }

   /* public class SetSessionModel 
    {
        public String Firstame { get; set; }
        public String? Lastname { get; set; }
        public string Mobilenumber { get; set; }
        public String? Email { get; set; }
        public String Address { get; set; }
        public String? Officaname { get; set; }
        public String City { get; set; }
        public String Gender { get; set; }
        public String Username { get; set; }
    }

    public class GetSessionModel
    {
        public String Firstame { get; set; }
        public String? Lastname { get; set; }
        public string Mobilenumber { get; set; }
        public String? Email { get; set; }
        public String Address { get; set; }
        public String? Officaname { get; set; }
        public String City { get; set; }
        public String Gender { get; set; }
        public String Username { get; set; }
    }*/
}
