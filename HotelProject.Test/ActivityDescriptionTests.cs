using HotelProject.BL.Exceptions;
using HotelProject.BL.Model;
using System.Xml.Linq;

namespace HotelProject.Test
{
    public class ActivityDescriptionTests
    {
        ActivityDescription sut = new ActivityDescription("Latenight walk", "Ghent", 4, "Walking around in Ghent");

        [Fact]
        public void NameCorrect()
        {
            Assert.True(!string.IsNullOrWhiteSpace(sut.Name));
            Assert.IsType<string>(sut.Name);
        }

        [Fact]
        public void NameIncorrect()
        {
            Assert.Throws<ActivityDescriptionException>(() => sut.Name = "");
        }

        [Fact]
        public void LocationCorrect()
        {
            Assert.True(!string.IsNullOrWhiteSpace(sut.Location));
            Assert.IsType<string>(sut.Location);
        }

        [Fact]
        public void LocationIncorrect()
        {
            Assert.Throws<ActivityDescriptionException>(() => sut.Location = "");
        }

        [Fact]
        public void DurationCorrect()
        {
            Assert.True(sut.Duration > 0);
            Assert.IsType<int>(sut.Duration);
        }

        [Fact]
        public void  DurationIncorrect()
        {
            Assert.Throws<ActivityDescriptionException>(() => sut.Duration = 0);
            Assert.Throws<ActivityDescriptionException>(() => sut.Duration = -1);
        }

        [Fact]
        public void DescriptionCorrect()
        {
            Assert.True(!string.IsNullOrWhiteSpace(sut.Description));
            Assert.IsType<string>(sut.Description);
        }

        [Fact]
        public void DescriptionIncorrect()
        {
            Assert.Throws<ActivityDescriptionException>(() => sut.Description = "");
        }
    }
}