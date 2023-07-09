using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Firstname can not be null")]
        public String FirstName { get; set; }
        public String LastName { get; set; }
        [Required(ErrorMessage = "Gender can not be null")]
        public String Gender { get; set; }
        [Required(ErrorMessage = "Date Of Birth can not be null")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Email Address can not be null")]
        public String EmailAddress { get; set; }
        [Required(ErrorMessage = "Phone Number can not be null")]
        public Int64 PhoneNumber { get; set; }
        [Required(ErrorMessage = "Aadhar Card Number can not be null")]
        public Int64 AadharCardNumber { get; set; }
        [Required(ErrorMessage = "Address can not be null")]
        public String Address { get; set; }
        [Required(ErrorMessage = "Specialization can not be null")]
        public String Specialization { get; set; }
        [Required(ErrorMessage = "Medical Liscense Number can not be null")]
        public String MedicalLiscenseNumber { get; set; }
        public String Qualification { get; set; }
        public String Experiance { get; set; }
        public String Affiliation { get; set; }
        public String LanguageSpoken { get; set; }
        [Required(ErrorMessage = "Username can not be null")]
        public String Username { get; set; }
        [Required(ErrorMessage = "Password can not be null")]
        public String Password { get; set; }
        [Required(ErrorMessage = "Profile Picture can not be Empty")]
        public String ProfilePicture { get; set; }
       

       
    }

    public class ResponseModel : RegistrationModel
    {
        public int DocId { get; set; }
    }

    public class SetSessionModel
    {
        public int DocId { get; set; }
        public String Firstname { get; set; }
        public String? Lastname { get; set; }
        public Int64 Mobilenumber { get; set; }
        public String? Email { get; set; }
        public String Address { get; set; }
        public String Gender { get; set; }
    }

    public class GetSessionModel
    {
        public int DocId { get; set; }
        public String? Firstname { get; set; }
        public String? Lastname { get; set; }
        public Int64 Mobilenumber { get; set; }
        public String? Email { get; set; }
        public String? Address { get; set; }
        public String? Gender { get; set; }
    }
}
