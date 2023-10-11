namespace ClinicManagementSystem.Models
{
    public class AllTestModel
    {
        public int Id { get; set; }
        public int RecordId { get; set; }
        public string TestName { get; set; }
        public string Description { get; set; }
    }

    public class AllTestModelVM 
    {
        public List<AllTestModel> allTestModelsList = new List<AllTestModel>(); 
    }

    public class NewTest 
    {
        public int DocID { get; set; }
        public string TestName { get; set; }
        public string Description { get; set; }
    }
    public class DeleteTestModel
    {
        public int DocId { get; set; }
        public int RecordId { get; set; }
    }
    public class ViewRowTestData 
    {
        public string TestName { get; set; }
        public string Description { get; set; }
    }
}
