using NUnit.Framework;
using Project.Service;
using Project.Model;
using Project.Service.Interface;
using Project.Repository;
using Project.Repository.Interface;

namespace Project.Tests
{
    public class Tests
    {
        IassetsService _assetsService = new assetsService();
        IassetsRepository iassetRepository = new assetsRepository();

        employeesRepository _employeesRepository = new employeesRepository();
        reservationsRepository _reservationsRepository = new reservationsRepository();
        maintenance_recordsRepository _maintenance_recordsRepository = new maintenance_recordsRepository();


       

      

        [Test]
        public void Test_AddAsset1()
        {
            assets ass = new assets();
            ass.Name = "Lg";
            ass.Type = "Lap";
            ass.Serial_number = 586941;
            ass.Purchase_date = DateTime.Now;
            ass.Location = "Mumbai";
            ass.Status = "in use";
            ass.Owner_id.Employee_id = 104;
            bool expected = true;
            bool actual = iassetRepository.addAsset(ass);
            Assert.That(actual, Is.True);
        }
        [Test]
        public void Test_AddAsset()
        {
            assets ass = new assets();
            ass.Name = "Lg";
            ass.Type = "Lap";
            ass.Serial_number = 586941;
            ass.Purchase_date = DateTime.Now;
            ass.Location = "Mumbai";
            ass.Status = "ie";
            ass.Owner_id.Employee_id = 104;
            bool expected = true;
            bool actual = iassetRepository.addAsset(ass);
            Assert.That(actual, Is.True);
        }

       //-------asset id-------
        [TestCase(202)]
        [TestCase(20)]

        public void Test_IsAssetIdAvailabe(int assetid)
        {
            
            bool expected = false;
            bool actual=iassetRepository.IsAssetIdAvailabe(assetid);
            Assert.That(actual, Is.False);
        }
        //----------------customer id---------
        [TestCase(101)]
        [TestCase(6)]

        public void Test_IsCustumerIdAvailable(int assetid) 
        {
            bool actual = _employeesRepository.IsEmployeeIDAvailabe(assetid);
            Assert.That(actual, Is.False);
        }

        //----------- reserved successfully or not.---------

        [TestCase(203,101,"2024/3/22"," 2024-04-16", "2024-04-18", "approved")]//correct data
        [TestCase(202,10, "2024-02-25","2024-02-26","2024-03-12", "approved")]//employeeid worng
        public void Test_ReservedSucessfully(int assid, int empid, DateTime date1, DateTime date2, DateTime date3, string valstatus)
        {
            bool expected = _reservationsRepository.ReserveAsset(assid,empid,date1,date2,date3,valstatus);
            Assert.That(expected, Is.True);
        }


        //-----------------PerformMaintenance-----------

        [TestCase(201,"2024-05-15","General service",5000)]
        [TestCase(24,"2024-04-12","Service",3000)]
         public void Test_PerformMaintenance(int assetid, DateTime date, string description, decimal cost)
        {
            bool expected = _maintenance_recordsRepository.PerformMaintenance(assetid, date, description, cost);
            Assert.That(expected, Is.True);
        }
    }
}
