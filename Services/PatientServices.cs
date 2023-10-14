using ClinicManagementSystem.Helper;
using ClinicManagementSystem.Models;
using System.Data;

namespace ClinicManagementSystem.Services
{
    public interface IPatientServices 
    {
        int AddNewPatient(NewPatient newPatient);
        List<AllPatientModel> GetAllPatientListData(int DocId);
        int deletePatientRecord(DeletePatientModel deletePatientModel);
        ViewPatientDataModel getDataToView(int DocId, int RecordId);
    }
    public class PatientServices : IPatientServices
    {
        PostgresDbHelper _pDb;
        public PatientServices()
        {
            _pDb = new PostgresDbHelper();
        }
        public int AddNewPatient(NewPatient newPatient) 
        {
            int result = 0;
            List<Parameters> parameters = new List<Parameters>() 
            {
                new Parameters{ ParameterName = "DocId", ParameterValue = Convert.ToString( newPatient.DocID)},
                new Parameters{ ParameterName = "PatientId", ParameterValue = Convert.ToString( newPatient.PatientID)},
                new Parameters{ ParameterName = "FirstName", ParameterValue = newPatient.FirstName},
                new Parameters{ ParameterName = "LastName", ParameterValue = newPatient.LastName},
                new Parameters{ ParameterName = "Age", ParameterValue = newPatient.Age},
                new Parameters{ ParameterName = "Phone", ParameterValue = newPatient.Phone},
                new Parameters{ ParameterName = "Gender", ParameterValue = newPatient.Gender},
                new Parameters{ ParameterName = "PresentComplaint", ParameterValue = newPatient.PresentComplaint},
                new Parameters{ ParameterName = "PastHistory", ParameterValue = newPatient.PastHistory},
                new Parameters{ ParameterName = "FamilyHistory", ParameterValue = newPatient.FamilyHistory},
                new Parameters{ ParameterName = "PresentMedication", ParameterValue = newPatient.PresentMedication},
                new Parameters{ ParameterName = "PhysicalNature", ParameterValue = newPatient.PhysicalNature},
                new Parameters{ ParameterName = "MentalNature", ParameterValue = newPatient.MentalNature},
               
            };
            result = _pDb.InsertUpdateDelete(QueryHelper.insertNewPatientData, parameters);
            if (result != 0 && result > 0)
            {
                return 1;
            }
            else 
            {
                return 0;
            }
        }

        public List<AllPatientModel> GetAllPatientListData(int DocId) 
        {            
            try
            {
                List<AllPatientModel> allPatientsList = new List<AllPatientModel>();
                DataTable dataTable = new DataTable();
                List<Parameters> parameters = new List<Parameters>()
                {
                    new Parameters{ ParameterName = "DocId", ParameterValue = Convert.ToString(DocId)}
                };
                dataTable = _pDb.SelectMethod(QueryHelper.getAllPatientListData, parameters);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        allPatientsList.Add(new AllPatientModel()
                        {
                            ID = i+1,
                            RecordId = Convert.ToString(dataTable.Rows[i]["id"]),
                            PatientFirstName = Convert.ToString(dataTable.Rows[i]["firstname"]),
                            PatientLastName = Convert.ToString(dataTable.Rows[i]["lastname"]),
                           // PatientName = Convert.ToString(dataTable.Rows[i]["firstname"]),
                            PatientId = Convert.ToString(dataTable.Rows[i]["patient_id"]),
                            Phone = Convert.ToString(dataTable.Rows[i]["phone"]),
                            Date = Convert.ToString(dataTable.Rows[i]["created_at"])
                        });
                    }
                }
                return allPatientsList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public int deletePatientRecord(DeletePatientModel deletePatientModel)
        {
            int result = 0;
            List<Parameters> parameters = new List<Parameters>()
            {
                new Parameters{ ParameterName = "DocId", ParameterValue = Convert.ToString( deletePatientModel.DocId)},
                new Parameters{ ParameterName = "PatientId", ParameterValue = Convert.ToString( deletePatientModel.PatientId)},
                new Parameters{ ParameterName = "RecordId", ParameterValue = Convert.ToString( deletePatientModel.RecordId)}
            };
            result = _pDb.InsertUpdateDelete(QueryHelper.deletePatientRecord, parameters);
            if (result != 0 && result > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public ViewPatientDataModel getDataToView(int DocId, int RecordId)
        {
            try
            {
                ViewPatientDataModel viewPatientDataModel = new ViewPatientDataModel();
                DataTable dataTable = new DataTable();
                List<Parameters> parameters = new List<Parameters>()
                {
                    new Parameters{ ParameterName = "DocId", ParameterValue = Convert.ToString(DocId)},
                    new Parameters{ ParameterName = "RecordId", ParameterValue = Convert.ToString(RecordId)}
                };
                dataTable = _pDb.SelectMethod(QueryHelper.getPatientRecordDataToView, parameters);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    viewPatientDataModel.FirstName = Convert.ToString(dataTable.Rows[0]["firstname"]);
                    viewPatientDataModel.LastName = Convert.ToString(dataTable.Rows[0]["lastname"]);
                    viewPatientDataModel.Age = Convert.ToString(dataTable.Rows[0]["patientage"]);
                    viewPatientDataModel.Phone = Convert.ToString(dataTable.Rows[0]["phone"]);
                    viewPatientDataModel.Gender = Convert.ToString(dataTable.Rows[0]["gender"]);
                    viewPatientDataModel.PresentComplaint = Convert.ToString(dataTable.Rows[0]["presentcomplaint"]);
                    viewPatientDataModel.PastHistory = Convert.ToString(dataTable.Rows[0]["pasthistory"]);
                    viewPatientDataModel.FamilyHistory = Convert.ToString(dataTable.Rows[0]["familyhistory"]);
                    viewPatientDataModel.PresentMedication = Convert.ToString(dataTable.Rows[0]["presentmedication"]);
                    viewPatientDataModel.PhysicalNature = Convert.ToString(dataTable.Rows[0]["physicalnature"]);
                    viewPatientDataModel.MentalNature = Convert.ToString(dataTable.Rows[0]["mentalnature"]);
                    viewPatientDataModel.CreatedAt = Convert.ToString(dataTable.Rows[0]["created_at"]);
                }
                return viewPatientDataModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
