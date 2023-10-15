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
        List<AllPastPrescriptionDataModel> allPastRecordsOfPatient(int DocId, int PatientId);
        int deletePrescriptionRecord(DeletePatientModel deletePatientModel);
        ViewPrescriptionDataModel getDataToView(int DocId, int RecordId);
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
                            PatientFirstName = Convert.ToString(dataTable.Rows[i]["firstname"]),
                            PatientLastName = Convert.ToString(dataTable.Rows[i]["lastname"])
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
                        RecordId = Convert.ToString(dataTable.Rows[i]["id"]),
                        PatientId = Convert.ToString(dataTable.Rows[i]["patient_id"]),
                        PatientName = Convert.ToString(dataTable.Rows[i]["patient_name"]),
                        DrugName = Convert.ToString(dataTable.Rows[i]["drug_name"]),
                        DrugAdviceOrComments = Convert.ToString(dataTable.Rows[i]["drug_dosage"]),
                        TestName = Convert.ToString(dataTable.Rows[i]["test_name"]),
                        TestDescription = Convert.ToString(dataTable.Rows[i]["test_description"]),
                    });
                }
            }
            return allPrescriptionLists;
        }

        public List<AllPastPrescriptionDataModel> allPastRecordsOfPatient(int DocId, int PatientId) 
        {
            List<AllPastPrescriptionDataModel> allPastRecordsOfPatientList = new List<AllPastPrescriptionDataModel>();
            DataTable dataTable = new DataTable();
            List<Parameters> parameters = new List<Parameters>()
            {
                new Parameters{ ParameterName = "DocId", ParameterValue=Convert.ToString(DocId)},
                new Parameters{ ParameterName = "PatientId", ParameterValue=Convert.ToString( PatientId)}
            };
            dataTable = _pDb.SelectMethod(QueryHelper.getAllPastRecordsOfPatientByPatientId, parameters);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    allPastRecordsOfPatientList.Add(new AllPastPrescriptionDataModel()
                    {
                        Id = i + 1,                        
                        PatientName = Convert.ToString(dataTable.Rows[i]["patient_name"]),
                        DrugName = Convert.ToString(dataTable.Rows[i]["drug_name"]),
                        DrugAdviceOrComments = Convert.ToString(dataTable.Rows[i]["drug_dosage"]),
                        TestName = Convert.ToString(dataTable.Rows[i]["test_name"]),
                        TestDescription = Convert.ToString(dataTable.Rows[i]["test_description"]),
                        CreatedAt = Convert.ToString(dataTable.Rows[i]["created_at"])
                    });
                }
            }
            return allPastRecordsOfPatientList;
        }

        public int deletePrescriptionRecord(DeletePatientModel deletePatientModel)
        {
            int result = 0;
            List<Parameters> parameters = new List<Parameters>()
            {
                new Parameters{ ParameterName = "DocId", ParameterValue = Convert.ToString( deletePatientModel.DocId)},
                new Parameters{ ParameterName = "PatientId", ParameterValue = Convert.ToString( deletePatientModel.PatientId)},
                new Parameters{ ParameterName = "RecordId", ParameterValue = Convert.ToString( deletePatientModel.RecordId)}
            };
            result = _pDb.InsertUpdateDelete(QueryHelper.deletePrescriptionRecord, parameters);
            if (result != 0 && result > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public ViewPrescriptionDataModel getDataToView(int DocId, int RecordId) 
        {
            try
            {
                ViewPrescriptionDataModel viewPrescriptionDataModel = new ViewPrescriptionDataModel();
                DataTable dataTable = new DataTable();
                List<Parameters> parameters = new List<Parameters>()
                {
                    new Parameters{ ParameterName = "DocId", ParameterValue = Convert.ToString(DocId)},
                    new Parameters{ ParameterName = "RecordId", ParameterValue = Convert.ToString(RecordId)}
                };
                dataTable = _pDb.SelectMethod(QueryHelper.getPrescriptionRecordDataToView, parameters);
                if (dataTable != null && dataTable.Rows.Count > 0) 
                {
                    viewPrescriptionDataModel.PatientName = Convert.ToString(dataTable.Rows[0]["patient_name"]);
                    viewPrescriptionDataModel.DrugType = Convert.ToString(dataTable.Rows[0]["drug_type"]);
                    viewPrescriptionDataModel.DrugName = Convert.ToString(dataTable.Rows[0]["drug_name"]);
                    viewPrescriptionDataModel.DrugMgOrMl = Convert.ToString(dataTable.Rows[0]["drug_mgorml"]);
                    viewPrescriptionDataModel.DrugDosage = Convert.ToString(dataTable.Rows[0]["drug_dosage"]);
                    viewPrescriptionDataModel.TestName = Convert.ToString(dataTable.Rows[0]["test_name"]);
                    viewPrescriptionDataModel.TestDescription = Convert.ToString(dataTable.Rows[0]["test_description"]);
                    viewPrescriptionDataModel.CreatedAt = Convert.ToString(dataTable.Rows[0]["created_at"]);
                }
                return viewPrescriptionDataModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
