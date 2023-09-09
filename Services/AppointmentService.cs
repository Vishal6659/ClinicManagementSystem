using ClinicManagementSystem.Helper;
using ClinicManagementSystem.Models;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata;

namespace ClinicManagementSystem.Services
{
    public interface IAppointmentService 
    {
         int AddNewAppointment(NewAppointment newAppointment);
        List<AllAppointmentModel> GetAllAppointmentList(int DocId);
        int deletePatientRecord(DeletePrescriptionModel deletePrescriptionModel);


    }
    public class AppointmentService : IAppointmentService
    {
        PostgresDbHelper _pDb;
        public AppointmentService()
        {
            _pDb = new PostgresDbHelper();
        }

        public int AddNewAppointment(NewAppointment newAppointment)
        {
            int result = 0;
            List<Parameters> parameters = new List<Parameters>()
            {
                new Parameters{ ParameterName = "DocId", ParameterValue=Convert.ToString( newAppointment.DocID)},
               new Parameters{ ParameterName = "Name", ParameterValue=Convert.ToString( newAppointment.Name)},
               new Parameters{ ParameterName = "Date", ParameterValue=Convert.ToString( newAppointment.Date)},
               new Parameters{ ParameterName = "Time", ParameterValue=Convert.ToString( newAppointment.Time)},
               new Parameters{ ParameterName = "Status", ParameterValue=Convert.ToString( newAppointment.Status)}
            };
            result = _pDb.InsertUpdateDelete(QueryHelper.insertNewAppointmentData, parameters);
            if (result != 0)
            {
                return 1;
            }
            else 
            {
                return 0;
            }
        }

        public List<AllAppointmentModel> GetAllAppointmentList(int DocId) 
        {
            try
            {
                List<AllAppointmentModel> allAppointmentModelsList = new List<AllAppointmentModel> ();
                DataTable dataTable = new DataTable();
                List<Parameters> parameter = new List<Parameters>() 
                {
                    new Parameters{ ParameterName = "DocId", ParameterValue = Convert.ToString( DocId)}
                };
                dataTable = _pDb.SelectMethod(QueryHelper.getAllAppointmentListData, parameter);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        allAppointmentModelsList.Add(new AllAppointmentModel()
                        {
                            Id = i+1,
                            RecordId = Convert.ToString(dataTable.Rows[i]["id"]),
                            Name = Convert.ToString( dataTable.Rows[i]["namee"]),
                            Date = Convert.ToString( dataTable.Rows[i]["datee"]),
                            Time = Convert.ToString( dataTable.Rows[i]["timee"]),
                            Status = Convert.ToString( dataTable.Rows[i]["status"]),
                            createdAt = Convert.ToString( dataTable.Rows[i]["createdat"])
                        });
                    }
                }
                return allAppointmentModelsList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public int deletePatientRecord(DeletePrescriptionModel deletePrescriptionModel)
        {
            int result = 0;
            List<Parameters> parameters = new List<Parameters>()
            {
                new Parameters{ ParameterName = "DocId", ParameterValue = Convert.ToString( deletePrescriptionModel.DocId)},
                new Parameters{ ParameterName = "RecordId", ParameterValue = Convert.ToString( deletePrescriptionModel.RecordId)}
            };
            result = _pDb.InsertUpdateDelete(QueryHelper.deleteAppointmentRecord, parameters);
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
