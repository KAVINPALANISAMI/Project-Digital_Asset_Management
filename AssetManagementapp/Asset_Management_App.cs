using Project.Service;
using Project.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AssetManagementapp
{
    internal class Asset_Management_App
    {
        readonly IassetsService _assetsService;
        readonly IemployeesService _employeesService;
        readonly Iasset_allocationsService _asset_allocationsService;
        readonly IreservationsService _reservationsService;
        readonly Imaintenance_recordsService _maintenance_recordsService;

        public Asset_Management_App()
        {
            _assetsService = new assetsService();
            _employeesService = new employeesService();
            _asset_allocationsService = new asset_allocationsService();
            _reservationsService = new reservationsService();
            _maintenance_recordsService = new maintenance_recordsService();
        }

        public void Run()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Welcome to Digital Assert Management System");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("***************************************");
            Console.ResetColor();

            bool loop = true;
            bool login = false;
            bool adminlogin = false;
            bool userlogin = false;
            while (loop)
            {
                try
                {
               
                    Console.ForegroundColor = ConsoleColor.Blue;

                    Console.WriteLine("---------Login Panel---------");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("****************************");

                    Console.ResetColor();

                    Console.WriteLine("Enter \n1.AdminLogin\n2.UserLogin\n3.Close Application");
                        int loginoption = int.Parse(Console.ReadLine());
                        switch (loginoption) 
                        {
                            case 2:
                                {
                                Console.ForegroundColor = ConsoleColor.Blue;

                                Console.WriteLine("Welcome TO User Login");
                                Console.ForegroundColor = ConsoleColor.Yellow;

                                Console.WriteLine("****************************");
                                Console.ResetColor();

                                userlogin = _employeesService.Login("USer");
                                    if (userlogin) 
                                    {
                                    Console.ForegroundColor = ConsoleColor.Green;

                                    Console.WriteLine("*********Logined as user**********");
                                    Console.ResetColor();

                                }
                                while (userlogin)
                                    {
                                        Console.WriteLine("Enter\n1.Asset Reservation\n2.Asset Reservation Withdraw\n3.Logout");
                                        int useroption=int.Parse(Console.ReadLine());
                                        switch(useroption) 
                                        {
                                            case 1:
                                                {
                                                Console.ForegroundColor = ConsoleColor.Blue;

                                                Console.WriteLine("Asset Reservation");
                                                Console.ForegroundColor = ConsoleColor.Yellow;

                                                Console.WriteLine("****************************");
                                                Console.ResetColor();

                                                  _assetsService.GetAllAssetdetails();
                                                    _reservationsService.ReserveAsset();
                                                    break;
                                                }
                                            case 2:
                                                {
                                                //Console.ForegroundColor = ConsoleColor.Blue;

                                                //Console.WriteLine("Asset Reservation Withdraw");
                                                //Console.ForegroundColor = ConsoleColor.Yellow;

                                                //Console.WriteLine("*****************");
                                                //Console.ResetColor();

                                                _reservationsService.WithdrawReservation();
                                                    break;
                                                }
                                            default:
                                                {
                                                        userlogin = false;
                                                    break;
                                                }
                                        }
                                    }
                                    break;
                                }
                            case 1:
                                {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("Welcome TO Admin Login");
                                Console.ForegroundColor = ConsoleColor.Yellow;

                                Console.WriteLine("****************************");
                                Console.ResetColor();
                                adminlogin = _employeesService.Login("Admin");
                                if (adminlogin) {
                                    Console.ForegroundColor = ConsoleColor.Green;

                                    Console.WriteLine("*********Logined as Admin**********");
                                    Console.ResetColor();
                                }
                                while(adminlogin)
                                {
                                    Mainmunue:
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("Welcome Main Menue");
                                    Console.ForegroundColor = ConsoleColor.Yellow;

                                    Console.WriteLine("****************************");
                                    Console.ResetColor();
                                    Console.WriteLine("Enter \n1.Asset management\n2.Asset tracking\n3.Asset maintenance\n4.Employe Details\n5.Log out ");
                                    int adminoption = int.Parse(Console.ReadLine());
                                    switch(adminoption) 
                                    {
                                        case 1:
                                            
                                            {
                                                bool assetsts=true;
                                                while (assetsts)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Blue;
                                                    Console.WriteLine("Asset Management");
                                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                                    Console.WriteLine("****************************");
                                                    Console.ResetColor();

                                                    Console.WriteLine("Enter\n1.Add Asset\n2.Update Asste\n3.Delete Asset\n4.Asset Details\n5.Back to Main menue");
                                                    int assatoption = int.Parse(Console.ReadLine());
                                                    switch (assatoption)
                                                    {
                                                        case 1: { _assetsService.addAsset(); break; }
                                                        case 2:
                                                            {
                                                                _assetsService.GetAllAssetdetails();
                                                                _assetsService.UpdateAsset(); break;
                                                            }
                                                        case 3:
                                                            {
                                                                _assetsService.GetAllAssetdetails();
                                                                _assetsService.DeleteAsset(); break;
                                                            }
                                                        case 4: { _assetsService.GetAllAssetdetails(); break; }
                                                        default: { goto Mainmunue; break; }
                                                    }
                                                }
                                                break;
                                            }
                                        case 2:
                                            {
                                                bool trackinsts=true;
                                                while (trackinsts)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Blue;
                                                    Console.WriteLine("Asset Tracking");
                                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                                    Console.WriteLine("****************************");
                                                    Console.ResetColor();
                                                   
                                                    Console.WriteLine("1.Reserve Asset\n2.Withdraw Reservation\n3.Reservation Details\n4.Approve Reservation\n5.Back to Main Menue");
                                                    int tracoption = int.Parse(Console.ReadLine());
                                                    switch (tracoption)
                                                    {

                                                        case 1:
                                                            {
                                                                _assetsService.GetAllAssetdetails();
                                                                _reservationsService.ReserveAsset(); break;
                                                            }
                                                        case 2: {
                                                                _reservationsService.GetAllReservation(); 
                                                                _reservationsService.WithdrawReservation(); break; }

                                                        case 3: { _reservationsService.GetAllReservation(); break; }
                                                        case 4: { _reservationsService.UpdateeRservationStatus(); break; }

                                                        default: { goto Mainmunue; break; }
                                                    }
                                                }
                                                break;
                                            }
                                        case 3:
                                            {
                                                bool managementsts=true;
                                                while (managementsts)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Blue;
                                                    Console.WriteLine(" Asset Maintenance");
                                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                                    Console.WriteLine("****************************");
                                                    Console.ResetColor();
                                                    
                                                    Console.WriteLine("1.Allocate Asset\n2.Deallocate Asset\n3.Allocation Details\n4.Perform Maintenance\n5.Maintenance Completed\n6.Maintenance Details\n7.Back to Main Menue");
                                                    int maintainopt = int.Parse(Console.ReadLine());
                                                    switch (maintainopt)
                                                    {
                                                        case 1:
                                                            {
                                                                _reservationsService.GetAllReservation();
                                                                _asset_allocationsService.AllocateAsset(); break;
                                                            }
                                                        case 2: {
                                                                _asset_allocationsService.GetAllasset_allocationsService();
                                                                _asset_allocationsService.DeallocateAsset(); break; }

                                                        case 3: { _asset_allocationsService.GetAllasset_allocationsService(); break; }
                                                        case 4:
                                                            {
                                                                _assetsService.GetAllAssetdetails();
                                                                _maintenance_recordsService.PerformMaintenance(); break;
                                                            }
                                                        case 5: {
                                                                _maintenance_recordsService.ViewPerformMaintenance();
                                                                _maintenance_recordsService.MaintenanceCompleted(); break; }
                                                        case 6: { _maintenance_recordsService.ViewPerformMaintenance(); break; }

                                                        default: { goto Mainmunue; break; }
                                                    }
                                                }
                                                break;
                                            }
                                        case 4:
                                            {
                                                bool empsts=true;
                                                while (empsts)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Blue;
                                                    Console.WriteLine("Sub Menue");
                                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                                    Console.WriteLine("****************************");
                                                    Console.ResetColor();

                                                   
                                                    Console.WriteLine("1.Add Employee\n2.Remove Employee\n3.Update Employee\n4.Employee Details");
                                                    int empoption = int.Parse(Console.ReadLine());
                                                    switch (empoption)
                                                    {
                                                        case 1: { _employeesService.AddEmployee(); break; }
                                                        case 2:
                                                            {
                                                                _employeesService.GetAllEmployees();
                                                                _employeesService.RemoveEmployee(); break;
                                                            }
                                                        case 3:
                                                            {
                                                                _employeesService.GetAllEmployees();
                                                                _employeesService.UpdateEmployee(); break;
                                                            }
                                                        case 4: { _employeesService.GetAllEmployees(); break; }
                                                        default: { goto Mainmunue; break; }
                                                    }
                                                }
                                                break;
                                            }
                                        default:                                                                             
                                            {
                                                adminlogin = false;
                                                break;
                                            }

                                    }
                                }

                                break;
                                }

                            default:
                                {
                                    loop = false;
                                    break;

                                }
                        }
                    
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
            
        }
    }
}
