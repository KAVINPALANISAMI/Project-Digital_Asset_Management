using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Interface
{
    public interface IassetsRepository
    {
        public  bool addAsset(assets asset);
        public bool UpdateAsset(assets asset);
        public bool IsAssetIdAvailabe(int assetId);
        public bool DeleteAsset(int assetId);
        public bool UpdateAssetStatus(int assetid,string status);
        public bool IsAssetAvailable(int assetid);
        public List<assets> GetAllAssetdetails();

        public assets GetAsset(int empid);

        public assets GetAsset();

        public bool MaintenanceCheckInAsset(int assid, DateTime resdate);
    }
}
