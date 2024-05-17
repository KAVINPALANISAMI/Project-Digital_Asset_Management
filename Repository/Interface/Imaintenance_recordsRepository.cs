using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Interface
{
    public interface Imaintenance_recordsRepository
    {
        public bool PerformMaintenance(int assetid,DateTime date,string description,decimal cost);
        public List<maintenance_records> ViewPerformMaintenance();

        public maintenance_records GetAddedMaintenance_Records();

        public bool MaintenanceCompleted(int assetid);
    }
}
