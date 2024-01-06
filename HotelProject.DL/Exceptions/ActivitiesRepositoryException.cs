using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.DL.Exceptions
{
    public class ActivitiesRepositoryException : Exception
    {
        public ActivitiesRepositoryException(string? message) : base(message)
        {
        }

        public ActivitiesRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}