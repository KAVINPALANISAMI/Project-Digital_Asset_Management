using Project.Service;
using Project.Service.Interface;

namespace Project.AssetManagementapp
{
    internal class AssetManagementApp
    {
        readonly IassetsService _assetsService;
        readonly IemployeesService _employeesService;
        readonly Iasset_allocationsService _asset_allocationsService;
        readonly IreservationsService _reservationsService;
        readonly Imaintenance_recordsService _maintenance_recordsService;
        public AssetManagementApp()
        {
            _assetsService=new assetsService();
            _employeesService=new employeesService();
            _asset_allocationsService=new asset_allocationsService();
            _reservationsService=new reservationsService();  
            _maintenance_recordsService=new maintenance_recordsService();
        }
        public void Run()
        {
            try
            {
                Console.WriteLine("Welcome to Digital Assert Management System");
                bool loop = true;
                bool login = false;
                while (loop)
                {
                    if (!login)
                    {
                        Console.WriteLine("---------Login Panel---------");
                        login = _employeesService.Login();
                    }

                    if (login)
                    {
                    returnhere:
                        Console.WriteLine("Enter \n1 for Asset management\n2 for  Asset tracking\n3 for Asset maintenance\n4 for Employe Details\n5 for exit\n6. log out ");
                        int option = int.Parse(Console.ReadLine());
                        switch (option)
                        {
                            case 1:
                                {
                                    Console.WriteLine("Enter\n1.Add Asset\n2.Update Asste\n3.Delete Asset\n4.Asset Details");
                                    int manageopt = int.Parse(Console.ReadLine());
                                    switch (manageopt)
                                    {
                                        case 1: { _assetsService.addAsset(); break; }
                                        case 2: {
                                                _assetsService.GetAllAssetdetails();
                                                _assetsService.UpdateAsset(); break; }
                                        case 3: {
                                                _assetsService.GetAllAssetdetails();
                                                _assetsService.DeleteAsset(); break; }
                                        case 4: { _assetsService.GetAllAssetdetails(); break; }
                                        default: { goto returnhere; break; }
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    Console.WriteLine(" Asset tracking");
                                    Console.WriteLine("1 for Allocate Asset\n2 for Deallocate Asset\n3 for Allocation Details ");
                                    int tracropt = int.Parse(Console.ReadLine());
                                    switch (tracropt)
                                    {
                                        case 1:
                                            {  _reservationsService.GetAllReservation();
                                                _asset_allocationsService.AllocateAsset(); break; }
                                        case 2: { _asset_allocationsService.DeallocateAsset(); break; }
                                        case 3: { _asset_allocationsService.GetAllasset_allocationsService(); break; }
                                        default: { goto returnhere; break; }
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    Console.WriteLine("Asset maintenance");
                                    Console.WriteLine("1 for Perform Maintenance\n2 for Reserve Asset\n3 for Withdraw Reservation:\n4 Maintenance Details \n5.Maintenance Completed\n6.Reservation Details");
                                    int mainoption = int.Parse(Console.ReadLine());
                                    switch (mainoption)
                                    {
                                        case 1: {
                                                _assetsService.GetAllAssetdetails(); _maintenance_recordsService.PerformMaintenance(); break; }
                                        case 2: { _assetsService.GetAllAssetdetails();
                                                _reservationsService.ReserveAsset(); break; }
                                        case 3: { _reservationsService.WithdrawReservation(); break; }
                                        case 4: { _maintenance_recordsService.ViewPerformMaintenance(); break; }
                                        case 5: { _maintenance_recordsService.MaintenanceCompleted(); break; }
                                        case 6: { _reservationsService.GetAllReservation(); break; }
                                        default: { goto returnhere; break; }
                                    }
                                    break;
                                }
                            case 4:
                                {
                                    Console.WriteLine("Sub Menue");
                                    Console.WriteLine("1 for Add Employee\n2 for Remove Employee\n3 for Update Employee\n4 for Employee Details");
                                    int empoption = int.Parse(Console.ReadLine());
                                    switch (empoption)
                                    {
                                        case 1: { _employeesService.AddEmployee(); break; }
                                        case 2:
                                            {    _employeesService.GetAllEmployees();
                                                _employeesService.RemoveEmployee(); break; }
                                        case 3: { _employeesService.GetAllEmployees();
                                                _employeesService.UpdateEmployee(); break; }
                                        case 4: { _employeesService.GetAllEmployees(); break; }
                                        default: { goto returnhere; break; }

                                    }
                                    break;
                                }
                            case 5:
                                {
                                    loop = false;
                                    break;
                                }
                            default: { login = false; break; }
                        }
                       
                    }
                    
                }

            }
            catch (Exception ex) { Console.WriteLine(ex); }

        }
    }

    
}
