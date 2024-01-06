using HotelProject.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Model
{
    public class Member
    {
        public Member(string name, DateTime birthDay)
        {
            Name = name;
            BirthDay = birthDay;
        }

        protected static List<Member> _members = new List<Member>();

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
                    throw new MemberException("Name is invalid!");
                }
                else
                {
                    _name = value;
                }
            }
        }

        private DateTime _birthDay;
        public DateTime BirthDay
        {
            get
            { 
                return _birthDay; 
            }
            set
            {
                if (value > DateTime.Now)
                {
                    throw new MemberException("Birthday is invalid!");
                }
                else
                {
                    _birthDay = value;
                }
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Member member &&
                   _name == member._name &&
                   _birthDay.Equals(member._birthDay);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_name, _birthDay);
        }

        public static void AddMember(Member member)
        {
            _members.Add(member);
        }

        public static List<Member> GetMembers()
        {
            return _members;
        }
    }
}