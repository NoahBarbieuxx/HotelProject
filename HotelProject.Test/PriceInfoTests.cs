using HotelProject.BL.Exceptions;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.Test
{
    public class PriceInfoTests
    {
        PriceInfo sut = new PriceInfo(10.05m, 8.05m, 20.5m, 16);

        [Fact]
        public void AdultCostCorrect()
        {
            Assert.True(sut.AdultCost > -1);
            Assert.IsType<decimal>(sut.AdultCost);
        }

        [Fact]
        public void AdultCostIncorrect()
        {
            Assert.Throws<PriceInfoException>(() => sut.AdultCost = -1);
        }

        [Fact]
        public void ChildCostCorrect()
        {
            Assert.True(sut.ChildCost > -1);
            Assert.IsType<decimal>(sut.ChildCost);
        }

        [Fact]
        public void ChildCostIncorrect()
        {
            Assert.Throws<PriceInfoException>(() => sut.ChildCost = -1);
        }

        [Fact]
        public void DiscountCorrect()
        {
            Assert.True(sut.Discount > 0 || sut.Discount < 101);
            Assert.IsType<decimal>(sut.Discount);
        }

        [Fact]
        public void DiscountIncorrect()
        {
            Assert.Throws<PriceInfoException>(() => sut.Discount = -1);
            Assert.Throws<PriceInfoException>(() => sut.Discount = 101);
        }

        [Fact]
        public void AdultAgeCorrect()
        {
            Assert.True(sut.AdultAge > -1);
            Assert.IsType<int>(sut.AdultAge);
        }

        [Fact]
        public void AdultAgeIncorrect()
        {
            Assert.Throws<PriceInfoException>(() => sut.AdultAge = -1);
        }

        [Fact]
        public void CostCorrect()
        {
            Member member1 = new Member("John", new DateTime(1990, 1, 1));
            Member member2 = new Member("Jane", new DateTime(2014, 1, 1));

            List<Member> members = new List<Member> { member1, member2 };

            decimal result = sut.Cost(members);

            decimal costWithoutDiscount = 10.05m + 8.05m;
            decimal discountCalculated = ((10.05m + 8.05m) / 100) * 20.5m;
            decimal expectedCost = costWithoutDiscount - discountCalculated;

            Assert.Equal(expectedCost, result);
            Assert.IsType<decimal>(result);
        }

        [Fact]
        public void CostWithoutMembers()
        {
            List<Member> members = new List<Member>();

            decimal result = sut.Cost(members);

            Assert.Equal(0m, result); 
            Assert.IsType<decimal>(result);
        }
    }
}