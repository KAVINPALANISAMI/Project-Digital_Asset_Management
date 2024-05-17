using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Interface
{
    internal interface IemployeesService
    {
        public bool Login();

        public void AddEmployee();
        public void RemoveEmployee();
        public void UpdateEmployee();

        public void GetAllEmployees();
    }
}
