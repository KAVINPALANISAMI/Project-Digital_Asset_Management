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
    internal class asset_allocationsRepository : Iasset_allocationsRepository
    {
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;

        public asset_allocationsRepository()
        {
            sqlConnection = new SqlConnection(DBPropertyUtil.GetConnectionString());
            cmd = new SqlCommand();
        }
        public bool AllocateAsset(int assid, int empid, DateTime date,DateTime retdate)
        {
            int status = 0;
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into asset_allocations values(@assid,@empid,@date1,@date2)";

                cmd.Parameters.AddWithValue("@assid", assid);
                cmd.Parameters.AddWithValue("@empid", empid);
                cmd.Parameters.AddWithValue("@date1", date);
                cmd.Parameters.AddWithValue("@date2", retdate);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                status = cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            if (status > 0) { return true; }
            else { return false; }
        }

        public bool DeallocateAsset(int assid)
        {
            int sts = 0;

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "delete from asset_allocations where asset_id=@assid";
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

        public List<asset_allocations> GetAllasset_allocationsService()
        {


            List<asset_allocations> reslist = new List<asset_allocations>();
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select * from asset_allocations";

                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                   
                    asset_allocations allocations = new asset_allocations();
                
                    allocations.Allocation_id = (int)reader["allocation_id"];
                    allocations.Asset_id.Asset_id = (int)reader["asset_id"];
                    allocations.Employee_id.Employee_id = (int)reader["employee_id"];
                    allocations.Allocation_date= (DateTime)reader["allocation_date"];
                    Object returndate= reader["return_date"];
                    if(returndate != null) { allocations.Return_date = (DateTime)returndate; }
                    //else { allocations.Return_date=null}
                   // allocations.Return_date = (DateTime)reader["return_date"];


                    reslist.Add(allocations);   
                   
                }
                sqlConnection.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return reslist;
        }

        public asset_allocations Getasset_allocationsService()
        {
            asset_allocations allocations = new asset_allocations();
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select top 1* from asset_allocations order by allocation_date desc";

                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    allocations.Allocation_id = (int)reader["allocation_id"];
                    allocations.Asset_id.Asset_id = (int)reader["asset_id"];
                    allocations.Employee_id.Employee_id = (int)reader["employee_id"];
                    allocations.Allocation_date = (DateTime)reader["allocation_date"];
                    Object returndate = reader["return_date"];
                    if (returndate != null) { allocations.Return_date = (DateTime)returndate; } 
                }
                sqlConnection.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return allocations;
        }
    }
}
