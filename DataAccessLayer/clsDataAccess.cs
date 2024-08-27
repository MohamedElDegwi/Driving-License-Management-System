using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;

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

            catch
            {
                //Console.WriteLine("Error: " + ex.Message);
            }

            finally
            {
                Connection.Close();
            }

            return AppID;
        }
    
        
        public static bool IsHaveLicenseFromSameClass(int DriverID, int LicenseClass)
        {
            bool HaveLicense = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessConfiguration.ConnectionString);

            String Query = @"SELECT Found = 1 from Licenses WHERE DriverID = @DriverID and LicenseClass = @LicenseClass and IsActive = 1;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@LicenseClass", LicenseClass);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                HaveLicense = reader.HasRows;

                reader.Close();
            }
            catch
            {
                //Console.WriteLine("Error: " + ex.Message);
                HaveLicense = false;
            }
            finally
            {
                Connection.Close();
            }

            return HaveLicense;
        }


        public static bool IsHaveUncompletedAppFromSameType(int PersonID, int AppTypeID)
        {
            bool HaveUncomApp = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessConfiguration.ConnectionString);

            String Query = @"SELECT Found = 1 from Applications WHERE ApplicantPersonID = @PersonID and ApplicationTypeID = @ApplicationTypeID
                             and ApplicationStatus != 3;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@ApplicationTypeID", AppTypeID);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                HaveUncomApp = reader.HasRows;

                reader.Close();
            }
            catch
            {
                //Console.WriteLine("Error: " + ex.Message);
                HaveUncomApp = false;
            }
            finally
            {
                Connection.Close();
            }

            return HaveUncomApp;
        }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    }
}
