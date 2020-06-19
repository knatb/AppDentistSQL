using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDentistSQL.Models
{
    public class PatientModel
    {
        string ConnectionString = "Server=tcp:azuresqldbklopezserver.database.windows.net,1433;Initial Catalog=azuredentist;Persist Security Info=False;User ID=klopez;Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public int IDPatient { get; set; }
        public string Name { get; set; }
        public string PictureBase64 { get; set; }
        public string Process { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public List<PatientModel> GetAll()
        {
            List<PatientModel> list = new List<PatientModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string tsql = "getAllPatients";
                    using (SqlCommand cmd = new SqlCommand(tsql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new PatientModel
                                {
                                    IDPatient = (int)reader["ID"],
                                    Name = reader["Name"].ToString(),
                                    PictureBase64 = reader["PictureBase64"].ToString(),
                                    Process = reader["Process"].ToString(),
                                    Latitude = (double)reader["Latitude"],
                                    Longitude = (double)reader["Longitude"]
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

        public PatientModel Get(int id)
        
        {
            PatientModel patient = new PatientModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string tsql = "SELECT * FROM Patients WHERE Patients.ID = @IDPatient;";
                    using (SqlCommand cmd = new SqlCommand(tsql, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@IDPatient", id));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                patient = new PatientModel
                                {
                                    IDPatient = (int)reader["ID"],
                                    Name = reader["Name"].ToString(),
                                    PictureBase64 = reader["PictureBase64"].ToString(),
                                    Process = reader["Process"].ToString(),
                                    Latitude = (double)reader["Latitude"],
                                    Longitude = (double)reader["Longitude"]
                                };
                            }
                        }
                    }
                }
                return patient;
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
                    using (SqlCommand cmd = new SqlCommand("createPatient", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", Name);
                        cmd.Parameters.AddWithValue("@PictureBase64", PictureBase64);
                        cmd.Parameters.AddWithValue("@Process", Process);
                        cmd.Parameters.AddWithValue("@Latitude", Latitude);
                        cmd.Parameters.AddWithValue("@Longitude", Longitude);
                        id = cmd.ExecuteNonQuery();
                    }
                }
                if (id != null && id.ToString().Length > 0)
                {
                    return new ApiResponse
                    {
                        IsSuccess = true,
                        Result = int.Parse(id.ToString()),
                        Message = "EXCELSIOR, Paciente creado"
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
                    using (SqlCommand cmd = new SqlCommand("updatePatient", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@Name", Name);
                        cmd.Parameters.AddWithValue("@PictureBase64", PictureBase64);
                        cmd.Parameters.AddWithValue("@Process", Process);
                        cmd.Parameters.AddWithValue("@Latitude", Latitude);
                        cmd.Parameters.AddWithValue("@Longitude", Longitude);
                        id = cmd.ExecuteNonQuery();
                    }
                }
                return new ApiResponse
                {
                    IsSuccess = true,
                    Result = IDPatient,
                    Message = "EXCELSIOR, Paciente actualizado"
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
                    string tsql = "deletePatient";

                    using (SqlCommand cmd = new SqlCommand(tsql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDPatient", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                return new ApiResponse
                {
                    IsSuccess = true,
                    Result = id,
                    Message = "EXCELSIOR, Paciente eliminado"
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
