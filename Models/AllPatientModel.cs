﻿namespace ClinicManagementSystem.Models
{
    public class AllPatientModel
    {
        public int ID { get; set; }
        public string RecordId { get; set; }
        public string PatientId { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
       // public string PatientName { get; set; }
        public string Phone { get; set; }
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
        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string PresentComplaint { get; set; }
        public string PastHistory { get; set; }
        public string FamilyHistory { get; set; }
        public string PresentMedication { get; set; }
        public string PhysicalNature { get; set; }
        public string MentalNature { get; set; }
    }

    public class DeletePatientModel 
    {
        public int DocId { get; set; }
        public int RecordId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string PatientCreatedDate { get; set; }
    }

    public class ViewPatientDataModel 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string PresentComplaint { get; set; }
        public string PastHistory { get; set; }
        public string FamilyHistory { get; set; }
        public string PresentMedication { get; set; }
        public string PhysicalNature { get; set; }
        public string MentalNature { get; set; }
        public string CreatedAt { get; set; }
    }

    public class EditPatientDataModel
    {
        public int RecordId { get; set; }
        public int DocId { get; set; }
        public int PatientId { get; set; }                
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string PresentComplaint { get; set; }
        public string PastHistory { get; set; }
        public string FamilyHistory { get; set; }
        public string PresentMedication { get; set; }
        public string PhysicalNature { get; set; }
        public string MentalNature { get; set; }
    }

    
}
