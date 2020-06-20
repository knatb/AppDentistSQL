using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDentistSQL.Models
{
    public class DatePatientModel
    {
        string ConnectionString = "Server=tcp:azuresqldbklopezserver.database.windows.net,1433;Initial Catalog=azuredentist;Persist Security Info=False;User ID=klopez;Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public int idDate { get; set; }
        public int idPatient { get; set; }
        public PatientModel Patient { get; set; }
        public DateModel Dates { get; set; }

        public List<DatePatientModel> GetAll()
        {
            List<DatePatientModel> list = new List<DatePatientModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string tsql = "SELECT * FROM DatePatient";
                    using (SqlCommand cmd = new SqlCommand(tsql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new DatePatientModel
                                {
                                    idDate = (int)reader["idDate"],
                                    idPatient = (int)reader["idPatient"]
                                });
                            }
                        }
                    }
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<DateModel> GetDates(int id)
        {
            List<DateModel> list = new List<DateModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    /*cmd.CommandType = System.Data.CommandType.StoredProcedure;
                      cmd.Parameters.Add(new SqlParameter("@IDProd", id));*/
                    string tsql = "getDatesFromPatient";
                    using (SqlCommand cmd = new SqlCommand(tsql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@IDPatient", id));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new DateModel
                                {
                                    IDDate = (int)reader["ID"],
                                    DayDate = (DateTime)reader["DayDate"],
                                    Cost = (double)reader["Cost"],
                                });
                            }
                        }
                    }
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ApiResponse InsertDateToPatient()
        {
            object id;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("addDateToPatient", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDPatient", idPatient);
                        cmd.Parameters.AddWithValue("@IDDate", idDate);
                        id = cmd.ExecuteNonQuery();
                    }
                }
                if (id != null && id.ToString().Length > 0)
                {
                    return new ApiResponse
                    {
                        IsSuccess = true,
                        Result = int.Parse(id.ToString()),
                        Message = "EXCELSIOR, Cita asignada al paciente: " + idPatient
                    };
                    //return int.Parse(id.ToString());
                }
                else
                {
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        Result = 0,
                        Message = "ERROR"
                    };
                    //return 0; 
                }
            }
            catch (Exception exc)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Result = null,
                    Message = exc.Message
                };
                throw;
            }
        }

        public ApiResponse DeleteDateFromPatient(DatePatientModel dp)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string tsql = "deleteDateToPatient";

                    using (SqlCommand cmd = new SqlCommand(tsql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDDate", dp.idDate);
                        cmd.Parameters.AddWithValue("@IDPatient", dp.idPatient);
                        cmd.ExecuteNonQuery();
                    }
                }
                return new ApiResponse
                {
                    IsSuccess = true,
                    Result = dp.idDate,
                    Message = "EXCELSIOR, Cita removida del Paciente: " + dp.idPatient
                };
            }
            catch (Exception exc)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Result = null,
                    Message = exc.Message
                };
                throw;
            }
        }
    }
}
