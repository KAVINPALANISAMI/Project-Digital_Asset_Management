using Project.Model;
using Project.Repository.Interface;
using Project.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class maintenance_recordsRepository : Imaintenance_recordsRepository
    {
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;
        public maintenance_recordsRepository()
        {
            sqlConnection = new SqlConnection(DBPropertyUtil.GetConnectionString());
            cmd = new SqlCommand();
        }
        public bool PerformMaintenance(int assetid, DateTime date, string description, decimal cost)
        {
           
            int status = 0;
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into maintenance_records values(@id,@date,@dec,@cost,'Under Maintenance')";                           
                cmd.Parameters.AddWithValue("@id", assetid);
                cmd.Parameters.AddWithValue ("@date", date);
                cmd.Parameters.AddWithValue ("@dec", description);
                cmd.Parameters.AddWithValue("@cost", cost);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                status = cmd.ExecuteNonQuery();
                Console.WriteLine(status);
                sqlConnection.Close();                        
            }
             catch (Exception ex) { Console.WriteLine(ex.Message); }
            if (status > 0) { return true; }
            else { return false; }
            
        }

        public List<maintenance_records> ViewPerformMaintenance()
        {
            List<maintenance_records> list = new List<maintenance_records>();
            
            try
            {
               
                cmd.Parameters.Clear();
                cmd.CommandText = "select * from maintenance_records";
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    maintenance_records rec = new maintenance_records();
                    rec.Maintenance_id = (int)reader["maintenance_id"];
                    rec.Asset_id.Asset_id = (int)reader["asset_id"];
                    rec.Maintenance_date = (DateTime)reader["maintenance_date"];
                    rec.Description = (string)reader["description"];
                    rec.Cost = (decimal)reader["cost"];
                    rec.Status = (string)reader["status"];
                    list.Add(rec);
                }
                sqlConnection.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return list;
        }

        public bool MaintenanceCompleted(int assetid)
        {

            int sts = 0;
           
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "update maintenance_records set [status]='Maintenance Completed' where asset_id=@id";
                cmd.Parameters.AddWithValue("@id", assetid);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                sts = cmd.ExecuteNonQuery();
                sqlConnection.Close();

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            if (sts > 0) { return true; }
            else { return false; }
        }

        public maintenance_records GetAddedMaintenance_Records()
        {

            maintenance_records rec = new maintenance_records();

            try
            {

                cmd.Parameters.Clear();
                cmd.CommandText = "select top 1 * from  maintenance_records order by maintenance_id desc";
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    
                    rec.Maintenance_id = (int)reader["maintenance_id"];                   
                    rec.Asset_id.Asset_id = (int)reader["asset_id"];                    
                    rec.Maintenance_date = (DateTime)reader["maintenance_date"];
                    rec.Description = (string)reader["description"];
                    rec.Cost = (decimal)reader["cost"];
                    rec.Status= (string)reader["status"];
                    
                }
                sqlConnection.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return rec;
        }

        public bool MaintenanceCheckInManagement(int assid,DateTime resdate)
        {
            DateTime maintaindate = default;
            DateTime refdate = default;
            TimeSpan span;
            int year=0;
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select top 1 maintenance_date from maintenance_records " +
                    "where asset_id=@id order by maintenance_date desc";
                cmd.Parameters.AddWithValue("@id", assid);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    maintaindate = (DateTime)reader["maintenance_date"];                    
                }
                sqlConnection.Close();

                 
               
                year =(resdate.Year-maintaindate.Year);
               
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            
            if ((year>2)||(maintaindate==refdate)) { return true; }
           else { return false; }
        }
    }
}
