using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class maintenance_records
    {
        public int Maintenance_id { get; set; }
        public assets Asset_id { get; set; }
        public DateTime Maintenance_date { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }

        public maintenance_records()
        {
            Asset_id = new assets();
        }
        public override string ToString()
        {
            return $"Maintenanct Id::{Maintenance_id}\tAssertID::{Asset_id.Asset_id}\t Date::{Maintenance_date}\t Description::{Description}\tCost ::{Cost} ";
        }

    }
}
