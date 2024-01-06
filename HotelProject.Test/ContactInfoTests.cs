using HotelProject.BL.Exceptions;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.Test
{
    public class ContactInfoTests
    {
        ContactInfo sut = new ContactInfo("barbieuxnoah@hotmail.com", "0491149667", new Address("Ghent", "9000", "2", "Ghentlaan"));

        [Fact]
        public void EmailCorrect()
        {
            Assert.True(!string.IsNullOrWhiteSpace(sut.Email));
            Assert.True(sut.Email.Contains('@'));
            Assert.IsType<string>(sut.Email);
        }

        [Fact]
        public void EmailIncorrect()
        {
            Assert.Throws<ContactInfoException>(() => sut.Email = "");
            Assert.Throws<ContactInfoException>(() => sut.Email = "barbieuxnoahhotmail.com");
        }

        [Fact]
        public void PhoneCorrect()
        {
            Assert.True(!string.IsNullOrWhiteSpace(sut.Phone));
            Assert.IsType<string>(sut.Phone);
        }

        [Fact]
        public void PhoneIncorrect()
        {
            Assert.Throws<ContactInfoException>(() => sut.Phone = "");
        }

        [Fact]
        public void AddressCorrect()
        {
            Assert.True(sut.Address != null);
            Assert.IsType<Address>(sut.Address);
        }

        [Fact]
        public void AddressIncorrect()
        {
            Assert.Throws<ContactInfoException>(() => sut.Address = null);
        }
    }
}