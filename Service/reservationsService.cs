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
    internal class reservationsService : IreservationsService
    {
        readonly IreservationsRepository _ireservationsRepository;
        readonly IassetsRepository _assetsRepository;
        readonly IemployeesRepository _iemployeesRepository;
        readonly Imaintenance_recordsRepository _maintenance_recordsRepository;
       // IreservationsService _IreservationsService;

        public reservationsService()
        {
            _ireservationsRepository = new reservationsRepository();
            _assetsRepository = new assetsRepository();
            _iemployeesRepository =new employeesRepository();
            _maintenance_recordsRepository =new maintenance_recordsRepository();
           // _IreservationsService = new reservationsService();
        }

        public void GetAllReservation()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;

                Console.WriteLine("Reservation Data ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("****************************");
                Console.ResetColor();
                List<reservations> reslist = _ireservationsRepository.GetAllReservation();
                foreach (reservations emp in reslist) { Console.WriteLine(emp); }
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

        public void ReserveAsset()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Reserve Asset");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("****************************");
                Console.ResetColor();
                Console.WriteLine("Enter assert Id");
                int assid = int.Parse(Console.ReadLine());

                bool idpresent = _assetsRepository.IsAssetIdAvailabe(assid);
                if (idpresent) { throw new AssetIdNotFound("Asset Id not found"); }

                

                bool available = _assetsRepository.IsAssetAvailable(assid);
                if (available) { throw new AssertNotAvailable("Asset  not Available"); }
               

                Console.WriteLine("Enter Employee Id");
                int empid=int.Parse(Console.ReadLine());

                bool empidpresent = _iemployeesRepository.IsEmployeeIDAvailabe(empid);
                if (empidpresent) { throw new EmployeeIdNotFound("Employee Id not found"); }

                Console.WriteLine("Enter reservationdate");
                DateTime date1=DateTime.Parse(Console.ReadLine());

                bool maintenance = _maintenance_recordsRepository.MaintenanceCheckInManagement(assid, date1);
                if (maintenance) 
                {
                    bool maintenanceAtAsset=_assetsRepository.MaintenanceCheckInAsset(assid, date1);
                    if (maintenanceAtAsset)
                    {
                        throw new AssetNotMaintain("Asset not maintenance for 2 years");
                    }
                }


                Console.WriteLine("Enter start date");
                DateTime date2 = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter end date");
                DateTime date3 = DateTime.Parse(Console.ReadLine());
                //Console.WriteLine("Enter status");
                //string valstatus=Console.ReadLine();
                string valstatus = "Pending";
                 bool status = _ireservationsRepository.ReserveAsset(assid,empid,date1,date2,date3,valstatus);
                if (status) {
                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine("*******Asset Reserved*****");
                    Console.ResetColor();
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("*********Asset not  Reserved*********");
                    Console.ResetColor();
                }

                bool asssts = _assetsRepository.UpdateAssetStatus(assid, "Reserved");
                if (asssts) { throw new AssertStatusException("Asset ststus not updated"); }

                Console.ForegroundColor = ConsoleColor.DarkYellow;

                Console.WriteLine("********** Reserved Details **********");
                Console.ResetColor();

                reservations res = _ireservationsRepository.GetReservation();
                Console.WriteLine(res) ;
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
                Console.ResetColor(); }
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

        public void UpdateeRservationStatus()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;

                Console.WriteLine("Update Rservation Status");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("****************************");
                Console.ResetColor();
                IreservationsService _IreservationsService = new reservationsService();
                _IreservationsService.GetAllReservation();

                Console.WriteLine("Enter assert Id");
                int assid = int.Parse(Console.ReadLine());

                bool idpresent = _assetsRepository.IsAssetIdAvailabe(assid);
                if (idpresent) { throw new AssetIdNotFound("Asset Id not found"); }

                bool isreserved = _ireservationsRepository.IsReserved(assid);
                if (isreserved) { throw new AssetNotReserved("Asset not found Reserved data"); }

                bool Ressts = _ireservationsRepository.UpdateeRservationStatus(assid, "approved");
                if (!Ressts) { Console.WriteLine("Reservation Status Updated"); }
            }
            catch (Exception ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }

        }

        public void WithdrawReservation()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;

                Console.WriteLine("Asset Reservation Withdraw");
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine("**********************");
                Console.ResetColor();
                Console.WriteLine("Enter assert Id");
                int assid = int.Parse(Console.ReadLine());

                bool idpresent = _assetsRepository.IsAssetIdAvailabe(assid);
                if (idpresent) { throw new AssetIdNotFound("Asset Id not found"); }

                bool isreserved = _ireservationsRepository.IsReserved(assid);
                if (isreserved) { throw new AssetNotReserved("Asset not found Reserved data"); }


                bool status = _ireservationsRepository.WithdrawReservation(assid);
                if (status) {
                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine("Asset Withdraw Reservation completed");
                    Console.ResetColor();
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine("Asset not Withdraw Reservation");
                    Console.ResetColor();
                }

                bool asssts = _assetsRepository.UpdateAssetStatus(assid, "Available");
                if (asssts) { throw new AssertStatusException("Asset ststus not updated"); }
            }
            catch(AssetNotReserved ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            catch (AssetIdNotFound ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            catch (AssertStatusException ex) {
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
