using Project.Model;
using Project.Repository.Interface;
using Project.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Project.Repository
{
    public class employeesRepository : IemployeesRepository
    {
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;

        public employeesRepository()
        {
            sqlConnection = new SqlConnection(DBPropertyUtil.GetConnectionString());
            cmd = new SqlCommand();
        }

        public bool AddEmployee(employees emp)
        {
            int status = 0;
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into employees values(@name,@dep,@mail,@password)";

                cmd.Parameters.AddWithValue("@name", emp.Name);
                cmd.Parameters.AddWithValue("@dep", emp.Department);
                cmd.Parameters.AddWithValue("@mail",emp.Email);
                cmd.Parameters.AddWithValue("@password", emp.Password);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                status = cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            if (status > 0) { return true; }
            else { return false; }
        }

        public List<employees> GetAllEmployees()
        {
           List<employees> emplist=new List<employees>();
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select * from employees";
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    employees emp = new employees();
                    emp.Employee_id = (int)reader["employee_id"];
                    emp.Name = (string)reader["name"];
                    emp.Department = (string)reader["department"];
                    emp.Email = (string)reader["email"];
                    emplist.Add(emp);
                }
                sqlConnection.Close();
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
            return emplist;
        }

        public employees GetEmployee(int empid)
        {

           employees emp = new employees();
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select * from employees where employee_id=@id";
                cmd.Parameters.AddWithValue("@id", empid);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    
                    emp.Employee_id = (int)reader["employee_id"];
                    emp.Name = (string)reader["name"];
                    emp.Department = (string)reader["department"];
                    emp.Email = (string)reader["email"];
                   
                }
                sqlConnection.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return emp;
        }


        public employees GetEmployee()
        {

            employees emp = new employees();
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select top 1 *  from employees order by employee_id desc ";
               
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    emp.Employee_id = (int)reader["employee_id"];
                    emp.Name = (string)reader["name"];
                    emp.Department = (string)reader["department"];
                    emp.Email = (string)reader["email"];

                }
                sqlConnection.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return emp;
        }

        public bool IsEmployeeIDAvailabe(int cusid)
        {

            int value = 0;
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select  employee_id from employees where employee_id=@id";
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = cusid;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) { value = (int)reader["employee_id"]; }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            sqlConnection.Close();
            if (value == cusid) { return false; }
            else return true;
        }

        public bool IsManager(int empid)
        {
            int value = 0;
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select employee_id from employees where department='Manager' and employee_id=@id";
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = empid;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) { value = (int)reader["employee_id"]; }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            sqlConnection.Close();
            if (value == empid) { return false; }
            else
            {
                return true;
            }
        }

        public bool Login(int empid, string password)
        {
            string dbpassword="";
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select [password] from employees where employee_id=@id";
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = empid;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read()) { dbpassword = (string)reader["password"]; }
                sqlConnection.Close ();
                
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            if(password==dbpassword) { return true; }
            else
            {  return false;}


        }

        public bool RemoveEmployee(int empid)
        {
            int status = 0;
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "delete employees where employee_id=@id";

                cmd.Parameters.AddWithValue("@id", empid);

                
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                status = cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            if (status > 0) { return true; }
            else { return false; }
        }

        public bool UpdateEmployee(employees emp)
        {
            int status = 0;
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "update employees set name =@name,department=@dep," +
                    "email=@email,[password]=@password where employee_id=@id";

                cmd.Parameters.AddWithValue("@id", emp.Employee_id);

                cmd.Parameters.AddWithValue("@name", emp.Name);
                cmd.Parameters.AddWithValue("@dep", emp.Department);
                cmd.Parameters.AddWithValue("@email", emp.Email);
                cmd.Parameters.AddWithValue("@password", emp.Password);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                status = cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            if (status > 0) { return true; }
            else { return false; }
        }
    }
}
