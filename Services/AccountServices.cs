using ClinicManagementSystem.Helper;
using ClinicManagementSystem.Models;
using System.Data;

namespace ClinicManagementSystem.Services
{
    public interface IAccountServices
    {
        int insertRegistrationData(RegistrationModel registrationModel);
        ResponseModel checkLoginCredentials(LoginModel loginModel);
        DashboardAllPatientsCount getTotalPatientsCount(int DocId);
        DashboardNewPatientsCount getAllNewPatientsCount(int DocId);
        DashboardAllAppointmentCount getAllAppointmentCountForDashboard(int DocId);
        DashboardNewAppointmentCount getNewAppointmentCountForDashboard(int DocId);
        DashboardNewPriscriptionCount getDashboardNewPriscriptionCount(int DocId);
        DashboardAllPriscriptionCount getAllPrescriptionCountForDashboard(int DocId);
        DashboardAllPaymentCountForToday GetAllPaymentCountForToday(int DocId);
        DashboardAllPaymentsCount getAllPaymentsCount(int DocId);
        List<AllPatientModel> GetAllPatientListDataForToday(int DocId);
        int deletePatientRecord(DeletePatientModel deletePatientModel);
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
                    responseModel.DocId = Convert.ToInt32(dataTable.Rows[0]["doc_id"]);
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

