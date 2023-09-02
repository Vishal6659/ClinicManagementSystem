namespace ClinicManagementSystem.Models
{
    public class AllPrescriptionModel
    {
        public int Id { get; set; }
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public string DrugName { get; set; }
        public string DrugAdviceOrComments { get; set; }
        public string TestName { get; set; }
        public string TestDescription { get; set; }

    }
    public class AllPrescriptionModelVM 
    {
        public List<AllPrescriptionModel> allPrescriptionModelList { get; set; } = new List<AllPrescriptionModel>();
    }

    public class NewPrescriptionModel
    {
        public int PatientId { get; set; }
        public int DocId { get; set; }
        public string PatientName { get; set; }
        public string DrugType { get; set; }
        public string DrugName { get; set; }
        public string DrugMgOrMl { get; set; }
        public string DrugDosage { get; set; }
        public string DrugDuration { get; set; }
        public string DrugAdviceOrComments { get; set; }
        public string TestName { get; set; }
        public string TestDescription { get; set; }

    }
    public class AllPatientsNamesDetails
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }  
        
    }
    public class AllDrugNamesDetails 
    {
        public string DrugName { get; set; }
    }

    public class AllTestNamesDetails
    {
        public string TestName { get; set; }
    }
    public class AllPastPrescriptionDataModel
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string DrugName { get; set; }
        public string DrugAdviceOrComments { get; set; }
        public string TestName { get; set; }
        public string TestDescription { get; set; }
        public string CreatedAt { get; set; }

    }
    public class ResponseListModel 
    {
        public Object data { get; set; } = new object();
    }

}
