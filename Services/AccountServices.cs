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
                    responseModel.Firstame = Convert.ToString(dataTable.Rows[0]["firstname"]);
                    responseModel.Middlename = Convert.ToString(dataTable.Rows[0]["middlename"]);
                    responseModel.Lastname = Convert.ToString(dataTable.Rows[0]["lastname"]);
                    responseModel.Age = Convert.ToString(dataTable.Rows[0]["age"]);
                    responseModel.Mobilenumber = Convert.ToString(dataTable.Rows[0]["mobilenumber"]);
                    responseModel.Email = Convert.ToString(dataTable.Rows[0]["email"]);
                    responseModel.Address = Convert.ToString(dataTable.Rows[0]["address"]);
                    responseModel.Officaname = Convert.ToString(dataTable.Rows[0]["officename"]);
                    responseModel.Landmark = Convert.ToString(dataTable.Rows[0]["landmark"]);
                    responseModel.City = Convert.ToString(dataTable.Rows[0]["city"]);
                    responseModel.State = Convert.ToString(dataTable.Rows[0]["state"]);
                    responseModel.Country = Convert.ToString(dataTable.Rows[0]["country"]);
                    responseModel.Gender = Convert.ToString(dataTable.Rows[0]["gender"]);
                    responseModel.Username = Convert.ToString(dataTable.Rows[0]["username"]);
                    responseModel.Password = Convert.ToString(dataTable.Rows[0]["_password"]);
                    responseModel.Confirmpassword = Convert.ToString(dataTable.Rows[0]["confirmpassword"]);
                    
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
