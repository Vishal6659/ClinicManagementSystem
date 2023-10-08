using ClinicManagementSystem.Helper;
using ClinicManagementSystem.Models;
using System.Data;
using System.Reflection.Metadata;

namespace ClinicManagementSystem.Services
{
    public interface IDrugSevices
    {
        int AddNewDrug(NewDrug newDrug);
        List<AllDrugModel> GetAllDrugListData(int DocId);
        int deleteDrugRecord(DeleteDrugModel deleteDrugModel);
    }
    public class DrugSevices : IDrugSevices
    {
        PostgresDbHelper _pDb;
        public DrugSevices()
        {
            _pDb = new PostgresDbHelper();
        }

        public int AddNewDrug(NewDrug newDrug)
        {
            int result = 0;
            List<Parameters> parameters = new List<Parameters>()
           {
               new Parameters{ ParameterName = "DocId", ParameterValue=Convert.ToString( newDrug.DocID)},
               new Parameters{ ParameterName = "DrugName", ParameterValue=Convert.ToString( newDrug.DrugName)},
               new Parameters{ ParameterName = "GenericName", ParameterValue=Convert.ToString( newDrug.GenericName)},
               new Parameters{ ParameterName = "Note", ParameterValue=Convert.ToString( newDrug.Note)}
           };
            result = _pDb.InsertUpdateDelete(QueryHelper.insertNewDrugData, parameters);
            if (result != 0 && result > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public List<AllDrugModel> GetAllDrugListData(int DocId)
        {
            try
            {
                List<AllDrugModel> allDrugModelsList = new List<AllDrugModel>();
                DataTable dataTable = new DataTable();
                List<Parameters> parameters = new List<Parameters>()
                {
                    new Parameters{ ParameterName = "DocId", ParameterValue = Convert.ToString(DocId)}
                };
                dataTable = _pDb.SelectMethod(QueryHelper.getAllDrugListData, parameters);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        allDrugModelsList.Add(new AllDrugModel()
                        {
                            Id = i + 1,
                            RecordId = Convert.ToInt32(dataTable.Rows[i]["id"]),
                            DrugName = Convert.ToString(dataTable.Rows[i]["drugname"]),
                            GenericName = Convert.ToString(dataTable.Rows[i]["genericname"])
                        });
                    }
                }
                return allDrugModelsList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public int deleteDrugRecord(DeleteDrugModel deleteDrugModel)
        {
            int result = 0;
            List<Parameters> parameters = new List<Parameters>()
            {
                new Parameters{ ParameterName = "DocId", ParameterValue = Convert.ToString( deleteDrugModel.DocId)},
                new Parameters{ ParameterName = "RecordId", ParameterValue = Convert.ToString( deleteDrugModel.RecordId)}
            };
            result = _pDb.InsertUpdateDelete(QueryHelper.deleteDrugRecord, parameters);
            if (result != 0 && result > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
