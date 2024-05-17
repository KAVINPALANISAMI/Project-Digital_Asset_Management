using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Interface
{
    internal interface IemployeesRepository
    {
        public bool IsEmployeeIDAvailabe(int cusid);

        public bool Login(int empid,string password);


        public bool IsManager(int empid);

        public bool AddEmployee(employees emp);

        public bool UpdateEmployee(employees emp);

        public bool RemoveEmployee(int empid);

        public List<employees> GetAllEmployees();

        public employees GetEmployee(int empid);

        public employees GetEmployee();
    }
}
