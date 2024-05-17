using Project.Model;
using Project.Repository.Interface;
using Project.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class reservationsRepository : IreservationsRepository
    {
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;
        public reservationsRepository()
        {
            sqlConnection = new SqlConnection(DBPropertyUtil.GetConnectionString());
            cmd = new SqlCommand();
        }

        public bool ISAvailableForAllocate(int assid, int employeeid)
        {
            int value = 0;
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select asset_id from reservations where asset_id=@assid and employee_id=@empid";
                cmd.Parameters.Add("@assid", SqlDbType.Int).Value = assid;
                cmd.Parameters.AddWithValue("@empid",employeeid);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) { value = (int)reader["asset_id"]; }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            sqlConnection.Close();
            if (value == assid) { return false; }
            else return true;
        }

        public bool IsReserved(int assetid)
        {
            int value = 0;
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select asset_id from reservations  where asset_id=@id";
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = assetid;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) { value = (int)reader["asset_id"]; }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            sqlConnection.Close();
            if (value == assetid) { return false; }
            else return true;
        }

        public bool ReserveAsset(int assid, int empid, DateTime date1, DateTime date2, DateTime date3, string valstatus)
        {
            int status = 0;
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into reservations values(@assid,@empid,@date1,@date2,@date3,@sts)";

                cmd.Parameters.AddWithValue("@assid", assid);
                cmd.Parameters.AddWithValue("@empid", empid);
                cmd.Parameters.AddWithValue("@date1",date1);
                cmd.Parameters.AddWithValue("@date2", date2);
                cmd.Parameters.AddWithValue("@date3", date3);
                cmd.Parameters.AddWithValue("@sts", valstatus); 
              
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                status = cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            if (status > 0) { return true; }
            else { return false; }
        }

        public bool UpdateeRservationStatus(int assetid, string status)
        {
            int sts = 0;
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "update reservations set [status] =@sts where asset_id=@id";
                cmd.Parameters.AddWithValue("@id", assetid);
                cmd.Parameters.AddWithValue("@sts", status);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                sts = cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            if (sts > 0) { return false; } else { return true; }
        }

        public bool WithdrawReservation(int assid)
        {
            int sts = 0;

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "delete from reservations where asset_id=@assid";
                cmd.Parameters.AddWithValue("@assid", assid);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                sts = cmd.ExecuteNonQuery();
                sqlConnection.Close();

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            if (sts > 0) { return true; }
            else { return false; }

        }

        public List<reservations> GetAllReservation()
        {

            List<reservations> reslist = new List<reservations>();
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select * from reservations";
              
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    reservations res = new reservations();
                    res.Reservation_id = (int)reader["reservation_id"];
                    res.Asset_id.Asset_id = (int)reader["asset_id"];
                    res.Employee_id.Employee_id = (int)reader["employee_id"];
                    res.Reservation_date = (DateTime)reader["reservation_date"];
                    res.Etart_date = (DateTime)reader["start_date"];
                    res.End_date = (DateTime)reader["end_date"];
                    res.Stauts = (string)reader["status"];
                    reslist.Add(res);                 
                }
                sqlConnection.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return reslist;
        }

        public reservations GetReservation()
        {
            
            reservations res = new reservations();
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select top 1 * from reservations order by reservation_id desc";

                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                   
                    res.Reservation_id = (int)reader["reservation_id"];
                    res.Asset_id.Asset_id = (int)reader["asset_id"];
                    res.Employee_id.Employee_id = (int)reader["employee_id"];
                    res.Reservation_date = (DateTime)reader["reservation_date"];
                    res.Etart_date = (DateTime)reader["start_date"];
                    res.End_date = (DateTime)reader["end_date"];
                    res.Stauts = (string)reader["status"];
                  
                }
                sqlConnection.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return res;
        }
    }
}
