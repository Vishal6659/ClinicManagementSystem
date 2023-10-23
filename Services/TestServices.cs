using ClinicManagementSystem.Models;
using ClinicManagementSystem.Helper;
using System.Data;

namespace ClinicManagementSystem.Services
{
    public interface ITestServices 
    {
        int AddTest(NewTest newTest);
        List<AllTestModel> GetAllTestList(int DocId);
        int deleteTestRecord(DeleteTestModel deleteTestModel);
        ViewRowTestData getDataToView(int DocId, int RecordId);
        int updateRowData(EditTestModel editTestModel);
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
                            Id = i+1,
                            RecordId = Convert.ToInt32(dataTable.Rows[i]["id"]),
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

        public ViewRowTestData getDataToView(int DocId, int RecordId) 
        {
            try
            {
                ViewRowTestData viewRowTestData = new ViewRowTestData();
                DataTable dataTable = new DataTable();
                List<Parameters> parameters = new List<Parameters>()
                {
                    new Parameters{ ParameterName = "DocId", ParameterValue = Convert.ToString(DocId)},
                    new Parameters{ ParameterName = "RecordId", ParameterValue = Convert.ToString(RecordId)}
                };
                dataTable = _pDb.SelectMethod(QueryHelper.getTestsRecordDataToView, parameters);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    viewRowTestData.TestName = Convert.ToString(dataTable.Rows[0]["testname"]);
                    viewRowTestData.Description = Convert.ToString(dataTable.Rows[0]["description"]);
                }
                return viewRowTestData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int deleteTestRecord(DeleteTestModel deleteTestModel)
        {
            int result = 0;
            List<Parameters> parameters = new List<Parameters>()
            {
                new Parameters{ ParameterName = "DocId", ParameterValue = Convert.ToString( deleteTestModel.DocId)},
                new Parameters{ ParameterName = "RecordId", ParameterValue = Convert.ToString( deleteTestModel.RecordId)}
            };
            result = _pDb.InsertUpdateDelete(QueryHelper.deleteTestRecord, parameters);
            if (result != 0 && result > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int updateRowData(EditTestModel editTestModel) 
        {
            int result = 0;
            List<Parameters> parameters = new List<Parameters>()
            {
                new Parameters{ ParameterName = "DocId", ParameterValue = Convert.ToString( editTestModel.DocID)},
                new Parameters{ ParameterName = "RecordId", ParameterValue = Convert.ToString( editTestModel.RecordID)},
                new Parameters{ ParameterName = "NewTestName", ParameterValue = Convert.ToString( editTestModel.NewTestName)},
                new Parameters{ ParameterName = "NewDescription", ParameterValue = Convert.ToString( editTestModel.NewDescription)}
            };
            result = _pDb.InsertUpdateDelete(QueryHelper.editRowDataForTests, parameters);
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
