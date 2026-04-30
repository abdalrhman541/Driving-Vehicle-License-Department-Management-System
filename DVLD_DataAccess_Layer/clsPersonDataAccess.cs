using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Layer
{
    public class clsPersonDataAccess
    {

        // Create
        public static int AddPerson(string NationalNumber, string FirstName, string SecondName, string ThirdName,
            string LastName, DateTime DateOfBirth, short Gendor, string Address, string Phone, string Email,
            int NationalityCountryID, string ImagePath)
        {
            int PersonID = -1;

            SqlConnection con = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"INSERT INTO [dbo].[People]
           ([NationalNo]
           ,[FirstName]
           ,[SecondName]
           ,[ThirdName]
           ,[LastName]
           ,[DateOfBirth]
           ,[Gendor]
           ,[Address]
           ,[Phone]
           ,[Email]
           ,[NationalityCountryID]
           ,[ImagePath])
     VALUES
           (@NationalNumber
           ,@FirstName
           ,@SecondName
           ,@ThirdName
           ,@LastName
           ,@DateOfBirth
           ,@Gendor
           ,@Address
           ,@Phone
           ,@Email
           ,@NationalityCountryID
           ,@ImagePath);
               SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@NationalNumner", NationalNumber);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            if (ThirdName != "")
            {
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            }
            else
            {
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);
            }
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            if (Email != "")
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            if (ImagePath != "")
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                con.Open();

                object Result = command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {
                    PersonID = InsertedID;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }

            return PersonID;

        }

        // Select // Read // Find 

        // find one person Knowing one column
        public static bool FindPersonByID(int PersonID, ref string NationalNumber, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref short Gendor,
            ref string Address, ref string Phone, ref string Email,
            ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT[PersonID]
                                  ,[NationalNo]
                                  ,[FirstName]
                                  ,[SecondName]
                                  ,[ThirdName]
                                  ,[LastName]
                                  ,[DateOfBirth]
                                  ,[Gendor]
                                  ,[Address]
                                  ,[Phone]
                                  ,[Email]
                                  ,[NationalityCountryID]
                                  ,[ImagePath]
                              FROM [dbo].[People]
                            WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    NationalNumber = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    if (reader["ThirdName"] != System.DBNull.Value)
                        ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (short)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    if (reader["Email"] != System.DBNull.Value)
                        Email = (string)reader["Email"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    if (reader["ImagePath"] != System.DBNull.Value)
                        ImagePath = (string)reader["ImagePath"];

                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            { connection.Close(); }



            return isFound;
        }

        public static string FindPersonByNationalNumber(string NationalNumber, ref int PersonID, ref string FirstName, ref string SecondName,
           ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref short Gendor,
           ref string Address, ref string Phone, ref string Email,
           ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT[PersonID]
                                  ,[NationalNo]
                                  ,[FirstName]
                                  ,[SecondName]
                                  ,[ThirdName]
                                  ,[LastName]
                                  ,[DateOfBirth]
                                  ,[Gendor]
                                  ,[Address]
                                  ,[Phone]
                                  ,[Email]
                                  ,[NationalityCountryID]
                                  ,[ImagePath]
                              FROM [dbo].[People]
                            WHERE NationalNo = @NationalNo";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNumber);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    PersonID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    if (reader["ThirdName"] != System.DBNull.Value)
                        ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (short)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    if (reader["Email"] != System.DBNull.Value)
                        Email = (string)reader["Email"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    if (reader["ImagePath"] != System.DBNull.Value)
                        ImagePath = (string)reader["ImagePath"];

                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            { connection.Close(); }



            return isFound;
        }

        public static bool FindPersonByFirstName(string FirstName, ref string NationalNumber, ref int PersonID, ref string SecondName,
            ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref short Gendor,
            ref string Address, ref string Phone, ref string Email,
            ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT[PersonID]
                           ,[NationalNo]
                           ,[FirstName]
                           ,[SecondName]
                           ,[ThirdName]
                           ,[LastName]
                           ,[DateOfBirth]
                           ,[Gendor]
                           ,[Address]
                           ,[Phone]
                           ,[Email]
                           ,[NationalityCountryID]
                           ,[ImagePath]
                       FROM [dbo].[People]
                     WHERE FirstName = @FirstName";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@FirstName", FirstName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    NationalNumber = (string)reader["NationalNo"];
                    PersonID = (int)reader["PersonID"];
                    SecondName = (string)reader["SecondName"];
                    if (reader["ThirdName"] != System.DBNull.Value)
                        ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (short)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    if (reader["Email"] != System.DBNull.Value)
                        Email = (string)reader["Email"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    if (reader["ImagePath"] != System.DBNull.Value)
                        ImagePath = (string)reader["ImagePath"];

                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            { connection.Close(); }



            return isFound;
        }

        public static bool FindPersonBySecondName(string SecondName, ref string NationalNumber, ref string FirstName, ref int PersonID,
            ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref short Gendor,
            ref string Address, ref string Phone, ref string Email,
            ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT[PersonID]
                           ,[NationalNo]
                           ,[FirstName]
                           ,[SecondName]
                           ,[ThirdName]
                           ,[LastName]
                           ,[DateOfBirth]
                           ,[Gendor]
                           ,[Address]
                           ,[Phone]
                           ,[Email]
                           ,[NationalityCountryID]
                           ,[ImagePath]
                       FROM [dbo].[People]
                     WHERE SecondName = @SecondName";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@SecondName", SecondName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    NationalNumber = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    PersonID = (int)reader["PersonID "];
                    if (reader["ThirdName"] != System.DBNull.Value)
                        ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (short)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    if (reader["Email"] != System.DBNull.Value)
                        Email = (string)reader["Email"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    if (reader["ImagePath"] != System.DBNull.Value)
                        ImagePath = (string)reader["ImagePath"];

                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            { connection.Close(); }



            return isFound;
        }

        public static bool FindPersonByThirdName(string ThirdName, ref string NationalNumber, ref string FirstName, ref string SecondName,
            ref int PersonID, ref string LastName, ref DateTime DateOfBirth, ref short Gendor,
            ref string Address, ref string Phone, ref string Email,
            ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT[PersonID]
                                  ,[NationalNo]
                                  ,[FirstName]
                                  ,[SecondName]
                                  ,[ThirdName]
                                  ,[LastName]
                                  ,[DateOfBirth]
                                  ,[Gendor]
                                  ,[Address]
                                  ,[Phone]
                                  ,[NationalityCountryID]
                                  ,[ImagePath]
                              FROM [dbo].[People]
                            WHERE ThirdName = @ThirdName";
            SqlCommand command = new SqlCommand(Query, connection);
            if (ThirdName != "")
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    NationalNumber = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    PersonID = (int)reader["PersonID"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (short)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    if (reader["Email"] != System.DBNull.Value)
                        Email = (string)reader["Email"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    if (reader["ImagePath"] != System.DBNull.Value)
                        ImagePath = (string)reader["ImagePath"];

                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            { connection.Close(); }



            return isFound;
        }


        public static bool FindPersonByLastName(string ThirdName, ref string NationalNumber, ref string FirstName, ref string SecondName,
           ref int PersonID, ref string LastName, ref DateTime DateOfBirth, ref short Gendor,
           ref string Address, ref string Phone, ref string Email,
           ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT[PersonID]
                                  ,[NationalNo]
                                  ,[FirstName]
                                  ,[SecondName]
                                  ,[ThirdName]
                                  ,[LastName]
                                  ,[DateOfBirth]
                                  ,[Gendor]
                                  ,[Address]
                                  ,[Phone]
                                  ,[Email]
                                  ,[NationalityCountryID]
                                  ,[ImagePath]
                              FROM [dbo].[People]
                            WHERE LastName = @LastName";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ThirdName", ThirdName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    NationalNumber = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    if (reader["ThirdName"] != System.DBNull.Value)
                        ThirdName = (string)reader["ThirdName"];
                    PersonID = (int)reader["PersonID"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (short)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    if (reader["Email"] != System.DBNull.Value)
                        Email = (string)reader["Email"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    if (reader["ImagePath"] != System.DBNull.Value)
                        ImagePath = (string)reader["ImagePath"];

                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            { connection.Close(); }



            return isFound;
        }


        public static bool FindPersonByDateOfBirth(DateTime DateOfBirth, ref string NationalNumber, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref int PersonID, ref short Gendor,
           ref string Address, ref string Phone, ref string Email,
           ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT[PersonID]
                                  ,[NationalNo]
                                  ,[FirstName]
                                  ,[SecondName]
                                  ,[ThirdName]
                                  ,[LastName]
                                  ,[DateOfBirth]
                                  ,[Gendor]
                                  ,[Address]
                                  ,[Phone]
                                  ,[Email]
                                  ,[NationalityCountryID]
                                  ,[ImagePath]
                              FROM [dbo].[People]
                            WHERE DateOfBirth = @DateOfBirth";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    NationalNumber = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    if (reader["ThirdName"] != System.DBNull.Value)
                        ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    PersonID = (int)reader["PersonID"];
                    Gendor = (short)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    if (reader["Email"] != System.DBNull.Value)
                        Email = (string)reader["Email"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    if (reader["ImagePath"] != System.DBNull.Value)
                        ImagePath = (string)reader["ImagePath"];

                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            { connection.Close(); }



            return isFound;
        }


        public static bool FindPersonByGendor(short Gendor, ref string NationalNumber, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref int PersonID,
            ref string Address, ref string Phone, ref string Email,
            ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT[PersonID]
                                  ,[NationalNo]
                                  ,[FirstName]
                                  ,[SecondName]
                                  ,[ThirdName]
                                  ,[LastName]
                                  ,[DateOfBirth]
                                  ,[Gendor]
                                  ,[Address]
                                  ,[Phone]
                                  ,[Email]
                                  ,[NationalityCountryID]
                                  ,[ImagePath]
                              FROM [dbo].[People]
                            WHERE Gendor = @Gendor";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@Gendor", Gendor);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    NationalNumber = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    if (reader["ThirdName"] != System.DBNull.Value)
                        ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    PersonID = (int)reader["PersonID"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    if (reader["Email"] != System.DBNull.Value)
                        Email = (string)reader["Email"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    if (reader["ImagePath"] != System.DBNull.Value)
                        ImagePath = (string)reader["ImagePath"];

                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            { connection.Close(); }



            return isFound;
        }

        public static bool FindPersonByAddress(string Address, ref string NationalNumber, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref short Gendor,
            ref int PersonID, ref string Phone, ref string Email,
            ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT[PersonID]
                          ,[NationalNo]
                          ,[FirstName]
                          ,[SecondName]
                          ,[ThirdName]
                          ,[LastName]
                          ,[DateOfBirth]
                          ,[Gendor]
                          ,[Address]
                          ,[Phone]
                          ,[Email]
                          ,[NationalityCountryID]
                          ,[ImagePath]
                      FROM [dbo].[People]
                    WHERE Address = @Address";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@Address", Address);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    NationalNumber = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    if (reader["ThirdName"] != System.DBNull.Value)
                        ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (short)reader["Gendor"];
                    PersonID = (int)reader["PersonID"];
                    Phone = (string)reader["Phone"];
                    if (reader["Email"] != System.DBNull.Value)
                        Email = (string)reader["Email"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    if (reader["ImagePath"] != System.DBNull.Value)
                        ImagePath = (string)reader["ImagePath"];

                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            { connection.Close(); }



            return isFound;
        }

        public static bool FindPersonByPhone(string Phone, ref string NationalNumber, ref string FirstName, ref string SecondName,
    ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref short Gendor,
    ref string Address, ref int PersonID, ref string Email,
    ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT[PersonID]
                                  ,[NationalNo]
                                  ,[FirstName]
                                  ,[SecondName]
                                  ,[ThirdName]
                                  ,[LastName]
                                  ,[DateOfBirth]
                                  ,[Gendor]
                                  ,[Address]
                                  ,[Phone]
                                  ,[Email]
                                  ,[NationalityCountryID]
                                  ,[ImagePath]
                              FROM [dbo].[People]
                            WHERE Phone = @Phone";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@Phone", Phone);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    NationalNumber = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    if (reader["ThirdName"] != System.DBNull.Value)
                        ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (short)reader["Gendor"];
                    Address = (string)reader["Address"];
                    PersonID = (int)reader["PersonID"];
                    if (reader["Email"] != System.DBNull.Value)
                        Email = (string)reader["Email"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    if (reader["ImagePath"] != System.DBNull.Value)
                        ImagePath = (string)reader["ImagePath"];

                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            { connection.Close(); }



            return isFound;
        }

        public static bool FindPersonByEmail(string Email, ref string NationalNumber, ref string FirstName, ref string SecondName,
    ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref short Gendor,
    ref string Address, ref string Phone, ref int PersonID,
    ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT[PersonID]
                                  ,[NationalNo]
                                  ,[FirstName]
                                  ,[SecondName]
                                  ,[ThirdName]
                                  ,[LastName]
                                  ,[DateOfBirth]
                                  ,[Gendor]
                                  ,[Address]
                                  ,[Phone]
                                  ,[Email]
                                  ,[NationalityCountryID]
                                  ,[ImagePath]
                              FROM [dbo].[People]
                            WHERE Email = @Email";
            SqlCommand command = new SqlCommand(Query, connection);
            if (Email != null)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    NationalNumber = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    if (reader["ThirdName"] != System.DBNull.Value)
                        ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (short)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    PersonID = (int)reader["PersonID"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    if (reader["ImagePath"] != System.DBNull.Value)
                        ImagePath = (string)reader["ImagePath"];

                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            { connection.Close(); }



            return isFound;
        }


        public static bool FindPersonByCountryID(int NationalityCountryID, ref string NationalNumber, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref short Gendor,
            ref string Address, ref string Phone, ref string Email,
            ref int PersonID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT[PersonID]
                                  ,[NationalNo]
                                  ,[FirstName]
                                  ,[SecondName]
                                  ,[ThirdName]
                                  ,[LastName]
                                  ,[DateOfBirth]
                                  ,[Gendor]
                                  ,[Address]
                                  ,[Phone]
                                  ,[Email]
                                  ,[NationalityCountryID]
                                  ,[ImagePath]
                              FROM [dbo].[People]
                            WHERE NationalityCountryID = @NationalityCountryID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    NationalNumber = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    if (reader["ThirdName"] != System.DBNull.Value)
                        ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (short)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    if (reader["Email"] != System.DBNull.Value)
                        Email = (string)reader["Email"];
                    PersonID = (int)reader["PersonID"];
                    if (reader["ImagePath"] != System.DBNull.Value)
                        ImagePath = (string)reader["ImagePath"];

                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            { connection.Close(); }



            return isFound;
        }


        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"Select * from People";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                connection.Close();
            }


            return dt;
        }


        public static bool IsPersonExist(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"Select found =1  from People 
                         where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
                isFound = false;
            }
            finally
            {
                connection.Close();
            }


            return isFound;
        }

        public static bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"Select found =1  from People 
                         where NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
                isFound = false;
            }
            finally
            {
                connection.Close();
            }


            return isFound;
        }

        // Update

        public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, DateTime DateOfBirth,
            short Gendor, string Address, string Phone, string Email,
            int NationalityCountryID, string ImagePath)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"UPDATE [dbo].[People]
   SET [NationalNo] = @NationalNo 
      ,[FirstName] = @FirstName 
      ,[SecondName] = @SecondName 
      ,[ThirdName] = @ThirdName 
      ,[LastName] = @LastName 
      ,[DateOfBirth] = @DateOfBirth 
      ,[Gendor] = @Gendor 
      ,[Address] = @Address 
      ,[Phone] = @Phone 
      ,[Email] = @Email 
      ,[NationalityCountryID] = @NationalityCountryID 
      ,[ImagePath] = @ImagePath 
 WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            if (ThirdName != "")
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            if (Email != "")
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            if (ImagePath != "")
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);


            try
            {
                connection.Open();

                rowAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }


            return (rowAffected > 0);

        }




        // Delete

        public static bool DeletePerson(int PersonID)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"Delete people
                                Where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);


            try
            {
                connection.Open();

                rowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }


            return (rowAffected > 0);

        }

    }
}
