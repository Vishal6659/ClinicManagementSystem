using ClinicManagementSystem.Helper;
using ClinicManagementSystem.Models;
using System.Data;

namespace ClinicManagementSystem.Services
{
    public interface IDrugSevices 
    {
      //  List<AllDrugModel> GetAllDrugListData();
    }
    public class DrugSevices : IDrugSevices
    {
        PostgresDbHelper _pDb;
        public DrugSevices()
        {
            _pDb = new PostgresDbHelper();
        }

       /* public List<AllDrugModel> GetAllDrugListData()
        {
            try
            {
                List<AllDrugModel> allDrugs = new List<AllDrugModel>();
                DataTable dataTable = new DataTable();
                dataTable = _pDb.SelectMethod();
            }
            catch (Exception ex)
            {
                throw;
            }

        }*/
    }
}
