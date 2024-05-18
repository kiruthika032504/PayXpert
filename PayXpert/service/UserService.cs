using PayXpert.exception;
using PayXpert.util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.service
{
    public class UserService : IUserService
    {
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;

        public UserService()
        {
            sqlConnection = new SqlConnection(DbConnUtil.GetConnectionString());
            cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
        }

        public bool RegisterUser(string username, string password)
        {
            try
            {
                sqlConnection.Open();
                cmd.CommandText = "INSERT INTO Users (Username, password) VALUES (@Username, @Password)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch(InvalidInputException ex)
            {
                Console.WriteLine($"User does not exist! Register as new User! {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to register user, Try again!" + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool IsUsernameExists(string username)
        {
            try
            {
                sqlConnection.Open();
                cmd.CommandText = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Username", username);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(new Exception($"Username does not exists {ex.Message}"));
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool IsLoginValid(string username, string password)
        {
            try
            {
                sqlConnection.Open();
                cmd.CommandText = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(new Exception($"Invalid User. Register as new user {ex.Message}"));
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }

        }
    }
}
