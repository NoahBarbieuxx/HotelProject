using HotelProject.BL.Exceptions;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.Test
{
    public class AddressTests
    {
        Address sut = new Address("Ghent", "9000", "2", "Ghentlaan");

        [Fact]
        public void MunicipalityCorrect()
        {
            Assert.True(!string.IsNullOrWhiteSpace(sut.Municipality));
            Assert.IsType<string>(sut.Municipality);
        }

        [Fact]
        public void MunicipalityIncorrect()
        {
            Assert.Throws<AddressException>(() => sut.Municipality = "");
        }

        [Fact]
        public void ZipCodeCorrect()
        {
            Assert.True(!string.IsNullOrWhiteSpace(sut.ZipCode));
            Assert.IsType<string>(sut.ZipCode);
        }

        [Fact]
        public void ZipCodeIncorrect()
        {
            Assert.Throws<AddressException>(() => sut.ZipCode = "");
        }

        [Fact]
        public void HouseNumberCorrect()
        {
            Assert.True(!string.IsNullOrWhiteSpace(sut.HouseNumber));
            Assert.IsType<string>(sut.HouseNumber);
        }

        [Fact]
        public void HouseNumberIncorrect()
        {
            Assert.Throws<AddressException>(() => sut.HouseNumber = "");
        }

        [Fact]
        public void StreetCorrect()
        {
            Assert.True(!string.IsNullOrWhiteSpace(sut.Street));
            Assert.IsType<string>(sut.Street);
        }

        [Fact]
        public void StreetIncorrect()
        {
            Assert.Throws<AddressException>(() => sut.Street = "");
        }

        [Fact]
        public void ToAddressLineCorrect()
        {
            var result = sut.ToAddressLine();

            Assert.Equal("Ghent|9000|Ghentlaan|2", result);
        }
    }
}