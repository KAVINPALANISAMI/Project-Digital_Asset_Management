using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class assets
    {
        public int Asset_id { get; set; }
        public string  Name { get; set; }
        public string Type { get; set; }

        public int Serial_number{ get; set; }
        public DateTime Purchase_date { get; set; }

        public string Location { get; set; }
        public string  Status { get; set; }
        public employees Owner_id { get; set; }

        

        public assets()
        {
            Owner_id = new employees();
        }
        public assets(string name,string type,int serialnum,DateTime date,string location,string stauts,employees ownerid)
        {
            Name=name;
            Type=type;
            Serial_number=serialnum;
            Purchase_date=date;
            Location=location;
            Status = stauts;
            Owner_id = ownerid;
           
        }

        public override string ToString() 
        {
            return $"AssetId::{Asset_id}\t Name::{Name}\tType::{Type}\tSerial Number::{Serial_number}\tPurchase Date::{Purchase_date}\tLocation::{Location}\tStauts::{Status}\tOwnerId::{Owner_id.Employee_id}";
        }


    }
}
