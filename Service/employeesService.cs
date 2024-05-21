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

                // Console.WriteLine("Enter your Employee Id");
                //int empid=int.Parse(Console.ReadLine());
                //  bool available = _IemployeesRepository.IsManager(empid);
                // if (available) { throw new NotManager("This is only accable to Managers"); }


                //  Console.WriteLine("Enter Password");
                // string password = Console.ReadLine();

                //bool result = _IemployeesRepository.Login(empid, password);
                //if (!result) { Console.WriteLine("Id password not match"); }

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Add Employee");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("****************************");
                Console.ResetColor();

               
                Console.WriteLine("Enter Employee Name");
                emp.Name = Console.ReadLine();

                Console.WriteLine("Enter Employee Department");
                emp.Department = Console.ReadLine();

                Console.WriteLine("Enter Employee E-Mail");
               emp.Email = (Console.ReadLine());


                Console.WriteLine("Enter Password");
                emp.Password= Console.ReadLine();

                emp.Status = "User";
                bool status = _IemployeesRepository.AddEmployee(emp);
                if (status) {
                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine("*********Employee added**********");
                Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("*********Employee not added*************");
                    Console.ResetColor();

                }

                employees empup = _IemployeesRepository.GetEmployee();
                Console.ForegroundColor = ConsoleColor.DarkYellow;

                Console.WriteLine("Added Employee Details");
                Console.ResetColor();

                Console.WriteLine(empup);
            }

            catch (NotManager ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            catch (Exception ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }

        }

        public bool Login(string adminOrUser )
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

                result=_IemployeesRepository.Login(empid, password,adminOrUser);
                if (!result) { Console.WriteLine("Id password not match"); }
            }                    
            catch (EmployeeIdNotFound ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }           
            catch (Exception ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            return result;

        }

        public void RemoveEmployee()
        {
            try
            {
                // Console.WriteLine("Enter your Employee Id");
                // int mempid = int.Parse(Console.ReadLine());

                //  bool available = _IemployeesRepository.IsManager(mempid);
                //  if (available) { throw new NotManager("This is only accable to Managers"); }

                //  Console.WriteLine("Enter Password");
                // string password = Console.ReadLine();
                // bool result = _IemployeesRepository.Login(mempid, password);
                //  if (!result) { throw new NotManager("Id password not match"); }


                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Remove Employee");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("****************************");
                Console.ResetColor();

              
                Console.WriteLine("Enter Employee Id");
                int empid = int.Parse(Console.ReadLine());

                bool empidpresent = _IemployeesRepository.IsEmployeeIDAvailabe(empid);
                if (empidpresent) { throw new EmployeeIdNotFound("Employee Id not found"); }


                bool status = _IemployeesRepository.RemoveEmployee(empid);
                if (status) {
                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine("Employee Removed");
                    Console.ResetColor();
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Employee not Removed");
                    Console.ResetColor();
                }
            }
            catch (EmployeeIdNotFound ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            catch (NotManager ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            catch (Exception ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        public void UpdateEmployee()
        {
            try
            {
                employees emp = new employees();

                ///// Console.WriteLine("Enter your Employee Id");
                // int empid = int.Parse(Console.ReadLine());

                //  bool available = _IemployeesRepository.IsManager(empid);
                //  if (available) { throw new NotManager("This is only accable to Managers"); }

                //// Console.WriteLine("Enter Password");
                // string password=Console.ReadLine();
                // bool result = _IemployeesRepository.Login(empid, password);
                // if (!result) { throw new NotManager("Id password not match"); }


                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Update Employee");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("****************************");
                Console.ResetColor();

               

                Console.WriteLine("Enter Employee Id");
                emp.Employee_id=int.Parse(Console.ReadLine());

                bool empidpresent = _IemployeesRepository.IsEmployeeIDAvailabe(emp.Employee_id);
                if (empidpresent) { throw new EmployeeIdNotFound("Employee Id not found"); }



                Console.WriteLine("Enter Employee Name");
                emp.Name = Console.ReadLine();

                Console.WriteLine("Enter Employee Department");
                emp.Department = Console.ReadLine();

                Console.WriteLine("Enter Employee E-Mail");
                emp.Email = (Console.ReadLine());


                Console.WriteLine("Enter Password");
                emp.Password = Console.ReadLine();

                Console.WriteLine("Enter Status");
                emp.Status = Console.ReadLine();


                bool status = _IemployeesRepository.UpdateEmployee(emp);
                if (status) {
                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine("***********Employee Updated**********");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("Employee not upadted");
                    Console.ResetColor();
                }

                employees empup=_IemployeesRepository.GetEmployee(emp.Employee_id);
                Console.WriteLine("Updated Details");
                Console.WriteLine(empup);
            }
            catch (EmployeeIdNotFound ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            catch (NotManager ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            catch (Exception ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        public void GetAllEmployees()
        {
            try
            {
                // Console.WriteLine("Enter your Employee Id");
                // int mempid = int.Parse(Console.ReadLine());

                //  bool available = _IemployeesRepository.IsManager(mempid);
                // if (available) { throw new NotManager("This is only accable to Managers"); }

                //  Console.WriteLine("Enter Password");
                //  string password = Console.ReadLine();
                // bool result = _IemployeesRepository.Login(mempid, password);
                // if (!result) { throw new NotManager("Id password not match"); }


                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Employee Data ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("****************************");
                Console.ResetColor();

               

                List<employees> emplist = _IemployeesRepository.GetAllEmployees();
                foreach (employees emp in emplist) {  Console.WriteLine(emp); }
               
            }
            catch (EmployeeIdNotFound ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            catch (NotManager ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            catch (Exception ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }
    }
}
