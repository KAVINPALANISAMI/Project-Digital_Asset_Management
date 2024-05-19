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

        public reservationsService()
        {
            _ireservationsRepository = new reservationsRepository();
            _assetsRepository = new assetsRepository();
            _iemployeesRepository =new employeesRepository();
        }

        public void GetAllReservation()
        {
            try
            {        
                Console.WriteLine("Reservation Data ");
                List<reservations> reslist = _ireservationsRepository.GetAllReservation();
                foreach (reservations emp in reslist) { Console.WriteLine(emp); }
            }
            catch (EmployeeIdNotFound ex) { Console.WriteLine(ex.Message); }
            catch (NotManager ex) { Console.WriteLine(ex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public void ReserveAsset()
        {
            try
            {
                Console.WriteLine("Reserve Asset");
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
                Console.WriteLine("Enter start date");
                DateTime date2 = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter end date");
                DateTime date3 = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter status");
                string valstatus=Console.ReadLine();
                bool status = _ireservationsRepository.ReserveAsset(assid,empid,date1,date2,date3,valstatus);
                if (status) { Console.WriteLine("Asset Reserved"); }
                else { Console.WriteLine("Asset not  Reserved"); }

                bool asssts = _assetsRepository.UpdateAssetStatus(assid, "in use");
                if (asssts) { throw new AssertStatusException("Asset ststus not updated"); }


                reservations res = _ireservationsRepository.GetReservation();
                Console.WriteLine(res) ;
            }
            catch (AssetIdNotFound ex) { Console.WriteLine(ex.Message); }
            catch (AssertStatusException ex) { Console.WriteLine(ex.Message); }
            catch (EmployeeIdNotFound ex) { Console.WriteLine(ex.Message); }
            catch (AssertNotAvailable ex) { Console.WriteLine(ex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
           
            
        }

        public void WithdrawReservation()
        {
            try
            {
                Console.WriteLine("Withdraw Reservation");
                Console.WriteLine("Enter assert Id");
                int assid = int.Parse(Console.ReadLine());

                bool idpresent = _assetsRepository.IsAssetIdAvailabe(assid);
                if (idpresent) { throw new AssetIdNotFound("Asset Id not found"); }

                bool isreserved = _ireservationsRepository.IsReserved(assid);
                if (isreserved) { throw new AssetNotReserved("Asset not found Reserved data"); }


                bool status = _ireservationsRepository.WithdrawReservation(assid);
                if (status) { Console.WriteLine("Asset Withdraw Reservation completed"); }
                else { Console.WriteLine("Asset not Withdraw Reservation"); }

                bool asssts = _assetsRepository.UpdateAssetStatus(assid, "Available");
                if (asssts) { throw new AssertStatusException("Asset ststus not updated"); }
            }
            catch(AssetNotReserved ex) {  Console.WriteLine(ex.Message); }
            catch (AssetIdNotFound ex) { Console.WriteLine(ex.Message); }
            catch (AssertStatusException ex) { Console.WriteLine(ex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }



    }
}
