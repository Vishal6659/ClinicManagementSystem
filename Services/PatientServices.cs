using ClinicManagementSystem.Helper;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Services
{
    public interface IPatientServices 
    {
        int AddNewPatient(NewPatient newPatient);
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
    }
}
