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
        readonly IassetsService _assetsService;
        
        public asset_allocationsService()
        {
            _assetsRepository = new assetsRepository();
            _iemployeesRepository = new employeesRepository();
            _Iasset_allocationsRepository = new asset_allocationsRepository();
            _IreservationsService =new reservationsService();
            _IreservationsRepository =new reservationsRepository();
            _assetsService = new assetsService();

        }
        public void AllocateAsset()
        {
            try
            {
            asset_allocate:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Allocate Asset");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("****************************");
                Console.ResetColor();

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


                bool availableallocate = _IreservationsRepository.ISAvailableForAllocate(assid, empid);
                if (availableallocate)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;

                    Console.WriteLine("Plese Reserve the asset first");
                    Console.ResetColor();
                    _assetsService.GetAllAssetdetails();
                    _IreservationsService.ReserveAsset();
                    goto asset_allocate;
                }
                Console.WriteLine("Enter Allocation date");
                DateTime date = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Enter Return date");
                DateTime date1 = DateTime.Parse(Console.ReadLine());


                bool status = _Iasset_allocationsRepository.AllocateAsset(assid, empid, date, date1);
                if (status)
                {
                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine("Asset Allocated");
                    Console.ResetColor();
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Asset not Allocated");
                    Console.ResetColor();
                }


                bool asssts = _assetsRepository.UpdateAssetStatus(assid, "Allocated");
                if (asssts) { throw new AssertStatusException("Asset ststus not updated"); }

                bool Ressts = _IreservationsRepository.UpdateeRservationStatus(assid, "approved");
                if (Ressts) { throw new AssertStatusException("Reservation Status"); }

                asset_allocations assall = _Iasset_allocationsRepository.Getasset_allocationsService();
                Console.WriteLine(assall);
            }
            catch (AssetIdNotFound ex)
            {
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
            catch (EmployeeIdNotFound ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            catch (AssertNotAvailable ex)
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
        public void DeallocateAsset()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Deallocate Asset:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("****************************");
                Console.ResetColor();
               



                //List<asset_allocations> reslist = _Iasset_allocationsRepository.GetAllasset_allocationsService();
               // foreach (asset_allocations emp in reslist) { Console.WriteLine(emp); }

                Console.WriteLine("Enter assert Id");
                int assid = int.Parse(Console.ReadLine());

                bool idpresent = _assetsRepository.IsAssetIdAvailabe(assid);
                if (idpresent) { throw new AssetIdNotFound("Asset Id not found"); }



                bool status = _Iasset_allocationsRepository.DeallocateAsset(assid);
                if (status)
                {
                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine("Asset Deallocated");
                    Console.ResetColor();
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Asset not Deallocated");
                    Console.ResetColor();
                }

                bool statusres = _IreservationsRepository.WithdrawReservation(assid);
                if (!statusres) { throw new ReservationNotDeleted("Reservation not deleted"); }

                bool asssts = _assetsRepository.UpdateAssetStatus(assid, "Available");
                if (asssts) { throw new AssertStatusException("Asset ststus not updated"); }


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
            catch (ReservationNotDeleted ex)
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

        public void GetAllasset_allocationsService()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Allocation  Data ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("****************************");
                Console.ResetColor();
               
                List<asset_allocations> reslist = _Iasset_allocationsRepository.GetAllasset_allocationsService();
                foreach (asset_allocations emp in reslist) { Console.WriteLine(emp); }
            }
            catch (EmployeeIdNotFound ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            catch (NotManager ex)
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
    }
}
