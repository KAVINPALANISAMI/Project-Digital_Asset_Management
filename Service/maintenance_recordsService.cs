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
                Console.WriteLine("Performance MAintenance");
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
                if (status) { Console.WriteLine("Addet to asset maintainance "); }
               else { Console.WriteLine("Not Addet to asset maintainance "); }


                bool asssts = _IassetsRepository.UpdateAssetStatus(assetid, "under maintenance");
                if (asssts) { throw new AssertStatusException("Asset ststus not updated"); }


                bool ressts = _reservationsRepository.WithdrawReservation(assetid);
               if (!ressts) { throw new AssertStatusException("Not Deleded -Reservation"); }

                bool allsts = _asset_allocationsRepository.DeallocateAsset(assetid);
                if (allsts) { throw new AssertStatusException("Not Deleded -Reservation"); }


                maintenance_records rec =_maintenance_recordsRepository.GetAddedMaintenance_Records();
                Console.WriteLine(rec);

            }
            catch (AssetIdNotFound ex) { Console.WriteLine(ex.Message); }
            catch(AssertStatusException ex) {  Console.WriteLine(ex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public void ViewPerformMaintenance()
        {
            Console.WriteLine(  "Performance Maintenance data");
            List<maintenance_records> list = _maintenance_recordsRepository.ViewPerformMaintenance();
            foreach (maintenance_records record in list) 
            { Console.WriteLine(record); }
        }

        public void MaintenanceCompleted()
        {
            try
            {
                Console.WriteLine("Withdraw assert");
                Console.WriteLine("Enter assert Id");
                int assid = int.Parse(Console.ReadLine());

                bool idpresent = _assetsRepository.IsAssetIdAvailabe(assid);
                if (idpresent) { throw new AssetIdNotFound("Asset Id not found"); }

                bool asssts = _IassetsRepository.UpdateAssetStatus(assid, "Available");
                if (asssts) { throw new AssertStatusException("Asset ststus not updated"); }


                bool status = _maintenance_recordsRepository.MaintenanceCompleted(assid);
                if (status) { Console.WriteLine("Asset in use"); }
                else { Console.WriteLine("Asset not in use"); }
            }
            catch (AssetIdNotFound ex) { Console.WriteLine(ex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
