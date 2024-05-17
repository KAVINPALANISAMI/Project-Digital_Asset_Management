using Project.Model;
using Project.Repository.Interface;
using Project.Utility;
using System.Data;
using System.Data.SqlClient;

namespace Project.Repository
{
    public class assetsRepository : IassetsRepository
    {

        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;
        public assetsRepository()
        {
            sqlConnection = new SqlConnection(DBPropertyUtil.GetConnectionString());
            cmd = new SqlCommand();
        }

        public bool IsAssetIdAvailabe(int assetId)
        {
            int value = 0;
            try
            {
                cmd.Parameters.Clear();        
                cmd.CommandText = "select asset_id from assets where asset_id=@assid";
                cmd.Parameters.Add("@assid", SqlDbType.Int).Value=assetId;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) { value = (int)reader["asset_id"]; }
                sqlConnection.Close();
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
           
            if (value == assetId) { return false; }
            else return true;
        }

        public bool addAsset(assets asset)
        {
            int status=0;
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into assets values(@name,@type,@serial,@date,@location,@status,@ownerid)";

                cmd.Parameters.AddWithValue("@name", asset.Name);
                cmd.Parameters.AddWithValue("@type", asset.Type);
                cmd.Parameters.AddWithValue("@serial", asset.Serial_number);
                cmd.Parameters.AddWithValue("@date", asset.Purchase_date);
                cmd.Parameters.AddWithValue("@location", asset.Location);
                cmd.Parameters.AddWithValue("@status", asset.Status);
                cmd.Parameters.AddWithValue("@ownerid", asset.Owner_id.Employee_id);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                 status = cmd.ExecuteNonQuery();
                sqlConnection.Close();               
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
            if (status > 0) { return true; }
            else { return false; }

        }

        

        public bool UpdateAsset(assets asset)
        {
            int status = 0;
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "update assets set  [name]=@name, [type]=@type, serial_number=@serial," +
                    "purchase_date=@date,[location]=@location,[stauts]=@status,owner_id=@ownerid " +
                    "where asset_id=@assid";
                cmd.Parameters.AddWithValue("@assid", asset.Asset_id);
                cmd.Parameters.AddWithValue("@name", asset.Name);
                cmd.Parameters.AddWithValue("@type", asset.Type);
                cmd.Parameters.AddWithValue("@serial", asset.Serial_number);
                cmd.Parameters.AddWithValue("@date", asset.Purchase_date);
                cmd.Parameters.AddWithValue("@location", asset.Location);
                cmd.Parameters.AddWithValue("@status", asset.Status);
                cmd.Parameters.AddWithValue("@ownerid", asset.Owner_id.Employee_id);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                 status = cmd.ExecuteNonQuery();
                sqlConnection.Close();
               
                
            }
             catch(Exception ex) { Console.WriteLine(ex.Message); }
            if (status > 0) { return true; }
            else { return false; }
        }

        public bool DeleteAsset(int assetId)
        {
            int sts=0;
            
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "delete from assets where asset_id=@assid";
                cmd.Parameters.AddWithValue("@assid", assetId);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                sts = cmd.ExecuteNonQuery();
                sqlConnection.Close();

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            if (sts > 0) { return true; }
            else { return false; }
        }

        public bool UpdateAssetStatus(int assetid,string status)
        {
            int sts=0;          
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "update assets set stauts=@sts where asset_id=@id";
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

        public bool IsAssetAvailable(int assetid)
        {
            int value = 0;
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select * from assets where asset_id=@id and stauts='Available'";
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = assetid;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) { value = (int)reader["asset_id"]; }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            sqlConnection.Close();
            if (value == assetid) { return false; }
            else
            {
                return true;
            }
        }

        public List<assets> GetAllAssetdetails()
        {
           List<assets> assets = new List<assets>();
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select * from assets";
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    assets ass = new assets();
                    ass.Asset_id = (int)reader["asset_id"];
                    ass.Name = (string)reader["name"];
                    ass.Type = (string)reader["type"];
                    ass.Serial_number= (int)reader["serial_number"];
                    ass.Purchase_date = (DateTime)reader["purchase_date"];
                    ass.Location = (string)reader["location"];
                    ass.Status = (string)reader["stauts"];
                    ass.Owner_id.Employee_id= (int)reader["owner_id"];
                    assets.Add(ass);
                }
                sqlConnection.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return assets;
        }

        public assets GetAsset(int empid)
        {
           assets ass=new assets();
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select  *  from assets where asset_id=@id ";
                cmd.Parameters.AddWithValue("id", empid);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {  
                    ass.Asset_id = (int)reader["asset_id"];
                    ass.Name = (string)reader["name"];
                    ass.Type = (string)reader["type"];
                    ass.Serial_number = (int)reader["serial_number"];
                    ass.Purchase_date = (DateTime)reader["purchase_date"];
                    ass.Location = (string)reader["location"];
                    ass.Status = (string)reader["stauts"];
                    ass.Owner_id.Employee_id = (int)reader["owner_id"];
                }
                sqlConnection.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return ass;
        }

        public assets GetAsset()
        {
            assets ass = new assets();
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select top 1 *  from assets order by asset_id desc";

                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ass.Asset_id = (int)reader["asset_id"];
                    ass.Name = (string)reader["name"];
                    ass.Type = (string)reader["type"];
                    ass.Serial_number = (int)reader["serial_number"];
                    ass.Purchase_date = (DateTime)reader["purchase_date"];
                    ass.Location = (string)reader["location"];
                    ass.Status = (string)reader["stauts"];
                    ass.Owner_id.Employee_id = (int)reader["owner_id"];
                }
                sqlConnection.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return ass;
        }
    }
}
