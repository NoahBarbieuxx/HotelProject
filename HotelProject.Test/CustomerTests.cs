using HotelProject.BL.Exceptions;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HotelProject.Test
{
    public class CustomerTests
    {
        Customer sut = new Customer("Name", 1, new ContactInfo("barbieuxnoah@hotmail.com", "0491149667", new Address("Ghent", "9000", "2", "Ghentlaan")));

        [Fact]
        public void NameCorrect()
        {
            Assert.True(!string.IsNullOrWhiteSpace(sut.Name));
            Assert.IsType<string>(sut.Name);
        }

        [Fact]
        public void NameIncorrect()
        {
            Assert.Throws<CustomerException>(() => sut.Name = "");
        }

        [Fact]
        public void IdCorrect()
        {
            Assert.True(sut.Id > 0);
            Assert.IsType<int>(sut.Id);
        }

        [Fact]
        public void IdIncorrect()
        {
            Assert.Throws<CustomerException>(() => sut.Id = 0);
            Assert.Throws<CustomerException>(() => sut.Id = -1);
        }

        [Fact]
        public void ContactInfoCorrect()
        {
            Assert.True(sut.ContactInfo != null);
            Assert.True(sut.ContactInfo.Address != null);

            Assert.IsType<ContactInfo>(sut.ContactInfo);
            Assert.IsType<Address>(sut.ContactInfo.Address);
        }

        [Fact]
        public void ContactInfoIncorrect()
        {
            Assert.Throws<CustomerException>(() => sut.ContactInfo = null);
            Assert.Throws<ContactInfoException>(() => sut.ContactInfo.Address = null);
        }

        [Fact]
        public void GetMembersCorrect()
        {
            var members = sut.GetMembers();

            Assert.IsType<List<Member>>(members.ToList());
        }

        [Fact]
        public void AddMemberCorrect()
        {
            Member member = new Member("John", new DateTime(1990, 1, 1));

            sut.AddMember(member);
            var result = sut.GetMembers().ToList();

            Assert.Single(result);
            Assert.Contains(member, result);
            Assert.IsType<List<Member>>(result);
        }

        [Fact]
        public void AddMemberIncorrect()
        {
            Member member = new Member("John", new DateTime(1990, 1, 1));
            Member member2 = new Member("John", new DateTime(1990, 1, 1));

            sut.AddMember(member);

            Assert.Throws<CustomerException>(() => sut.AddMember(member2));
        }

        [Fact]
        public void RemoveMemberCorrect()
        {
            Member member = new Member("John", new DateTime(1990, 1, 1));

            sut.AddMember(member);
            sut.RemoveMember(member);

            var members = sut.GetMembers();

            Assert.Empty(members);
        }

        [Fact]
        public void RemoveMemberIncorrect()
        {
            Member member = new Member("John", new DateTime(1990, 1, 1));
            Member member2 = new Member("Test", new DateTime(2000, 1, 1));


            sut.AddMember(member);

            Assert.Throws<CustomerException>(() => sut.RemoveMember(member2));
        }
    }
}