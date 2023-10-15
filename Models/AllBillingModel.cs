namespace ClinicManagementSystem.Models
{
    public class AllBillingModel
    {
        public int Id { get; set; }
        public int RecordId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string CreatedOn { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }
    }

    public class AllBillingModelVM 
    {
        public List<AllBillingModel> allBillingModelsList { get; set; } = new List<AllBillingModel>();
    }

    public class NewBillingModel 
    {
        public int DocId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PaymentMode { get; set; }
        public string Amount { get; set; }
        public string PaymentStatus { get; set; }
    }

    public class ViewBillingDataModel 
    {
        public string PatientName { get; set; }
        public string PaymentMode { get; set; }
        public string Amount { get; set; }
        public string PaymentStatus { get; set; }
        public string CreatedAt{ get; set; }
    }
    public class AllPatientsNamesDetail
    {
        public int PatientId { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }

    }
    public class DeleteBillingModel
    {
        public int DocId { get; set; }
        public int RecordId { get; set; }
        public int PatientId { get; set; }
    }
}
