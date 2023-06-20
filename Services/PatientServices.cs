using ClinicManagementSystem.Helper;
using ClinicManagementSystem.Models;
using System.Data;

namespace ClinicManagementSystem.Services
{
    public interface IPatientServices 
    {
        int AddNewPatient(NewPatient newPatient);
        List<AllPatientModel> GetAllPatientListData(int DocId);
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
                new Parameters{ ParameterName = "FirstName", ParameterValue = newPatient.FirstName},
                new Parameters{ ParameterName = "LastName", ParameterValue = newPatient.LastName},
                new Parameters{ ParameterName = "Age", ParameterValue =Convert.ToString(newPatient.Age)},
                new Parameters{ ParameterName = "Phone", ParameterValue = Convert.ToString(newPatient.Phone)},
                new Parameters{ ParameterName = "Birthday", ParameterValue = (newPatient.Birthday)},
                new Parameters{ ParameterName = "Gender", ParameterValue = newPatient.Gender},
                new Parameters{ ParameterName = "BloodGroup", ParameterValue = newPatient.BloodGroup},
                new Parameters{ ParameterName = "Address", ParameterValue = newPatient.Address},
                new Parameters{ ParameterName = "PatientWeight", ParameterValue =Convert.ToString( newPatient.PatientWeight)},
                new Parameters{ ParameterName = "PatientHeight", ParameterValue = Convert.ToString(newPatient.PatientHeight)}
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
                            ID = Convert.ToInt32(dataTable.Rows[i]["id"]),
                            PatientName = Convert.ToString(dataTable.Rows[i]["firstname"]),
                            Phone = Convert.ToInt64(dataTable.Rows[i]["phone"]),
                            BloodGroup = Convert.ToString(dataTable.Rows[i]["bloodGroup"]),
                            Date = Convert.ToString(dataTable.Rows[i]["birthday"])
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
    }
}
