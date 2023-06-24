namespace ClinicManagementSystem.Models
{
    public class AllTestModel
    {
        public string Id { get; set; }
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
}
