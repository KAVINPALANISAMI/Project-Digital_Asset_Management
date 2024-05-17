using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class asset_allocations
    {
        public int Allocation_id { get; set; }
        public assets Asset_id { get; set; }
        public employees Employee_id { get; set; }
        public DateTime Allocation_date { get; set; }
        public DateTime Return_date { get; set; }

        public asset_allocations()
        {
            Asset_id = new assets();
            Employee_id = new employees();
        }
        public override string ToString()
        {
            return$"Asset Allocation ID::{Allocation_id}\tAssetID::{Asset_id.Asset_id}\tEmployeeID{Employee_id.Employee_id}\t" +
                $"Allocation Date{Allocation_date}\tReturn Date::{Return_date}";
        }
    }
}
