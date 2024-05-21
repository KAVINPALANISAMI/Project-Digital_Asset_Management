using Project.Exceptions;
using Project.Model;
using Project.Repository;
using Project.Repository.Interface;
using Project.Service.Interface;

namespace Project.Service
{
    public class maintenance_recordsService : Imaintenance_recordsService
    {
        readonly IassetsRepository _IassetsRepository;
        readonly IassetsRepository _assetsRepository;
        readonly maintenance_recordsRepository _maintenance_recordsRepository;
        readonly reservationsRepository _reservationsRepository;
        readonly asset_allocationsRepository _asset_allocationsRepository;
        public maintenance_recordsService()
        {
            _IassetsRepository = new assetsRepository();
            _maintenance_recordsRepository =new maintenance_recordsRepository();
            _assetsRepository = new assetsRepository();
            _reservationsRepository = new reservationsRepository();
            _asset_allocationsRepository = new asset_allocationsRepository();
        }
        public void PerformMaintenance()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Performance Maintenance");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("****************************");
                Console.ResetColor();

               
                Console.WriteLine("Enter asset Id");
                int assetid = int.Parse(Console.ReadLine());

                bool idpresent = _IassetsRepository.IsAssetIdAvailabe(assetid);
                if (idpresent) { throw new AssetIdNotFound("AssetId not found"); }

               

                Console.WriteLine("Enter date");
                DateTime date = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Description");
                string description= Console.ReadLine();
                Console.WriteLine("Enter Cost");
                decimal cost=decimal.Parse(Console.ReadLine());

                bool status = _maintenance_recordsRepository.PerformMaintenance(assetid, date, description, cost);
                if (status) {
                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine("Addet to asset maintainance ");
                    Console.ResetColor();
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Not Addet to asset maintainance ");
                    Console.ResetColor();
                }


                bool asssts = _IassetsRepository.UpdateAssetStatus(assetid, "under maintenance");
                if (asssts) { throw new AssertStatusException("Asset ststus not updated"); }


                bool ressts = _reservationsRepository.WithdrawReservation(assetid);
               //if (!ressts) { throw new AssertStatusException("Not Deleded -Reservation"); }

                bool allsts = _asset_allocationsRepository.DeallocateAsset(assetid);
                //if (allsts) { throw new AssertStatusException("Not Deleded -Reservation"); }

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Added Asset to Maintenance");      
                Console.ResetColor();
                maintenance_records rec =_maintenance_recordsRepository.GetAddedMaintenance_Records();
                Console.WriteLine(rec);

            }
            catch (AssetIdNotFound ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();

            }
            catch (AssertStatusException ex)
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

        public void ViewPerformMaintenance()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Performance Maintenance data");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("****************************");
                Console.ResetColor();


              
                List<maintenance_records> list = _maintenance_recordsRepository.ViewPerformMaintenance();
                foreach (maintenance_records record in list)
                { Console.WriteLine(record); }
            }catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        public void MaintenanceCompleted()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Maintenance of assert Completed");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("****************************");
                Console.ResetColor();

               
                Console.WriteLine("Enter assert Id");
                int assid = int.Parse(Console.ReadLine());

                bool idpresent = _assetsRepository.IsAssetIdAvailabe(assid);
                if (idpresent) { throw new AssetIdNotFound("Asset Id not found"); }

                


                bool status = _maintenance_recordsRepository.MaintenanceCompleted(assid);
                if (status)
                {
                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine("Assert  Maintenance Completed");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("Assert  Maintenance Not Completed");
                    Console.ResetColor();
                }


                bool asssts = _IassetsRepository.UpdateAssetStatus(assid, "Available");
                if (asssts) { throw new AssertStatusException("Asset ststus not updated"); }
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
                Console.ResetColor();  }
        }
    }
}
