using HotelProject.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Model
{
    public class Organiser
    {
        public Organiser(int id, string name, ContactInfo contactInfo)
        {
            Id = id;
            Name = name;
            ContactInfo = contactInfo;
        }

        public Organiser(string name, ContactInfo contactInfo)
        {
            _name = name;
            _contactInfo = contactInfo;
        }

        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value <= 0)
                {
                    throw new OrganiserException("Id is invalid!");
                }
                else
                {
                    _id = value;
                }
            }
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
                    throw new OrganiserException("Name is invalid!");
                }
                else
                {
                    _name = value;
                }
            }
        }

        private ContactInfo _contactInfo;
        public ContactInfo ContactInfo
        {
            get 
            {
                return _contactInfo;
            }
            set
            {
                if (value == null)
                {
                    throw new OrganiserException("ContactInfo is invalid!");
                }
                else
                {
                    _contactInfo = value;
                }
            }
        }
    }
}