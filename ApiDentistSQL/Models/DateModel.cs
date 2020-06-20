using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDentistSQL.Models
{
    public class DateModel
    {
        string ConnectionString = "Server=tcp:azuresqldbklopezserver.database.windows.net,1433;Initial Catalog=azuredentist;Persist Security Info=False;User ID=klopez;Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public int IDDate { get; set; }
        public DateTime DayDate { get; set; }
        public double Cost { get; set; }

        public List<DateModel> GetAll()
        {
            List<DateModel> list = new List<DateModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string tsql = "getAllDates";
                    using (SqlCommand cmd = new SqlCommand(tsql, conn))
                    {
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

        public DateModel Get(int id)
        {
            DateModel date = new DateModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string tsql = "getDateByID";
                    using (SqlCommand cmd = new SqlCommand(tsql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDDate", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                date = new DateModel
                                {
                                    IDDate = (int)reader["ID"],
                                    DayDate = (DateTime)reader["DayDate"],
                                    Cost = (double)reader["Cost"],
                                };
                            }
                        }
                    }
                }
                return date;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ApiResponse Insert()
        {
            object id;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("createDate", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DayDate", DayDate);
                        cmd.Parameters.AddWithValue("@Cost", Cost);
                        id = cmd.ExecuteNonQuery();
                    }
                }
                if (id != null && id.ToString().Length > 0)
                {
                    return new ApiResponse
                    {
                        IsSuccess = true,
                        Result = int.Parse(id.ToString()),
                        Message = "EXCELSIOR, Cita creada"
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

        public ApiResponse Update(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("updateDate", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@DayDate", DayDate);
                        cmd.Parameters.AddWithValue("@Cost", Cost);
                        id = cmd.ExecuteNonQuery();
                    }
                }
                return new ApiResponse
                {
                    IsSuccess = true,
                    Result = IDDate,
                    Message = "EXCELSIOR, Cita actualizada"
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

        public ApiResponse Delete(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string tsql = "deleteDate";

                    using (SqlCommand cmd = new SqlCommand(tsql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDDate", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                return new ApiResponse
                {
                    IsSuccess = true,
                    Result = id,
                    Message = "EXCELSIOR, Cita eliminada"
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