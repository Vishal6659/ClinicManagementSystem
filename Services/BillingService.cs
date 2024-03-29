﻿using ClinicManagementSystem.Helper;
using ClinicManagementSystem.Models;
using System.Data;

namespace ClinicManagementSystem.Services
{
    public interface IBillingService 
    {
        int addNewBillingData(NewBillingModel newBillingModel);
        List<AllBillingModel> allBillingList(int DocId);
        List<AllPatientsNamesDetail> allPatientsNames(int DocId);
        int deleteBillingRecord(DeleteBillingModel deleteBillingModel);
        ViewBillingDataModel getDataToView(int DocId, int RecordId, int PatientId);
        int updateRowData(UpdateNewBillingModel updateNewBillingModel);
    }
    public class BillingService : IBillingService
    {
        PostgresDbHelper _pDb;
        public BillingService()
        {
            _pDb = new PostgresDbHelper();
        }
        public int addNewBillingData(NewBillingModel newBillingModel) 
        {
            int result = 0;
            string Name = newBillingModel.PatientName;
            int indexOfName = Name.IndexOf('-');
            if (indexOfName >= 0)
            {
                string nameOfPatient = Name.Substring(0, indexOfName);
                newBillingModel.PatientName = nameOfPatient;
            }
            List<Parameters> parameters = new List<Parameters>()
            {
                new Parameters{ ParameterName = "PatientId", ParameterValue = Convert.ToString( newBillingModel.PatientId)},
                new Parameters{ ParameterName = "DocId", ParameterValue = Convert.ToString( newBillingModel.DocId)},
                new Parameters{ ParameterName = "PatientName", ParameterValue = Convert.ToString( newBillingModel.PatientName)},
                new Parameters{ ParameterName = "PaymentMode", ParameterValue = Convert.ToString( newBillingModel.PaymentMode)},
                new Parameters{ ParameterName = "Amount", ParameterValue = Convert.ToString( newBillingModel.Amount)},
                new Parameters{ ParameterName = "PaymentStatus", ParameterValue = Convert.ToString( newBillingModel.PaymentStatus)}                
            };
            result = _pDb.InsertUpdateDelete(QueryHelper.insertNewBillingData, parameters);
            if (result != 0 && result > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public List<AllBillingModel> allBillingList(int DocId) 
        {
            List<AllBillingModel> allBillingModelsList = new List<AllBillingModel>();
            DataTable dataTable = new DataTable();
            List<Parameters> parameters = new List<Parameters>()
            {
                new Parameters{ ParameterName = "DocId", ParameterValue=Convert.ToString( DocId)}
            };
            dataTable = _pDb.SelectMethod(QueryHelper.getAllBillingListData, parameters);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    allBillingModelsList.Add(new AllBillingModel()
                    {
                        Id = i + 1,
                        RecordId = Convert.ToInt32(dataTable.Rows[i]["id"]),
                        PatientId = Convert.ToInt32(dataTable.Rows[i]["patient_id"]),
                        PatientName = Convert.ToString(dataTable.Rows[i]["patient_name"]),
                        Amount = Convert.ToString(dataTable.Rows[i]["amount"]),
                        Status = Convert.ToString(dataTable.Rows[i]["status"]),
                        CreatedOn = Convert.ToString(dataTable.Rows[i]["created_at"]),
                    });
                }
            }
            return allBillingModelsList;
        }

        public List<AllPatientsNamesDetail> allPatientsNames(int DocId)
        {
            List<AllPatientsNamesDetail> allPatientsNames = new();
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
                        allPatientsNames.Add(new AllPatientsNamesDetail
                        {
                            PatientId = Convert.ToInt32(dataTable.Rows[i]["patient_id"]),
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

        public int deleteBillingRecord(DeleteBillingModel deleteBillingModel)
        {
            int result = 0;
            List<Parameters> parameters = new List<Parameters>()
            {
                new Parameters{ ParameterName = "DocId", ParameterValue = Convert.ToString( deleteBillingModel.DocId)},
                new Parameters{ ParameterName = "PatientId", ParameterValue = Convert.ToString( deleteBillingModel.PatientId)},
                new Parameters{ ParameterName = "RecordId", ParameterValue = Convert.ToString( deleteBillingModel.RecordId)}
            };
            result = _pDb.InsertUpdateDelete(QueryHelper.deleteBillingRecord, parameters);
            if (result != 0 && result > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public ViewBillingDataModel getDataToView(int DocId, int RecordId, int PatientId) 
        {
            try
            {
                ViewBillingDataModel viewBillingDataModel = new ViewBillingDataModel();
                DataTable dataTable = new DataTable();
                List<Parameters> parameters = new List<Parameters>()
                {
                    new Parameters{ ParameterName = "DocId", ParameterValue = Convert.ToString(DocId)},
                    new Parameters{ ParameterName = "RecordId", ParameterValue = Convert.ToString(RecordId)},
                    new Parameters{ ParameterName = "PatientId", ParameterValue = Convert.ToString(PatientId)}
                };
                dataTable = _pDb.SelectMethod(QueryHelper.getBillingRecordDataToView, parameters);
                if (dataTable != null && dataTable.Rows.Count > 0) 
                {
                    viewBillingDataModel.PatientName = Convert.ToString(dataTable.Rows[0]["patient_name"]);
                    viewBillingDataModel.PaymentMode = Convert.ToString(dataTable.Rows[0]["payment_mode"]);
                    viewBillingDataModel.Amount = Convert.ToString(dataTable.Rows[0]["amount"]);
                    viewBillingDataModel.PaymentStatus = Convert.ToString(dataTable.Rows[0]["status"]);
                    viewBillingDataModel.CreatedAt = Convert.ToString(dataTable.Rows[0]["created_at"]);                    
                }
                return viewBillingDataModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int updateRowData(UpdateNewBillingModel updateNewBillingModel) 
        {
            int result = 0;
            List<Parameters> parameters = new List<Parameters>()
            {
                new Parameters{ ParameterName = "DocId", ParameterValue = Convert.ToString( updateNewBillingModel.DocId)},
                new Parameters{ ParameterName = "PatientId", ParameterValue = Convert.ToString( updateNewBillingModel.PatientId)},
                new Parameters{ ParameterName = "RecordId", ParameterValue = Convert.ToString( updateNewBillingModel.RecordId)},
                new Parameters{ ParameterName = "NewPatientName", ParameterValue = Convert.ToString( updateNewBillingModel.NewPatientName)},
                new Parameters{ ParameterName = "NewPaymentMode", ParameterValue = Convert.ToString( updateNewBillingModel.NewPaymentMode)},
                new Parameters{ ParameterName = "NewAmount", ParameterValue = Convert.ToString( updateNewBillingModel.NewAmount)},
                new Parameters{ ParameterName = "NewPaymentStatus", ParameterValue = Convert.ToString( updateNewBillingModel.NewPaymentStatus)}
            };
            result = _pDb.InsertUpdateDelete(QueryHelper.editRowDataForBilling, parameters);
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
