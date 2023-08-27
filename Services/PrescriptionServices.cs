using ClinicManagementSystem.Helper;
using ClinicManagementSystem.Models;
using System.Data;
using System.Text.RegularExpressions;

namespace ClinicManagementSystem.Services
{
    public interface IPrescriptionServices 
    {
        List<AllPatientsNamesDetails> allPatientsNames(int DocId);
        List<AllDrugNamesDetails> allDrugNames(int DocId);
        List<AllTestNamesDetails> allTestNames(int DocId);
        int addNewPrescriptionData(NewPrescriptionModel newPrescriptionModel);
        List<AllPrescriptionModel> allPrescriptionLists(int DocId);
    }
    public class PrescriptionServices : IPrescriptionServices
    {
        PostgresDbHelper _pDb;
        public PrescriptionServices()
        {
            _pDb = new PostgresDbHelper();
        }
        public List<AllPatientsNamesDetails> allPatientsNames(int DocId) 
        {
            List<AllPatientsNamesDetails> allPatientsNames = new();
            try
            {
                if (DocId != 0)
                {
                    List<Parameters> parameters = new List<Parameters>
                    {
                        new Parameters{ParameterName = "DocId", ParameterValue = Convert.ToString(DocId)}
                    };
                    DataTable dataTable = _pDb.SelectMethod(QueryHelper.getAllPatientsNameByDocId, parameters);
                    int i = 0;
                    for (int j = dataTable.Rows.Count; i < j; i++)
                    {
                        allPatientsNames.Add(new AllPatientsNamesDetails
                        {
                            PatientId = Convert.ToInt32( dataTable.Rows[i]["patient_id"]),
                            PatientName = Convert.ToString(dataTable.Rows[i]["firstname"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return allPatientsNames;
        }

        public List<AllDrugNamesDetails> allDrugNames(int DocId) 
        {
            List<AllDrugNamesDetails> allDrugNames = new();
            try
            {
                if (DocId != 0)
                {
                    List<Parameters> parameters = new List<Parameters>
                    {
                        new Parameters{ParameterName = "DocId", ParameterValue = Convert.ToString(DocId)}
                    };
                    DataTable dataTable = _pDb.SelectMethod(QueryHelper.getAllDrugNameByDocId, parameters);
                    int i = 0;
                    for (int j = dataTable.Rows.Count; i < j; i++)
                    {
                        allDrugNames.Add(new AllDrugNamesDetails
                        {
                            DrugName = Convert.ToString(dataTable.Rows[i]["drugname"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return allDrugNames;
        }

        public List<AllTestNamesDetails> allTestNames(int DocId) 
        {
            List<AllTestNamesDetails> allTestNames = new();
            try
            {
                List<Parameters> parameters = new List<Parameters>
                    {
                        new Parameters{ParameterName = "DocId", ParameterValue = Convert.ToString(DocId)}
                    };
                DataTable dataTable = _pDb.SelectMethod(QueryHelper.getAllTestNameByDocId, parameters);
                int i = 0;
                for (int j = dataTable.Rows.Count; i < j; i++)
                {
                    allTestNames.Add(new AllTestNamesDetails
                    {
                        TestName = Convert.ToString(dataTable.Rows[i]["testname"])
                    });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return allTestNames;
        }

        public int addNewPrescriptionData(NewPrescriptionModel newPrescriptionModel) 
        {
            int result = 0;
            string Name = newPrescriptionModel.PatientName;
            int indexOfName = Name.IndexOf('-');
            if (indexOfName >=0) 
            {
                string nameOfPatient = Name.Substring(0, indexOfName);
                newPrescriptionModel.PatientName = nameOfPatient;
            }            
            List<Parameters> parameters = new List<Parameters>()
            {
                new Parameters{ ParameterName = "PatientId", ParameterValue = Convert.ToString( newPrescriptionModel.PatientId)},
                new Parameters{ ParameterName = "DocId", ParameterValue = Convert.ToString( newPrescriptionModel.DocId)},
                new Parameters{ ParameterName = "PatientName", ParameterValue = Convert.ToString( newPrescriptionModel.PatientName)},
                new Parameters{ ParameterName = "DrugType", ParameterValue = Convert.ToString( newPrescriptionModel.DrugType)},
                new Parameters{ ParameterName = "DrugName", ParameterValue = Convert.ToString( newPrescriptionModel.DrugName)},
                new Parameters{ ParameterName = "DrugMgOrMl", ParameterValue = Convert.ToString( newPrescriptionModel.DrugMgOrMl)},
                new Parameters{ ParameterName = "DrugDosage", ParameterValue = Convert.ToString( newPrescriptionModel.DrugDosage)},
                new Parameters{ ParameterName = "DrugDuration", ParameterValue = Convert.ToString( newPrescriptionModel.DrugDuration)},
                new Parameters{ ParameterName = "DrugAdviceOrComments", ParameterValue = Convert.ToString( newPrescriptionModel.DrugAdviceOrComments)},
                new Parameters{ ParameterName = "TestName", ParameterValue = Convert.ToString( newPrescriptionModel.TestName)},
                new Parameters{ ParameterName = "TestDescription", ParameterValue = Convert.ToString( newPrescriptionModel.TestDescription)}
            };
            result = _pDb.InsertUpdateDelete(QueryHelper.insertNewPrescriptionData, parameters);
            if (result != 0 && result > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public List<AllPrescriptionModel> allPrescriptionLists(int DocId) 
        {
            List<AllPrescriptionModel> allPrescriptionLists = new List<AllPrescriptionModel>();
            DataTable dataTable = new DataTable();
            List<Parameters> parameters = new List<Parameters>() 
            {
                new Parameters{ ParameterName = "DocId", ParameterValue=Convert.ToString( DocId)}
            };
            dataTable = _pDb.SelectMethod(QueryHelper.getAllPriscriptionListData, parameters);
            if (dataTable != null && dataTable.Rows.Count > 0) 
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    allPrescriptionLists.Add(new AllPrescriptionModel()
                    {
                        Id = i+1,
                        PatientName = Convert.ToString(dataTable.Rows[i]["patient_name"]),
                        DrugName = Convert.ToString(dataTable.Rows[i]["patient_name"]),
                        DrugAdviceOrComments = Convert.ToString(dataTable.Rows[i]["drug_dosage"]),
                        TestName = Convert.ToString(dataTable.Rows[i]["test_name"]),
                        TestDescription = Convert.ToString(dataTable.Rows[i]["test_description"]),
                    });
                }
            }
            return allPrescriptionLists;
        }
    }
}
