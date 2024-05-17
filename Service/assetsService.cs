using Project.Exceptions;
using Project.Model;
using Project.Repository;
using Project.Repository.Interface;
using Project.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class assetsService : IassetsService
    {
        readonly IassetsRepository _assetsRepository;
        public assetsService()
        {
            _assetsRepository = new assetsRepository();
        }
        public void addAsset()
        {
            try
            {
                assets ass = new assets();
                Console.WriteLine("Add Assert");
                Console.WriteLine("Enter Assert Name");
                ass.Name = Console.ReadLine();
                Console.WriteLine("Enter Assert Type");
                ass.Type = Console.ReadLine();
                Console.WriteLine("Enter Assert Serial Number");
                ass.Serial_number = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Assert Purchase date");
                ass.Purchase_date = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter Assert Location");
                ass.Location = Console.ReadLine();
                Console.WriteLine("Enter Assert Status");
                ass.Status = Console.ReadLine();
                Console.WriteLine("Enter Assert Owner");
                ass.Owner_id.Employee_id = int.Parse(Console.ReadLine());
                bool status = _assetsRepository.addAsset(ass);
                if (status) { Console.WriteLine("Asset added"); }
                else
                {
                    Console.WriteLine("Asset not added");
                }


                assets asss = _assetsRepository.GetAsset();
                Console.WriteLine("added Details");
                Console.WriteLine(asss);


            }
            catch (Exception ex) { Console.WriteLine( ex.Message); }
        }

        public void DeleteAsset()
        {
            try
            {


                Console.WriteLine("Delete assert");
                Console.WriteLine("Enter assert Id");
                int assid=int.Parse(Console.ReadLine());

                bool idpresent = _assetsRepository.IsAssetIdAvailabe(assid);
                if(idpresent) { throw new AssetIdNotFound("Asset Id not found"); }

                bool status = _assetsRepository.DeleteAsset(assid);
                if (status) { Console.WriteLine("Asset Deleted"); }
                else { Console.WriteLine("Asset not deleted"); }
            }
            catch (AssetIdNotFound ex) { Console.WriteLine(ex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public void GetAllAssetdetails()
        {
            try
            {
                Console.WriteLine("Asset Data ");

                List<assets> asslist = _assetsRepository.GetAllAssetdetails();
                foreach (assets ass in asslist) { Console.WriteLine(ass); }

            }
            catch (EmployeeIdNotFound ex) { Console.WriteLine(ex.Message); }
            catch (NotManager ex) { Console.WriteLine(ex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public void UpdateAsset()
        {
            try
            {
                assets ass = new assets();
                Console.WriteLine("Update Assert");
                Console.WriteLine("Enter Assert Id");
               
                ass.Asset_id=int.Parse(Console.ReadLine());
                bool idpresent = _assetsRepository.IsAssetIdAvailabe(ass.Asset_id);
                if (idpresent) { throw new AssetIdNotFound("AssetId not found"); }

                Console.WriteLine("Enter Assert Name");
                ass.Name = Console.ReadLine();
                Console.WriteLine("Enter Assert Type");
                ass.Type = Console.ReadLine();
                Console.WriteLine("Enter Assert Serial Number");
                ass.Serial_number = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Assert Purchase date");
                ass.Purchase_date = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter Assert Location");
                ass.Location = Console.ReadLine();
                Console.WriteLine("Enter Assert Status");
                ass.Status = Console.ReadLine();
                Console.WriteLine("Enter Assert OwnerId");
                ass.Owner_id.Employee_id = int.Parse(Console.ReadLine());
                bool status = _assetsRepository.UpdateAsset(ass);
                if (status) { Console.WriteLine("Asset Updated "); }
                else{Console.WriteLine("Asset not updated");}

                assets asss = _assetsRepository.GetAsset(ass.Asset_id);
                Console.WriteLine("Updated Details");
                Console.WriteLine(asss);
            }
            catch (AssetIdNotFound ex) { Console.WriteLine(ex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
