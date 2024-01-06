using HotelProject.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Model
{
    public class PriceInfo
    {
        public PriceInfo(decimal adultCost, decimal childCost, decimal discount, int adultAge)
        {
            AdultCost = adultCost;
            ChildCost = childCost;
            Discount = discount;
            AdultAge = adultAge;
        }

        private decimal _adultCost;
        public decimal AdultCost
        {
            get
            { 
                return _adultCost; 
            }
            set 
            {
                if (value < 0)
                {
                    throw new PriceInfoException("AdultCost is invalid!");
                }
                else
                {
                    _adultCost = value;
                }
            }
        }

        private decimal _childCost;
        public decimal ChildCost
        {
            get 
            {
                return _childCost;
            }
            set
            {
                if (value < 0)
                {
                    throw new PriceInfoException("ChildCost is invalid!");
                }
                else
                {
                    _childCost = value;
                }
            }
        }

        private decimal _discount;
        public decimal Discount
        {
            get 
            {
                return _discount;
            }
            set 
            {
                if (value < 0 || value > 100)
                {
                    throw new PriceInfoException("Discount is invalid!");
                }
                else
                {
                    _discount = value;
                }
            }
        }

        private int _adultAge;
        public int AdultAge
        {
            get
            { 
                return _adultAge;
            }
            set
            {
                if (value < 0)
                {
                    throw new PriceInfoException("AdultAge is invalid!");
                }
                else
                {
                    _adultAge = value;
                }
            }
        }

        public decimal Cost(List<Member> members)
        {
            decimal totalCost = 0;

            foreach (Member member in members)
            {
                int age = DateOnly.FromDateTime(DateTime.Now).Year - member.BirthDay.Year;
                DateTime birthDayThisYear = member.BirthDay.AddYears(age);

                if (DateTime.Now < birthDayThisYear)
                {
                    age--;
                }

                if (age >= AdultAge)
                {
                    totalCost += AdultCost;
                }
                else
                {
                    totalCost += ChildCost;
                }
            }

            decimal discount = (totalCost / 100) * Discount;

            return totalCost - discount;
        }
    }
}