namespace ClinicManagementSystem.Models
{
    public class AllDrugModel
    {
        public string Id { get; set; }
        public string DrugName { get; set; }        
        public string GenericName { get; set; }
    }
    public class AllDrugModelVM 
    {
        public List<AllDrugModel> allDrugModelsList { get; set; } = new List<AllDrugModel>();
    }

    public class NewDrug 
    {
        public int DocID { get; set; }
        public string DrugName { get; set; }
        public string GenericName { get; set; }
        public string Note { get; set; }
    }
}
