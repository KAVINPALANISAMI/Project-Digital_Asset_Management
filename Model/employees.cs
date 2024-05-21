using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class employees
    {
        public int Employee_id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string  Password { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            return $"EmpliyeeId::{Employee_id}\tName::{Name}\tDepartment::{Department}\tEmail::{Email}\tAdminOrUser::{Status}";
        }

    }
}
