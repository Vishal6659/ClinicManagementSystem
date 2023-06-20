using ClinicManagementSystem.Models;
using Npgsql;
using System.Data;


namespace ClinicManagementSystem.Helper
{
    public class PostgresDbHelper
    {
        public int InsertUpdateDelete(string query, List<Models.Parameters> dbDataParameters)
        {
            try
            {
                using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection(Convert.ToString(Program.configuration["ClinicConnectionString"])))
                {
                    npgsqlConnection.Open();
                    using (var npgsqlCommand = new NpgsqlCommand(query, npgsqlConnection))
                    {
                        npgsqlCommand.CommandText = query;
                        npgsqlCommand.CommandTimeout = 36000;
                        if (dbDataParameters != null)
                        {
                            foreach (var item in dbDataParameters)
                            {
                                var parameter = npgsqlCommand.CreateParameter();
                                parameter.ParameterName = item.ParameterName;
                                parameter.NpgsqlValue = item.ParameterValue ?? "";
                                npgsqlCommand.Parameters.Add(parameter);
                            }
                        }
                        npgsqlCommand.Prepare();
                        return npgsqlCommand.ExecuteNonQuery();
                    }
                }                           
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public string InsertUpdateDelete(List<string> queryList)
        {
            int i = 0;
            string result = string.Empty;
            try
            {
                using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection(Convert.ToString(Program.configuration["ClinicConnectionString"])))
                {
                    npgsqlConnection.Open();
                    foreach (var query in queryList)
                    {
                        using (var npgsqlCommand = new NpgsqlCommand(query, npgsqlConnection))
                        {
                            npgsqlCommand.CommandText = query;
                            npgsqlCommand.CommandTimeout = 36000;
                            npgsqlCommand.Prepare();
                            i = npgsqlCommand.ExecuteNonQuery();
                            if (i > 0)
                            {
                                result = "Success";
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                result = "Exception :" + ex.Message;
            }
            return result;
        }

        public DataTable SelectMethod(string query, List<Parameters>? dbDataParameters = null)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection(Convert.ToString(Program.configuration["ClinicConnectionString"])))
                {
                    npgsqlConnection.Open();
                    using (var npgsqlCommand = new NpgsqlCommand(query, npgsqlConnection))
                    {
                        npgsqlCommand.CommandText = query;
                        npgsqlCommand.CommandTimeout = 36000;
                        if (dbDataParameters != null)
                        {
                            foreach (var item in dbDataParameters)
                            {
                                var parameter = npgsqlCommand.CreateParameter();
                                parameter.ParameterName = item.ParameterName;
                                parameter.Value = item.ParameterValue ?? "";
                                npgsqlCommand.Parameters.Add(parameter);
                            }
                        }
                        npgsqlCommand.Prepare();
                        NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter(npgsqlCommand);
                        npgsqlDataAdapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
