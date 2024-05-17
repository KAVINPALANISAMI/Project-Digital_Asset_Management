using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class reservations
    {
        public int Reservation_id { get; set; }
        public assets Asset_id { get; set; }
        public employees Employee_id { get; set; }
        public DateTime Reservation_date { get; set; }
        public DateTime Etart_date { get; set; }
        public DateTime End_date { get; set;}
        public string Stauts { get; set; }

        public reservations()
        {
            Asset_id = new assets();
            Employee_id = new employees();
        }

        public override string ToString()
        {
            return $"Reservation ID::{Reservation_id}\tAssetID::{Asset_id.Asset_id}\tEmployeId::{Employee_id.Employee_id}" +
                $"\tReserved Date::{Reservation_date}\tStart Date::{Etart_date}\tEndDate::{End_date}\tStatus::{Stauts}";
        }

    }
}
