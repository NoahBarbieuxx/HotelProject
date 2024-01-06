using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Exceptions
{
    public class RegistrationException : Exception
    {
        public RegistrationException(string? message) : base(message)
        {
        }

        public RegistrationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}