using HotelProject.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Model
{
    public class Registration
    {
        public Registration(int id, Activity activity, Customer customer, decimal cost)
        {
            Id = id;
            Activity = activity;
            Customer = customer;
        }

        public Registration(Activity activity, Customer customer)
        {
            Activity = activity;
            Customer = customer;
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
                    throw new RegistrationException("Id is invalid!");
                }
                else
                {
                    _id = value;
                }
            }
        }

        private Activity _activity;
        public Activity Activity
        {
            get
            {
                return _activity;
            }
            set
            {
                if (value == null)
                {
                    throw new RegistrationException("Activity is invalid!");
                }
                else
                {
                    _activity = value;
                }
            }
        }

        private Customer _customer;
        public Customer Customer
        {
            get
            {
                return _customer;
            }
            set
            {
                if (value == null)
                {
                    throw new RegistrationException("Customer is invalid!");
                }
                else
                {
                    _customer = value;
                }
            }
        }

        private List<Member> _members = new List<Member>();
        public List<Member> Members
        {
            get
            {
                return GetMembers();
            }
        }

        public List<Member> GetMembers()
        {
            return _members;
        }

        public void AddMember(Member member)
        {
            if (!_members.Contains(member))
            {
                _members.Add(member);
            }
            else
            {
                throw new RegistrationException("AddMember");
            }
        }

        public decimal Cost()
        {
            return Activity.Cost(Members);
        }
    }
}