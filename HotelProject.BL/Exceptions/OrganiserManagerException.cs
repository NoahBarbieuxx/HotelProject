using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Exceptions
{
    public class OrganiserManagerException : Exception
    {
        public OrganiserManagerException(string? message) : base(message)
        {
        }

        public OrganiserManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}