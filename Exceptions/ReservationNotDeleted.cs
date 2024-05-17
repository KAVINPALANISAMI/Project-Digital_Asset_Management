using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Exceptions
{
    internal class ReservationNotDeleted:ApplicationException
    {
        public ReservationNotDeleted(string msg):base(msg)
        {
            
        }
    }
}
