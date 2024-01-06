using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Exceptions
{
    public class RegistrationManagerException : Exception
    {
        public RegistrationManagerException(string? message) : base(message)
        {
        }

        public RegistrationManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}