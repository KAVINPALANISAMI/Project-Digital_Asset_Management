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
        readonly IemployeesRepository _iemployeesRepository;
        public assetsService()
        {
            _assetsRepository = new assetsRepository();
            _iemployeesRepository = new employeesRepository();
        }
        public void addAsset()
        {
            try
            {
                assets ass = new assets();
                Console.ForegroundColor = ConsoleColor.Blue;

                Console.WriteLine("Add Assert");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("****************************");
                Console.ResetColor();

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
                // Console.WriteLine("Enter Assert Status");
                //ass.Status = Console.ReadLine();
                ass.Status = "Available";
                Console.WriteLine("Enter Assert Owner");
                ass.Owner_id.Employee_id = int.Parse(Console.ReadLine());
                bool status = _assetsRepository.addAsset(ass);
                if (status) {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("*******Asset added********");
                    Console.ResetColor();

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("**********Asset not added**********");
                    Console.ResetColor();
                }


                assets asss = _assetsRepository.GetAsset();
                Console.ForegroundColor = ConsoleColor.DarkYellow;

                Console.WriteLine("*******New  Added Asset Details*******");
                Console.ResetColor();

                Console.WriteLine(asss);


            }
            catch (Exception ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine( ex.Message);
                    Console.ResetColor();
            }
        }

        public void DeleteAsset()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;

                Console.WriteLine("Delete assert");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("****************************");
                Console.ResetColor();

                IassetsService _IassetsService = new assetsService();
                _IassetsService.GetAllAssetdetails();

                Console.ForegroundColor = ConsoleColor.Blue;

                Console.WriteLine("Deleting Asset Details");

                Console.ResetColor();

                Console.WriteLine("Enter assert Id");
                int assid=int.Parse(Console.ReadLine());

                bool idpresent = _assetsRepository.IsAssetIdAvailabe(assid);
                if(idpresent) { throw new AssetIdNotFound("Asset Id not found"); }

                bool status = _assetsRepository.DeleteAsset(assid);
                if (status) {
                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine("**********Asset Deleted*********");
                    Console.ResetColor();
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("********Asset not deleted**********");
                    Console.ResetColor();
                }
            }
            catch (AssetIdNotFound ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        public void GetAllAssetdetails()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;

                Console.WriteLine("Asset Data ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("****************************");
                Console.ResetColor();
                List<assets> asslist = _assetsRepository.GetAllAssetdetails();
                foreach (assets ass in asslist) { Console.WriteLine(ass); }

            }
            catch (EmployeeIdNotFound ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            catch (NotManager ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            catch (Exception ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        public void UpdateAsset()
        {
            try
            {
                assets ass = new assets();
                Console.ForegroundColor = ConsoleColor.Blue;

                Console.WriteLine("Update Assert");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("****************************");
                Console.ResetColor();

               // IassetsService _IassetsService = new assetsService();
                //_IassetsService.GetAllAssetdetails();

                Console.ForegroundColor = ConsoleColor.Blue;

                Console.WriteLine("Enter Updated Assert Details");
                
                Console.ResetColor();

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

                bool empidpresent = _iemployeesRepository.IsEmployeeIDAvailabe(ass.Owner_id.Employee_id);
                if (empidpresent) { throw new EmployeeIdNotFound("Employee Id not found"); }


                bool status = _assetsRepository.UpdateAsset(ass);

                if (status) {
                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine("**********Asset Updated **********");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("********Asset not updated*********");
                    Console.ResetColor();
                }

                assets asss = _assetsRepository.GetAsset(ass.Asset_id);
                Console.ForegroundColor = ConsoleColor.DarkYellow;

                Console.WriteLine("******Updated Details********");
                Console.ResetColor();

                Console.WriteLine(asss);
            }
            catch (AssetIdNotFound ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            catch (Exception ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }
    }
}
