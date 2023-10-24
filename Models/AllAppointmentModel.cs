namespace ClinicManagementSystem.Models
{
    public class AllAppointmentModel
    {
        public int Id { get; set; }
        public string RecordId { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Status { get; set; }
        public string createdAt { get; set; }
    }

    public class AllAppointmentModelVM 
    {
        public List<AllAppointmentModel> allAppointmentModelsList {  get; set; } = new List<AllAppointmentModel>();
    }

    public class NewAppointment 
    {
        public int DocID { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Status { get; set; }

    }

    public class ViewAppointmentDataModel 
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Status { get; set; }
        public string CreatedAt { get; set; }
    }

    public class EditAppointmentModel
    {
        public int DocID { get; set; }
        public int RecordID { get; set; }
        public string NewName { get; set; }
        public string NewDate { get; set; }
        public string NewTime { get; set; }
        public string NewStatus { get; set; }
    }
}
