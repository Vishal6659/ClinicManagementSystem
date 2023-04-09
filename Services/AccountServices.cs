using ClinicManagementSystem.Helper;
using ClinicManagementSystem.Models;
using System.Data;

namespace ClinicManagementSystem.Services
{
    public interface IAccountServices 
    {
        bool insertRegistrationData(RegistrationModel registrationModel);
        bool checkLoginCredentials(LoginModel loginModel);
    }
    public class AccountServices : IAccountServices        
    {
        PostgresDbHelper _pDb;
        public AccountServices() 
        {
            _pDb = new PostgresDbHelper();  
        }
        public bool insertRegistrationData(RegistrationModel registrationModel) 
        {
            int _result = 0;
            List<Parameters> parameters = new List<Parameters>() 
            {
                new Parameters{ ParameterName = "Firstname", ParameterValue = registrationModel.Firstame},
                new Parameters{ ParameterName = "Middlename", ParameterValue = registrationModel.Middlename},
                new Parameters{ ParameterName = "Lastname", ParameterValue = registrationModel.Lastname},
                new Parameters{ ParameterName = "Age", ParameterValue = (registrationModel.Age)},
                new Parameters{ ParameterName = "Mobilenumber", ParameterValue = (registrationModel.Mobilenumber)},
                new Parameters{ ParameterName = "Email", ParameterValue = registrationModel.Email},
                new Parameters{ ParameterName = "Address", ParameterValue = registrationModel.Address},
                new Parameters{ ParameterName = "Officename", ParameterValue = registrationModel.Officaname},
                new Parameters{ ParameterName = "Landmark", ParameterValue = registrationModel.Landmark},
                new Parameters{ ParameterName = "City", ParameterValue = registrationModel.City},
                new Parameters{ ParameterName = "State", ParameterValue = registrationModel.State},
                new Parameters{ ParameterName = "Country", ParameterValue = registrationModel.Country},
                new Parameters{ ParameterName = "Gender", ParameterValue = registrationModel.Gender},
                new Parameters{ ParameterName = "Username", ParameterValue = registrationModel.Username},
                new Parameters{ ParameterName = "Pass", ParameterValue = registrationModel.Password},
                new Parameters{ ParameterName = "Confirmpassword", ParameterValue = registrationModel.Confirmpassword}
            };
            _result = _pDb.InsertUpdateDelete(QueryHelper.insertRegistrationData, parameters, Convert.ToString(Program.configuration["ClinicConnectionString"]));
            if (_result != null && _result > 0)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        public bool checkLoginCredentials(LoginModel loginModel) 
        {
            try
            {
                DataTable dataTable = new DataTable();
                List<Parameters> parameters = new List<Parameters>() 
                {
                    new Parameters{ ParameterName = "Username", ParameterValue = loginModel.Username},
                    new Parameters{ ParameterName = "Password", ParameterValue = loginModel.Password}
                };
                dataTable = _pDb.SelectMethod(Convert.ToString(Program.configuration["ClinicConnectionString"]), QueryHelper.verifyLoginCredentials, parameters);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
