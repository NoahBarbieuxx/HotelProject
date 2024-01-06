using HotelProject.BL.Exceptions;

namespace HotelProject.BL.Model
{
    public class Customer
    {
        public Customer(string name, int id, ContactInfo contactInfo)
        {
            Name = name;
            Id = id;
            ContactInfo = contactInfo;
        }

        public Customer(string name, ContactInfo contactInfo)
        {
            Name = name;
            ContactInfo = contactInfo;
        }

        private List<Member> _members = new List<Member>();

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
                    throw new CustomerException("Name is invalid!");
                }
                else
                {
                    _name = value;
                }
            }
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
                    throw new CustomerException("Id is invalid!");
                }
                else
                {
                    _id = value;
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
                    throw new CustomerException("ContactInfo is invalid!");
                }
                else
                {
                    _contactInfo = value;
                }
            }
        }

        public IReadOnlyList<Member> GetMembers()
        {
            return _members.AsReadOnly();
        }

        public void AddMember(Member member) 
        {
            if (!_members.Contains(member))
            {
                _members.Add(member);
            }
            else
            {
                throw new CustomerException("AddMember");
            }
        }

        public void RemoveMember(Member member)
        {
            if (_members.Contains(member))
            {
                _members.Remove(member);
            }
            else
            {
                throw new CustomerException("RemoveMember");
            }
        }
    }
}