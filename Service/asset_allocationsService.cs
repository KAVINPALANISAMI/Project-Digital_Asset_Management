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
    internal class asset_allocationsService : Iasset_allocationsService
    {

        
        readonly IassetsRepository _assetsRepository;
        readonly IemployeesRepository _iemployeesRepository;
        readonly Iasset_allocationsRepository _Iasset_allocationsRepository;
        readonly IreservationsService _IreservationsService;
        readonly IreservationsRepository _IreservationsRepository;
        
        public asset_allocationsService()
        {
            _assetsRepository = new assetsRepository();
            _iemployeesRepository = new employeesRepository();
            _Iasset_allocationsRepository = new asset_allocationsRepository();
            _IreservationsService =new reservationsService();
            _IreservationsRepository =new reservationsRepository();

        }
        public void AllocateAsset()
        {
            try
            {

                Console.WriteLine("Allocate Asset");
                Console.WriteLine("Enter assert Id");
                int assid = int.Parse(Console.ReadLine());

                bool idpresent = _assetsRepository.IsAssetIdAvailabe(assid);
                if (idpresent) { throw new AssetIdNotFound("Asset Id not found"); }

               // bool available = _assetsRepository.IsAssetAvailable(assid);
              //  if (!available) { throw new AssertNotAvailable("Asset  not Available"); }

                Console.WriteLine("Enter Employee Id");
                int empid = int.Parse(Console.ReadLine());

                bool empidpresent = _iemployeesRepository.IsEmployeeIDAvailabe(empid);
                if (empidpresent) { throw new EmployeeIdNotFound("Employee Id not found"); }

                //bool asssts = _assetsRepository.UpdateAssetStatus(assid, "in use");
                // if (asssts) { throw new AssertStatusException("Asset ststus not updated"); }
                bool availableallocate = _IreservationsRepository.ISAvailableForAllocate(assid, empid);
                if (availableallocate) 
                {
                    Console.WriteLine("Plese Reserve the asset first");
                    _IreservationsService.ReserveAsset();  
                }
                Console.WriteLine("Enter Allocation date");
                DateTime date = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Enter Return date");
                DateTime date1 = DateTime.Parse(Console.ReadLine());


                bool status = _Iasset_allocationsRepository.AllocateAsset(assid, empid, date,date1);
                if (status) { Console.WriteLine("Asset Allocated"); }
                else { Console.WriteLine("Asset not Allocated"); }

                bool Ressts = _IreservationsRepository.UpdateeRservationStatus(assid, "approved");
                if (empidpresent) { throw new AssertStatusException("Employee Id not found"); }

                asset_allocations assall=_Iasset_allocationsRepository.Getasset_allocationsService();
                Console.WriteLine(assall);
            }
            catch (AssetIdNotFound ex) { Console.WriteLine(ex.Message); }
           // catch (AssertStatusException ex) { Console.WriteLine(ex.Message); }
            catch (EmployeeIdNotFound ex) { Console.WriteLine(ex.Message); }
            catch (AssertNotAvailable ex) { Console.WriteLine(ex.Message); }
            catch (AssertStatusException ex) { Console.WriteLine(ex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public void DeallocateAsset()
        {
            try
            {
                Console.WriteLine("Deallocate Asset:");



                List<asset_allocations> reslist = _Iasset_allocationsRepository.GetAllasset_allocationsService();
                foreach (asset_allocations emp in reslist) { Console.WriteLine(emp); }

                Console.WriteLine("Enter assert Id");
                int assid = int.Parse(Console.ReadLine());

                bool idpresent = _assetsRepository.IsAssetIdAvailabe(assid);
                if (idpresent) { throw new AssetIdNotFound("Asset Id not found"); }



                bool status = _Iasset_allocationsRepository.DeallocateAsset(assid);
                if (status) { Console.WriteLine("Asset Deallocated"); }
                else { Console.WriteLine("Asset not Deallocated"); }

                bool statusres = _IreservationsRepository.WithdrawReservation(assid);
                if (!statusres) { throw new ReservationNotDeleted("Reservation not deleted"); }

                bool asssts = _assetsRepository.UpdateAssetStatus(assid, "Available");
                if (asssts) { throw new AssertStatusException("Asset ststus not updated"); }


            }
            catch (AssetIdNotFound ex) { Console.WriteLine(ex.Message); }
            catch (AssertStatusException ex) { Console.WriteLine(ex.Message); }
            catch (ReservationNotDeleted ex) { Console.WriteLine(ex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public void GetAllasset_allocationsService()
        {
            try
            {
                Console.WriteLine("Allodation  Data ");
                List<asset_allocations> reslist = _Iasset_allocationsRepository.GetAllasset_allocationsService();
                foreach (asset_allocations emp in reslist) { Console.WriteLine(emp); }
            }
            catch (EmployeeIdNotFound ex) { Console.WriteLine(ex.Message); }
            catch (NotManager ex) { Console.WriteLine(ex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
