using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Interface
{
    internal interface Iasset_allocationsRepository
    {
        public bool AllocateAsset(int assid,int empid,DateTime date,DateTime retdate);

        public bool DeallocateAsset(int assid);

        public List<asset_allocations> GetAllasset_allocationsService();

        public asset_allocations Getasset_allocationsService();
    }
}
