using HotelProject.BL.Exceptions;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.Test
{
    public class ActivityTests
    {
        Activity sut = new Activity(1, new ActivityDescription("Latenight walk", "Ghent", 4, "Walking around in Ghent"), DateTime.Now, 5, new PriceInfo(10.05m, 8.05m, 20.5m, 16));

        [Fact]
        public void IdCorrect()
        {
            Assert.True(sut.Id > 0);
            Assert.IsType<int>(sut.Id);
        }

        [Fact]
        public void IdIncorrect()
        {
            Assert.Throws<ActivityException>(() => sut.Id = 0);
            Assert.Throws<ActivityException>(() => sut.Id = -1);
        }

        [Fact]
        public void ActivityDescriptionCorrect()
        {
            Assert.True(sut.ActivityDescription != null);
            Assert.IsType<ActivityDescription>(sut.ActivityDescription);
        }

        [Fact]
        public void ActivityDescriptionIncorrect()
        {
            Assert.Throws<ActivityException>(() => sut.ActivityDescription = null);
        }

        [Fact]
        public void FixtureCorrect()
        {
            Assert.IsType<DateTime>(sut.Fixture);
            Assert.IsType<DateTime>(sut.Fixture);
        }

        [Fact]
        public void NumberOfPlaceCorrect()
        {
            Assert.True(sut.NumberOfPlaces > 0);
            Assert.IsType<int>(sut.NumberOfPlaces);
        }

        [Fact]
        public void NumberOfPlaceIncorrect()
        {
            Assert.Throws<ActivityException>(() => sut.NumberOfPlaces = -1);
        }

        [Fact]
        public void PriceInfoCorrect()
        {
            Assert.True(sut.PriceInfo != null);
            Assert.IsType<PriceInfo>(sut.PriceInfo);
        }

        [Fact]
        public void PriceInfoIncorrect()
        {
            Assert.Throws<ActivityException>(() => sut.PriceInfo = null);
        }

        [Fact]
        public void CostCorrect()
        {
            List<Member> members = new List<Member>();
            Member member = new Member("Name", new DateTime(1990, 1, 1));
            members.Add(member);

            decimal result = sut.Cost(members);

            Assert.IsType<decimal>(result);
        }
    }
}