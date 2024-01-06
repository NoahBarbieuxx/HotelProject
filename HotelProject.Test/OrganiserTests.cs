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
    public class OrganiserTests
    {
        Organiser sut = new Organiser(1, "Name", new ContactInfo("barbieuxnoah@hotmail.com", "0491149667", new Address("Ghent", "9000", "2", "Ghentlaan")));

        [Fact]
        public void IdCorrect()
        {
            Assert.True(sut.Id > 0);
            Assert.IsType<int>(sut.Id);
        }

        [Fact]
        public void IdIncorrect()
        {
            Assert.Throws<OrganiserException>(() => sut.Id = 0);
            Assert.Throws<OrganiserException>(() => sut.Id = -1);
        }

        [Fact]
        public void NameCorrect()
        {
            Assert.True(!string.IsNullOrWhiteSpace(sut.Name));
            Assert.IsType<string>(sut.Name);
        }

        [Fact]
        public void NameIncorrect()
        {
            Assert.Throws<OrganiserException>(() => sut.Name = "");
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
            Assert.Throws<OrganiserException>(() => sut.ContactInfo = null);
            Assert.Throws<ContactInfoException>(() => sut.ContactInfo.Address = null);
        }
    }
}