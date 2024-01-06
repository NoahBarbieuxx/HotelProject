using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.DL.Exceptions
{
    public class OrganiserRepositoryException : Exception
    {
        public OrganiserRepositoryException(string? message) : base(message)
        {
        }

        public OrganiserRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}