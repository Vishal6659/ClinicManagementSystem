using ClinicManagementSystem.Helper;
using ClinicManagementSystem.Models;
using System.Data;

namespace ClinicManagementSystem.Services
{
    public interface IAccountServices 
    {
        int insertRegistrationData(RegistrationModel registrationModel);
        ResponseModel checkLoginCredentials(LoginModel loginModel);
    }
    public class AccountServices : IAccountServices        
    {
        PostgresDbHelper _pDb;
        public AccountServices() 
        {
            _pDb = new PostgresDbHelper();  
        }
        public int insertRegistrationData(RegistrationModel registrationModel) 
        {
            int _result = 0;
            List<Parameters> parameters = new List<Parameters>() 
            {
                new Parameters{ ParameterName = "Firstname", ParameterValue = registrationModel.FirstName},
                new Parameters{ ParameterName = "Lastname", ParameterValue = registrationModel.LastName},
                new Parameters{ ParameterName = "Gender", ParameterValue = registrationModel.Gender},
                new Parameters{ ParameterName = "DateOfBirth", ParameterValue = registrationModel.DateOfBirth.ToString("dddd, dd MMMM yyyy")},
                new Parameters{ ParameterName = "EmailAddress", ParameterValue = registrationModel.EmailAddress},
                new Parameters{ ParameterName = "PhoneNumber", ParameterValue = Convert.ToString( registrationModel.PhoneNumber)},
                new Parameters{ ParameterName = "Address", ParameterValue = registrationModel.Address},
                new Parameters{ ParameterName = "AadharCardNumber", ParameterValue = Convert.ToString( registrationModel.AadharCardNumber)},
                new Parameters{ ParameterName = "Specialization", ParameterValue = registrationModel.Specialization},
                new Parameters{ ParameterName = "MedicalLiscense", ParameterValue = registrationModel.MedicalLiscenseNumber},
                new Parameters{ ParameterName = "Qualificaition", ParameterValue = registrationModel.Qualification},
                new Parameters{ ParameterName = "Experiance", ParameterValue = registrationModel.Experiance},
                new Parameters{ ParameterName = "Affiliation", ParameterValue = registrationModel.Affiliation},
                new Parameters{ ParameterName = "LanguageSpoken", ParameterValue = registrationModel.LanguageSpoken},
                new Parameters{ ParameterName = "Username", ParameterValue = registrationModel.Username},
                new Parameters{ ParameterName = "Password", ParameterValue = registrationModel.Password}
               
            };
            _result = _pDb.InsertUpdateDelete(QueryHelper.insertRegistrationData, parameters);
            if (_result != 0 && _result > 0)
            {
                return 1;
            }
            else 
            {
                return 0;
            }
        }

        public ResponseModel checkLoginCredentials(LoginModel loginModel) 
        {           
            ResponseModel responseModel = new ResponseModel();
            try
            {
                DataTable dataTable = new DataTable();
                List<Parameters> parameters = new List<Parameters>() 
                {
                    new Parameters{ ParameterName = "Username", ParameterValue = loginModel.Username},
                    new Parameters{ ParameterName = "Password", ParameterValue = loginModel.Password}
                };
                dataTable = _pDb.SelectMethod(QueryHelper.verifyLoginCredentials, parameters);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    responseModel.Id = Convert.ToInt16(dataTable.Rows[0]["id"]);
                    responseModel.FirstName = Convert.ToString(dataTable.Rows[0]["firstname"]);
                    responseModel.LastName = Convert.ToString(dataTable.Rows[0]["lastname"]);
                    responseModel.Gender = Convert.ToString(dataTable.Rows[0]["gender"]);
                    responseModel.DateOfBirth = Convert.ToDateTime(dataTable.Rows[0]["dateofbirth"]);
                    responseModel.EmailAddress = Convert.ToString(dataTable.Rows[0]["email"]);
                    responseModel.PhoneNumber = Convert.ToInt64(dataTable.Rows[0]["phonenumber"]);
                    responseModel.AadharCardNumber = Convert.ToInt64(dataTable.Rows[0]["aadharcard"]);
                    responseModel.Address = Convert.ToString(dataTable.Rows[0]["address"]);
                    responseModel.Specialization = Convert.ToString(dataTable.Rows[0]["specialization"]);
                    responseModel.MedicalLiscenseNumber = Convert.ToString(dataTable.Rows[0]["medicalliscence"]);
                    responseModel.Qualification = Convert.ToString(dataTable.Rows[0]["qualification"]);
                    responseModel.Experiance = Convert.ToString(dataTable.Rows[0]["experiance"]);
                    responseModel.Affiliation = Convert.ToString(dataTable.Rows[0]["affiliation"]);
                    responseModel.LanguageSpoken = Convert.ToString(dataTable.Rows[0]["languagespoken"]);                    
                    
                }                
            }
            catch (Exception ex)
            {
                throw;
            }
            return responseModel;
        }
    }
}
