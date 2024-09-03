using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace DataAccessLayer
{
    public class clsDataAccess
    {

        public static int AddNewPerson(String NationalNumber, String FName, String SName, String RdName, String LastName,
                                       DateTime DateofBirth, int Gender, String Address, String Phone, String Email,
                                       int Nationality, String ImgPath)
        {
            int PersonID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessConfiguration.ConnectionString);

            String Query = @"INSERT INTO People 
                             Values(@NID, @FName, @SName, @RdName, @LastName, @DateofBirth, @Gender,
                            @Address, @Phone, @Email, @Nationality, @ImgPath);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@NID", NationalNumber);
            Command.Parameters.AddWithValue("@FName", FName);
            Command.Parameters.AddWithValue("@SName", SName);
            Command.Parameters.AddWithValue("@RdName", RdName);
            Command.Parameters.AddWithValue("@LastName", LastName);
            Command.Parameters.AddWithValue("@DateofBirth", DateofBirth);
            Command.Parameters.AddWithValue("@Gender", Gender);
            Command.Parameters.AddWithValue("@Address", Address);
            Command.Parameters.AddWithValue("@Phone", Phone);
            Command.Parameters.AddWithValue("@Email", Email);
            Command.Parameters.AddWithValue("@Nationality", Nationality);
            Command.Parameters.AddWithValue("@ImgPath", ImgPath);

            try
            {
                Connection.Open();

                object Result = Command.ExecuteScalar();


                if (Result != null && int.TryParse(Result.ToString(), out int insertedID))
                {
                    PersonID = insertedID;
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

            return PersonID;
        }

        public static bool DeletePerson(int PersonID)
        {
            int AffectedRows = 0;


            SqlConnection connection = new SqlConnection(clsDataAccessConfiguration.ConnectionString);

            string query = @"Delete People 
                                where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                AffectedRows = command.ExecuteNonQuery();

            }
            catch
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {

                connection.Close();

            }

            return AffectedRows > 0;
        }

        public static bool UpdatePerson(String NationalNumber, String FName, String SName, String RdName, String LastName,
                                       DateTime DateofBirth, int Gender, String Address, String Phone, String Email,
                                       int Nationality, String ImgPath)
        {
            int AffectedRows =0;

            SqlConnection Connection = new SqlConnection(clsDataAccessConfiguration.ConnectionString);

            String Query = @"UPDATE People 
                             SET NationalNo = @NID,
                                 FirstNama = @FName,
                                 SecondName = @SName,
                                 ThirdName = @RdName,
                                 LastName = @LastName,
                                 DateOfBirth = @DateofBirth,
                                 Gender = @Gender,
                                 Address = @Address,
                                 Phone = @Phone,
                                 Email = @Email,
                                 Nationality = @Nationality,
                                 ImagePath = @ImgPath);";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@NID", NationalNumber);
            Command.Parameters.AddWithValue("@FName", FName);
            Command.Parameters.AddWithValue("@SName", SName);
            Command.Parameters.AddWithValue("@RdName", RdName);
            Command.Parameters.AddWithValue("@LastName", LastName);
            Command.Parameters.AddWithValue("@DateofBirth", DateofBirth);
            Command.Parameters.AddWithValue("@Gender", Gender);
            Command.Parameters.AddWithValue("@Address", Address);
            Command.Parameters.AddWithValue("@Phone", Phone);
            Command.Parameters.AddWithValue("@Email", Email);
            Command.Parameters.AddWithValue("@Nationality", Nationality);
            Command.Parameters.AddWithValue("@ImgPath", ImgPath);

            try
            {
                Connection.Open();

                AffectedRows = Command.ExecuteNonQuery();
            }

            catch
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                Connection.Close();
            }

            return AffectedRows >0;
        }

        public static DataTable GetAllPeople() 
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataAccessConfiguration.ConnectionString);

            String Query = "SELECT * FROM People;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.HasRows)

                {
                    dt.Load(Reader);
                }

                Reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }


            return dt;
        }


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
