using PayXpert.model;
using PayXpert.util;
using System.Data.SqlClient;

namespace PayXpert.service
{
    public class EmployeeService : IEmployeeService
    {
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;
        public EmployeeService()
        {
            sqlConnection = new SqlConnection(DbConnUtil.GetConnectionString());
            cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
        }

        public Employee GetEmployeeById(int employeeId)
        {
            cmd.CommandText = "SELECT * FROM Employee WHERE EmployeeId = @EmployeeId";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Employee employee = new Employee
                    {
                        EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                        Gender = reader["Gender"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Address = reader["Address"].ToString(),
                        Position = reader["Position"].ToString(),
                        JoiningDate = Convert.ToDateTime(reader["JoiningDate"]),
                    };
                    // Check if TerminationDate is DBNull before converting
                    if (reader["TerminationDate"] != DBNull.Value)
                    {
                        employee.TerminationDate = Convert.ToDateTime(reader["TerminationDate"]);
                    }
                    return employee;
                }
                else
                {
                    return null; // Employee not found
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            cmd.CommandText = "SELECT * FROM Employee";

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Employee employee = new Employee
                    {
                        EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                        Gender = reader["Gender"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Address = reader["Address"].ToString(),
                        Position = reader["Position"].ToString(),
                        JoiningDate = Convert.ToDateTime(reader["JoiningDate"])
                    };
                    // Check if TerminationDate is DBNull before converting
                    if (reader["TerminationDate"] != DBNull.Value)
                    {
                        employee.TerminationDate = Convert.ToDateTime(reader["TerminationDate"]);
                    }
                    employees.Add(employee);
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }

            return employees;
        }

        public void AddEmployee(Employee employee)
        {
            try
            {
                sqlConnection.Open();
                cmd.CommandText = "INSERT INTO Employee (FirstName, LastName, DateOfBirth, Gender, Email, PhoneNumber, Address, Position, JoiningDate, TerminationDate)" +
                                  "VALUES (@FirstName, @LastName, @DateOfBirth, @Gender, @Email, @PhoneNumber, @Address, @Position, @JoiningDate, @TerminationDate)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                cmd.Parameters.AddWithValue("@Address", employee.Address);
                cmd.Parameters.AddWithValue("@Position", employee.Position);
                cmd.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);
                // Check if TerminationDate is null and handle accordingly
                if (employee.TerminationDate == DateTime.MinValue)
                {
                    cmd.Parameters.AddWithValue("@TerminationDate", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@TerminationDate", employee.TerminationDate);
                }
                cmd.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                
                Console.WriteLine("Error adding employee: " + ex.Message);                
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public void UpdateEmployee(Employee employee)
        {
            try
            {
                sqlConnection.Open();
                cmd.CommandText = "UPDATE Employee SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, Gender = @Gender, Email = @Email, PhoneNumber = @PhoneNumber, Address = @Address, Position = @Position, JoiningDate = @JoiningDate, TerminationDate = @TerminationDate" + 
                                  " WHERE EmployeeId = @EmployeeId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                cmd.Parameters.AddWithValue("@Address", employee.Address);
                cmd.Parameters.AddWithValue("@Position", employee.Position);
                cmd.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);
                // Check if TerminationDate is null and handle accordingly
                if (employee.TerminationDate == DateTime.MinValue)
                {
                    cmd.Parameters.AddWithValue("@TerminationDate", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@TerminationDate", employee.TerminationDate);
                }

                cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                cmd.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                
                Console.WriteLine("Error updating employee: " + ex.Message);                
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void RemoveEmployee(int employeeId)
        {
            try
            {
                sqlConnection.Open();
                cmd.CommandText = "DELETE FROM Employee WHERE EmployeeId = @EmployeeId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                
                Console.WriteLine("Error removing employee: " + ex.Message);                
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
