namespace ClinicManagementSystem.Models
{
    public class AllTestModel
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public string Description { get; set; }
    }

    public class AllTestModelVM 
    {
        public List<AllTestModel> allTestModelsList = new List<AllTestModel>(); 
    }

    public class NewTest 
    {
        public string TestName { get; set; }
        public string Description { get; set; }
    }
}
