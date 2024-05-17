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
    internal class employeesService : IemployeesService
    {
        readonly IemployeesRepository _IemployeesRepository;

        public employeesService()
        {
            _IemployeesRepository = new employeesRepository();
        }

        public void AddEmployee()
        {
            try
            {
               employees emp=new employees();

                Console.WriteLine("Enter your Employee Id");
                int empid=int.Parse(Console.ReadLine());
                bool available = _IemployeesRepository.IsManager(empid);
                if (available) { throw new NotManager("This is only accable to Managers"); }


                Console.WriteLine("Enter Password");
                string password = Console.ReadLine();

                bool result = _IemployeesRepository.Login(empid, password);
                if (!result) { Console.WriteLine("Id password not match"); }

                Console.WriteLine("Add Employee");
                Console.WriteLine("Enter Employee Name");
                emp.Name = Console.ReadLine();

                Console.WriteLine("Enter Employee Department");
                emp.Department = Console.ReadLine();

                Console.WriteLine("Enter Employee E-Mail");
               emp.Email = (Console.ReadLine());


                Console.WriteLine("Enter Password");
                emp.Password= Console.ReadLine();


                bool status = _IemployeesRepository.AddEmployee(emp);
                if (status) { Console.WriteLine("Employee added"); }
                else
                { Console.WriteLine("Employee not added"); }

                employees empup = _IemployeesRepository.GetEmployee();
                Console.WriteLine("Added Employee Details");
                Console.WriteLine(empup);
            }

            catch (NotManager ex) {  Console.WriteLine(ex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

        }

        public bool Login()
        {
            bool result = false;
            try
            {


                Console.WriteLine("Enter employee Id");
                int empid = int.Parse(Console.ReadLine());

                bool empidpresent = _IemployeesRepository.IsEmployeeIDAvailabe(empid);
                if (empidpresent) { throw new EmployeeIdNotFound("Employee Id not found"); }

                Console.WriteLine("Enter Password");
                string password= Console.ReadLine();

                result=_IemployeesRepository.Login(empid, password);
                if (!result) { Console.WriteLine("Id password not match"); }
            }                    
            catch (EmployeeIdNotFound ex) { Console.WriteLine(ex.Message); }           
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return result;

        }

        public void RemoveEmployee()
        {
            try
            {
                Console.WriteLine("Enter your Employee Id");
                int mempid = int.Parse(Console.ReadLine());

                bool available = _IemployeesRepository.IsManager(mempid);
                if (available) { throw new NotManager("This is only accable to Managers"); }

                Console.WriteLine("Enter Password");
                string password = Console.ReadLine();
                bool result = _IemployeesRepository.Login(mempid, password);
                if (!result) { throw new NotManager("Id password not match"); }


                Console.WriteLine("Remove Employee");
                Console.WriteLine("Enter Employee Id");
                int empid = int.Parse(Console.ReadLine());

                bool empidpresent = _IemployeesRepository.IsEmployeeIDAvailabe(empid);
                if (empidpresent) { throw new EmployeeIdNotFound("Employee Id not found"); }


                bool status = _IemployeesRepository.RemoveEmployee(empid);
                if (status) { Console.WriteLine("Employee Removed"); }
                else { Console.WriteLine("Employee not Removed"); }
            }
            catch (EmployeeIdNotFound ex) { Console.WriteLine(ex.Message); }
            catch (NotManager ex) { Console.WriteLine(ex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public void UpdateEmployee()
        {
            try
            {
                employees emp = new employees();

                Console.WriteLine("Enter your Employee Id");
                int empid = int.Parse(Console.ReadLine());

                bool available = _IemployeesRepository.IsManager(empid);
                if (available) { throw new NotManager("This is only accable to Managers"); }

                Console.WriteLine("Enter Password");
                string password=Console.ReadLine();
                bool result = _IemployeesRepository.Login(empid, password);
                if (!result) { throw new NotManager("Id password not match"); }

                Console.WriteLine("Update Employee");

                Console.WriteLine("Enter Employee Id");
                emp.Employee_id=int.Parse(Console.ReadLine());

                bool empidpresent = _IemployeesRepository.IsEmployeeIDAvailabe(empid);
                if (empidpresent) { throw new EmployeeIdNotFound("Employee Id not found"); }



                Console.WriteLine("Enter Employee Name");
                emp.Name = Console.ReadLine();

                Console.WriteLine("Enter Employee Department");
                emp.Department = Console.ReadLine();

                Console.WriteLine("Enter Employee E-Mail");
                emp.Email = (Console.ReadLine());


                Console.WriteLine("Enter Password");
                emp.Password = Console.ReadLine();


                bool status = _IemployeesRepository.UpdateEmployee(emp);
                if (status) { Console.WriteLine("Employee Updated"); }
                else
                { Console.WriteLine("Employee not upadted"); }

                employees empup=_IemployeesRepository.GetEmployee(emp.Employee_id);
                Console.WriteLine("Updated Details");
                Console.WriteLine(empup);
            }
            catch (EmployeeIdNotFound ex) { Console.WriteLine(ex.Message); }
            catch (NotManager ex) { Console.WriteLine(ex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public void GetAllEmployees()
        {
            try
            {
                Console.WriteLine("Enter your Employee Id");
                int mempid = int.Parse(Console.ReadLine());

                bool available = _IemployeesRepository.IsManager(mempid);
                if (available) { throw new NotManager("This is only accable to Managers"); }

                Console.WriteLine("Enter Password");
                string password = Console.ReadLine();
                bool result = _IemployeesRepository.Login(mempid, password);
                if (!result) { throw new NotManager("Id password not match"); }


                Console.WriteLine("Employee Data ");

                List<employees> emplist = _IemployeesRepository.GetAllEmployees();
                foreach (employees emp in emplist) {  Console.WriteLine(emp); }
               
            }
            catch (EmployeeIdNotFound ex) { Console.WriteLine(ex.Message); }
            catch (NotManager ex) { Console.WriteLine(ex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
