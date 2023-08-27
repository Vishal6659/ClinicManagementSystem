using ClinicManagementSystem.Helper;
using ClinicManagementSystem.Models;
using System.Data;

namespace ClinicManagementSystem.Services
{
    public interface IPatientServices 
    {
        int AddNewPatient(NewPatient newPatient);
        List<AllPatientModel> GetAllPatientListData(int DocId);
        List<AllPatientModel> GetAllPatientListDataForToday(int DocId);
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
                new Parameters{ ParameterName = "Age", ParameterValue =Convert.ToString(newPatient.Age)},
                new Parameters{ ParameterName = "Phone", ParameterValue = Convert.ToString(newPatient.Phone)},
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
                            PatientName = Convert.ToString(dataTable.Rows[i]["firstname"]),
                            Phone = Convert.ToInt64(dataTable.Rows[i]["phone"]),
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

        public List<AllPatientModel> GetAllPatientListDataForToday(int DocId) 
        {
            try
            {
                List<AllPatientModel> allPatientsList = new List<AllPatientModel>();
                DataTable dataTable = new DataTable();
                List<Parameters> parameters = new List<Parameters>()
                {
                    new Parameters{ ParameterName = "DocId", ParameterValue = Convert.ToString(DocId)}
                };
                dataTable = _pDb.SelectMethod(QueryHelper.getAllPatientListDataForToday, parameters);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        allPatientsList.Add(new AllPatientModel()
                        {
                            ID = i + 1,
                            PatientName = Convert.ToString(dataTable.Rows[i]["firstname"]),
                            Phone = Convert.ToInt64(dataTable.Rows[i]["phone"]),
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
    }
}