        public DashboardAllPatientsCount getTotalPatientsCount(int DocId)
        {
            DashboardAllPatientsCount dashboardAllPatientsCount = new DashboardAllPatientsCount();
            try
            {
                DataTable dataTable = new DataTable();
                List<Parameters> parameters = new List<Parameters>()
                {
                    new Parameters{ParameterName = "DocId", ParameterValue =Convert.ToString(DocId)}
                };
                dataTable = _pDb.SelectMethod(QueryHelper.getAllPatientCountForDashboard, parameters);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    dashboardAllPatientsCount.Count = Convert.ToInt32(dataTable.Rows[0]["Count"]);
                }
                return dashboardAllPatientsCount;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DashboardNewPatientsCount getAllNewPatientsCount(int DocId)
        {
            DashboardNewPatientsCount dashboardNewPatientsCount = new DashboardNewPatientsCount();
            try
            {
                DataTable dataTable = new DataTable();
                List<Parameters> parameters = new List<Parameters>()
                {
                    new Parameters{ParameterName = "DocId", ParameterValue =Convert.ToString(DocId)}
                };
                dataTable = _pDb.SelectMethod(QueryHelper.getAllNewPatientCountForDashboard, parameters);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    dashboardNewPatientsCount.Count = Convert.ToInt32(dataTable.Rows[0]["Count"]);
                }
                return dashboardNewPatientsCount;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DashboardAllAppointmentCount getAllAppointmentCountForDashboard(int DocId)
        {
            DashboardAllAppointmentCount dashboardAllAppointmentCount = new DashboardAllAppointmentCount();
            try
            {
                DataTable dataTable = new DataTable();
                List<Parameters> parameters = new List<Parameters>()
                {
                    new Parameters{ParameterName = "DocId", ParameterValue =Convert.ToString(DocId)}
                };
                dataTable = _pDb.SelectMethod(QueryHelper.getAllAppointmentCountForDashboard, parameters);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    dashboardAllAppointmentCount.Count = Convert.ToInt32(dataTable.Rows[0]["Count"]);
                }
                return dashboardAllAppointmentCount;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DashboardNewAppointmentCount getNewAppointmentCountForDashboard(int DocId)
        {
            DashboardNewAppointmentCount dashboardNewAppointmentCount = new DashboardNewAppointmentCount();
            try
            {
                DataTable dataTable = new DataTable();
                List<Parameters> parameters = new List<Parameters>()
                {
                    new Parameters{ParameterName = "DocId", ParameterValue =Convert.ToString(DocId)}
                };
                dataTable = _pDb.SelectMethod(QueryHelper.getNewAppointmentCountForDashboard, parameters);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    dashboardNewAppointmentCount.Count = Convert.ToInt32(dataTable.Rows[0]["Count"]);
                }
                return dashboardNewAppointmentCount;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DashboardNewPriscriptionCount getDashboardNewPriscriptionCount(int DocId)
        {
            DashboardNewPriscriptionCount dashboardNewPriscriptionCount = new DashboardNewPriscriptionCount();
            try
            {
                DataTable dataTable = new DataTable();
                List<Parameters> parameters = new List<Parameters>()
                {
                    new Parameters{ParameterName = "DocId", ParameterValue =Convert.ToString(DocId)}
                };
                dataTable = _pDb.SelectMethod(QueryHelper.getNewPrescriptionCountForDashboard, parameters);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    dashboardNewPriscriptionCount.Count = Convert.ToInt32(dataTable.Rows[0]["Count"]);
                }
                return dashboardNewPriscriptionCount;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DashboardAllPriscriptionCount getAllPrescriptionCountForDashboard(int DocId)
        {
            DashboardAllPriscriptionCount dashboardAllAppointmentCount = new DashboardAllPriscriptionCount();
            try
            {
                DataTable dataTable = new DataTable();
                List<Parameters> parameters = new List<Parameters>()
                {
                    new Parameters{ParameterName = "DocId", ParameterValue =Convert.ToString(DocId)}
                };
                dataTable = _pDb.SelectMethod(QueryHelper.getAllPrescriptionCountForDashboard, parameters);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    dashboardAllAppointmentCount.Count = Convert.ToInt32(dataTable.Rows[0]["Count"]);
                }
                return dashboardAllAppointmentCount;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DashboardAllPaymentCountForToday GetAllPaymentCountForToday(int DocId)
        {
            DashboardAllPaymentCountForToday dashboardAllPaymentCountForToday = new DashboardAllPaymentCountForToday();
            try
            {
                DataTable dataTable = new DataTable();
                List<Parameters> parameters = new List<Parameters>()
                {
                    new Parameters{ParameterName = "DocId", ParameterValue =Convert.ToString(DocId)}
                };
                dataTable = _pDb.SelectMethod(QueryHelper.getAllPaymentCountForToday, parameters);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    if (dataTable.Rows[0]["amount"] != DBNull.Value)
                    {
                        dashboardAllPaymentCountForToday.Count = Convert.ToInt32(dataTable.Rows[0]["amount"]);
                    }
                }
                else
                {
                    dashboardAllPaymentCountForToday.Count = 0;
                }
                return dashboardAllPaymentCountForToday;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DashboardAllPaymentsCount getAllPaymentsCount(int DocId)
        {
            DashboardAllPaymentsCount dashboardAllPaymentsCount = new DashboardAllPaymentsCount();
            try
            {
                DataTable dataTable = new DataTable();
                List<Parameters> parameters = new List<Parameters>()
                {
                    new Parameters{ParameterName = "DocId", ParameterValue =Convert.ToString(DocId)}
                };
                dataTable = _pDb.SelectMethod(QueryHelper.getAllPaymentCounts, parameters);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    if (dataTable.Rows[0]["amount"] != DBNull.Value)
                    {
                        dashboardAllPaymentsCount.Count = Convert.ToInt32(dataTable.Rows[0]["amount"]);
                    }
                }
                else
                {
                    dashboardAllPaymentsCount.Count = 0;
                }
                return dashboardAllPaymentsCount;
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
                            RecordId = Convert.ToString(dataTable.Rows[i]["id"]),
                            PatientId = Convert.ToString(dataTable.Rows[i]["patient_id"]),
                            PatientFirstName = Convert.ToString(dataTable.Rows[i]["firstname"]),
                            PatientLastName = Convert.ToString(dataTable.Rows[i]["lastname"]),
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
    }
}
