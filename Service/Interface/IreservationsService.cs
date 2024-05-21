using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Interface
{
    internal interface IreservationsService
    {
        public void WithdrawReservation();
        public void ReserveAsset();

        public void GetAllReservation();

        public void UpdateeRservationStatus();


    }
}
