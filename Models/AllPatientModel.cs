namespace ClinicManagementSystem.Models
{
    public class AllPatientModel
    {
        public int ID { get; set; }
        public string PatientName { get; set; }
        public Int64 Phone { get; set; }
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
        public int DocID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public Int64 Phone { get; set; }
        public string Birthday { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public string Address { get; set; }
        public string PatientWeight { get; set; }
        public string PatientHeight { get; set; }
    }
}
