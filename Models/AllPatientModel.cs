namespace ClinicManagementSystem.Models
{
    public class AllPatientModel
    {
        public string ID { get; set; }
        public string PatientName { get; set; }
        public string Phone { get; set; }
        public string BloodGroup { get; set; }
        public string Date { get; set; }
        public string Actions { get; set; }
       
    }

    public class NewPatient 
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Birthday { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public string Address { get; set; }
        public string PatientWeight { get; set; }
        public string PatientHeight { get; set; }
    }
}
