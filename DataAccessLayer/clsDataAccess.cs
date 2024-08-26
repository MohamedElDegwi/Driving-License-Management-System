using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class clsDataAccess
    {
        public static int AddAppForService(int AppPersonID, DateTime AppDate, int AppType,
            short AppStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            int AppID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessConfiguration.ConnectionString);

            string Query = @"INSERT INTO Applications
                             VALUES (@AppPersonID, @AppDate, @AppType, @AppStatus, @LastStatusDate, @PaidFees, @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@AppPersonID", AppPersonID);
            Command.Parameters.AddWithValue("@AppDate", AppDate);
            Command.Parameters.AddWithValue("@AppType", AppType);
            Command.Parameters.AddWithValue("@AppStatus", AppStatus);
            Command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int insertedID))
                {
                    AppID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
            }

            finally
            {
                Connection.Close();
            }

            return AppID;
        }
    
        
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    }
}
