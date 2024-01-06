using HotelProject.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Model
{
    public class Activity
    {
        public Activity(ActivityDescription activityDescription, DateTime fixture, int numberOfPlaces, PriceInfo priceInfo)
        {
            ActivityDescription = activityDescription;
            Fixture = fixture;
            NumberOfPlaces = numberOfPlaces;
            PriceInfo = priceInfo;
        }

        public Activity(int id, ActivityDescription activityDescription, DateTime fixture, int numberOfPlaces, PriceInfo priceInfo)
        {
            Id = id;
            ActivityDescription = activityDescription;
            Fixture = fixture;
            NumberOfPlaces = numberOfPlaces;
            PriceInfo = priceInfo;
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
                    throw new ActivityException("Id is invalid!");
                }
                else
                {
                    _id = value;
                }
            }
        }

        private DateTime _fixture;
        public DateTime Fixture
        {
            get
            {
                return _fixture;
            }
            set
            {
                _fixture = value;
            }
        }

        private int _numberOfPlaces;
        public int NumberOfPlaces
        {
            get
            {
                return _numberOfPlaces;
            }
            set
            {
                if (value < 0)
                {
                    throw new ActivityException("NumberOfPlaces is invalid!");
                }
                else
                {
                    _numberOfPlaces = value;
                }
            }
        }

        private ActivityDescription _activityDescription;
        public ActivityDescription ActivityDescription
        {
            get
            {
                return _activityDescription;
            }
            set
            {
                if (value == null)
                {
                    throw new ActivityException("ActivityDescription is invalid!");
                }
                else
                {
                    _activityDescription = value;
                }
            }
        }

        private PriceInfo _priceInfo;
        public PriceInfo PriceInfo
        {
            get
            {
                return _priceInfo;
            }
            set
            {
                if (value == null)
                {
                    throw new ActivityException("PriceInfo is invalid!");
                }
                else
                {
                    _priceInfo = value;
                }
            }
        }

        public decimal Cost(List<Member> members)
        {
            return PriceInfo.Cost(members);
        }
    }
}