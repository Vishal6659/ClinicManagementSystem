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
    public class AllPatientModelVM 
    {
        public List<AllPatientModel> patientModelList { get; set; } = new List<AllPatientModel>();
    }

    public class NewPatient 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Int32 Phone { get; set; }
        public string Birthday { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public string Address { get; set; }
        public int PatientWeight { get; set; }
        public int PatientHeight { get; set; }
    }
}
