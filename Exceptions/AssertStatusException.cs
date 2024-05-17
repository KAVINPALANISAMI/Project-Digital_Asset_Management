using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Exceptions
{
    internal class AssertStatusException: ApplicationException
    {
        public AssertStatusException(string msg):base(msg) 
        {
            
        }
    }
}
