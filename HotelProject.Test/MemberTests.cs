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
    public class MemberTests
    {
        Member sut = new Member("Name", new DateTime(1990, 1, 1));

        [Fact]
        public void NameCorrect()
        {
            Assert.True(!string.IsNullOrWhiteSpace(sut.Name));
            Assert.IsType<string>(sut.Name);
        }

        [Fact]
        public void NameIncorrect()
        {
            Assert.Throws<MemberException>(() => sut.Name = "");
        }

        [Fact]
        public void BirthdayCorrect()
        {
            Assert.True(sut.BirthDay < DateTime.Now);

            DateTime birthday = DateTime.Now;
            Assert.True(birthday.Date == DateTime.Now.Date);

            Assert.IsType<DateTime>(sut.BirthDay);
        }

        [Fact]
        public void BirthdayIncorrect()
        {
            Assert.Throws<MemberException>(() => sut.BirthDay = DateTime.Now.AddDays(1));
        }
    }
}