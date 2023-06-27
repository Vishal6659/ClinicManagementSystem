using ClinicManagementSystem.Models;
using ClinicManagementSystem.Helper;
using System.Data;

namespace ClinicManagementSystem.Services
{
    public interface ITestServices 
    {
        int AddTest(NewTest newTest);
        List<AllTestModel> GetAllTestList(int DocId);
    }
    public class TestServices :ITestServices
    {
        PostgresDbHelper _pDb;
        public TestServices()
        {
            _pDb = new PostgresDbHelper();
        }
        public int AddTest(NewTest newTest) 
        {
            int result = 0;
            List<Parameters> parameters = new List<Parameters>() 
            {
                new Parameters{ ParameterName = "DocId", ParameterValue=Convert.ToString( newTest.DocID)},
                new Parameters{ ParameterName = "TestName", ParameterValue=Convert.ToString( newTest.TestName)},
                new Parameters{ ParameterName = "Description", ParameterValue=Convert.ToString( newTest.Description)}

            };
            result = _pDb.InsertUpdateDelete(QueryHelper.insertNewTestData, parameters);
            if (result != 0 && result > 0)
            {
                return 1;
            }
            else 
            {
                return 0;
            }
        }
        public List<AllTestModel> GetAllTestList(int DocId) 
        {
            try
            {
                List<AllTestModel> allTestModelsList = new List<AllTestModel>();
                DataTable dataTable = new DataTable();
                List<Parameters> parameters = new List<Parameters>() 
                {
                    new Parameters{ ParameterName = "DocId", ParameterValue = Convert.ToString(DocId)}
                };
                dataTable = _pDb.SelectMethod(QueryHelper.getAllTestListData, parameters);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        allTestModelsList.Add(new AllTestModel()
                        {
                            /*Id = Convert.ToString( dataTable.Rows[i]["id"]),*/
                            Id = i+1,
                            TestName = Convert.ToString(dataTable.Rows[i]["testname"]),
                            Description = Convert.ToString(dataTable.Rows[i]["description"])
                        });
                    }
                }
                return allTestModelsList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
