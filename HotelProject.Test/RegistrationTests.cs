using HotelProject.BL.Exceptions;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.Test
{
    public class RegistrationTests
    {
        Registration sut = new Registration(1, new Activity(1, new ActivityDescription("Latenight walk", "Ghent", 4, "Walking around in Ghent"), DateTime.Now, 5, new PriceInfo(10.05m, 8.05m, 20.5m, 16)), new Customer("Name", 1, new ContactInfo("barbieuxnoah@hotmail.com", "0491149667", new Address("Ghent", "9000", "2", "Ghentlaan"))), 20.5m);

        [Fact]
        public void IdCorrect()
        {
            Assert.True(sut.Id > 0);
            Assert.IsType<int>(sut.Id);
        }

        [Fact]
        public void IdIncorrect()
        {
            Assert.Throws<RegistrationException>(() => sut.Id = 0);
            Assert.Throws<RegistrationException>(() => sut.Id = -1);
        }

        [Fact]
        public void ActivityCorrect()
        {
            Assert.True(sut.Activity != null);
            Assert.True(sut.Activity.ActivityDescription != null);
            Assert.True(sut.Activity.PriceInfo != null);

            Assert.IsType<Activity>(sut.Activity);
            Assert.IsType<ActivityDescription>(sut.Activity.ActivityDescription);
            Assert.IsType<PriceInfo>(sut.Activity.PriceInfo);
        }

        [Fact]
        public void ActivityIncorrect()
        {
            Assert.Throws<RegistrationException>(() => sut.Activity = null);
            Assert.Throws<ActivityException>(() => sut.Activity.ActivityDescription = null);
            Assert.Throws<ActivityException>(() => sut.Activity.PriceInfo = null);
        }

        [Fact]
        public void CustomerCorrect()
        {
            Assert.True(sut.Customer != null);
            Assert.True(sut.Customer.ContactInfo != null);
            Assert.True(sut.Customer.ContactInfo.Address != null);

            Assert.IsType<Customer>(sut.Customer);
            Assert.IsType<ContactInfo>(sut.Customer.ContactInfo);
            Assert.IsType<Address>(sut.Customer.ContactInfo.Address);
        }

        [Fact]
        public void CustomerIncorrect()
        {
            Assert.Throws<RegistrationException>(() => sut.Customer = null);
            Assert.Throws<CustomerException>(() => sut.Customer.ContactInfo = null);
            Assert.Throws<ContactInfoException>(() => sut.Customer.ContactInfo.Address = null);
        }

        [Fact]
        public void GetMembersCorrect()
        {
            Assert.IsType<List<Member>>(sut.GetMembers());
        }

        [Fact]
        public void AddMemberCorrect()
        {
            Member member = new Member("John", new DateTime(1990, 1, 1));

            sut.AddMember(member);
            List<Member> result = sut.GetMembers();

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

            Assert.Throws<RegistrationException>(() => sut.AddMember(member2));
        }

        [Fact]
        public void CostCorrect()
        {
            decimal result = sut.Cost();
            Assert.IsType<decimal>(result);
        }
    }
}