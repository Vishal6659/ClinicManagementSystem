namespace ClinicManagementSystem.Models
{
    public class AllDrugModel
    {
        public int Id { get; set; }
        public int RecordId { get; set; }
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
    public class DeleteDrugModel
    {
        public int DocId { get; set; }
        public int RecordId { get; set; }
    }
    public class ViewRowDrugData
    {
        public string DrugName { get; set; }
        public string GenericName { get; set; }
        public string Description { get; set; }
    }

    public class EditDrugModel
    {
        public int DocID { get; set; }
        public int RecordID { get; set; }
        public string NewDrugName { get; set; }
        public string NewDrugDescription { get; set; }
        public string NewGenericName { get; set; }
    }


}
