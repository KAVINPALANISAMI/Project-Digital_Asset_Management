using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Interface
{
    internal interface IreservationsRepository
    {
        public bool WithdrawReservation(int assid);
        public bool ReserveAsset(int assid,int empid,DateTime date1,DateTime date2,DateTime date3 ,string valstatus);

        public bool UpdateeRservationStatus(int assetid, string status);

        public bool ISAvailableForAllocate(int assid,int employeeid);

        public bool IsReserved(int assetid);

        public List<reservations> GetAllReservation();

        public reservations GetReservation();
    }
}
