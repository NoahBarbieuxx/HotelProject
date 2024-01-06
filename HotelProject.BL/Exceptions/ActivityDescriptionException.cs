using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Exceptions
{
    public class ActivityDescriptionException : Exception
    {
        public ActivityDescriptionException(string? message) : base(message)
        {
        }

        public ActivityDescriptionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}