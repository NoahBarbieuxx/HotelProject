using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.UI.CustomerWPF.Model
{
    public class ActivitiesUI : INotifyPropertyChanged
    {
        public ActivitiesUI(int? id, string description, string location, int duration, string name, DateTime date, int availableSpots, decimal adultCost, decimal childCost, int adultAge, decimal discount)
        {
            this.id = id;
            this.description = description;
            this.location = location;
            this.duration = duration;
            this.name = name;
            this.date = date;
            this.availableSpots = availableSpots;
            this.adultCost = adultCost;
            this.childCost = childCost;
            this.adultAge = adultAge;
            this.discount = discount;
        }

        private int? id;
        public int? Id { get { return id; } set { id = value; OnPropertyChanged(); } }

        private string description;
        public string Description { get { return description; } set { description = value; OnPropertyChanged(); } }

        private string location;
        public string Location { get { return location; } set { location = value; OnPropertyChanged(); } }

        private int duration;
        public int Duration { get { return duration; } set { duration = value; OnPropertyChanged(); } }

        private string name;
        public string Name { get { return name; } set { name = value; OnPropertyChanged(); } }

        private DateTime date;
        public DateTime Date { get { return date; } set { date = value; OnPropertyChanged(); } }

        private int availableSpots;
        public int AvailableSpots { get { return availableSpots; } set { availableSpots = value; OnPropertyChanged(); } }

        private decimal adultCost;
        public decimal AdultCost { get { return adultCost; } set { adultCost = value; OnPropertyChanged(); } }

        private decimal childCost;
        public decimal ChildCost { get { return childCost; } set { childCost = value; OnPropertyChanged(); } }

        private int adultAge;
        public int AdultAge { get { return adultAge; } set { adultAge = value; OnPropertyChanged(); } }

        private decimal discount;
        public decimal Discount { get { return discount; } set { discount = value; OnPropertyChanged(); } }

        private void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}