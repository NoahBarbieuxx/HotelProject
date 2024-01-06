using HotelProject.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Model
{
    public class ActivityDescription
    {
        public ActivityDescription(string name, string location, int duration, string description)
        {
            Name = name;
            Location = location;
            Duration = duration;
            Description = description;
        }

        private string _name;
        public string Name
        {
            get 
            { 
                return _name; 
            }
            set 
            { 
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ActivityDescriptionException("Name is invalid!");
                }
                else
                {
                    _name = value;
                }
            }
        }

        private string _location;
        public string Location
        {
            get 
            { 
                return _location;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ActivityDescriptionException("Location is invalid!");
                }
                else
                {
                    _location = value;
                }
            }
        }

        private int _duration;
        public int Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ActivityDescriptionException("Duration is invalid!");
                }
                else
                {
                    _duration = value;
                }
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ActivityDescriptionException("Desciption is invalid!");
                }
                else
                {
                    _description = value;
                }
            }
        }
    }
}