using HotelProject.BL.Managers;
using HotelProject.BL.Model;
using HotelProject.UI.CustomerWPF.Model;
using HotelProject.Util;
using System;
using System.Windows;

namespace HotelProject.UI.CustomerWPF
{
    public partial class ActivitiesWindow2 : Window
    {
        private readonly ActivityManager activityManager;
        public ActivitiesUI activitiesUI;

        public ActivitiesWindow2(ActivitiesUI activitiesUI)
        {
            InitializeComponent();
            activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
            this.activitiesUI = activitiesUI;
            IdTextBox.IsEnabled = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!TryParseDecimal(AdultCostTextBox.Text, out decimal adultCost) ||
                !TryParseDecimal(ChildCostTextBox.Text, out decimal childCost) ||
                !TryParseDecimal(DiscountTextBox.Text, out decimal discount) ||
                !TryParseInt(AdultAgeTextBox.Text, out int adultAge) ||
                !TryParseInt(DurationTextBox.Text, out int duration) ||
                !TryParseInt(AvailableSpotsTextBox.Text, out int availableSpots) ||
                !DateTime.TryParse(DateTextBox.Text, out DateTime date))
            {
                return;
            }

            if (availableSpots <= 0)
            {
                MessageBox.Show("Number of places must be greater than 0!");
                return;
            }

            if (date < DateTime.Now)
            {
                MessageBox.Show("Date needs to be in the future!");
                return;
            }

            ActivityDescription aD = new ActivityDescription(NameTextBox.Text, LocationTextBox.Text, duration, DescriptionTextBox.Text);
            PriceInfo p = new PriceInfo(adultCost, childCost, discount, adultAge);
            Activity a = new Activity(aD, date, availableSpots, p);

            activityManager.AddActivity(a);

            DialogResult = true;
            Close();
        }

        private bool TryParseDecimal(string value, out decimal result)
        {
            return Decimal.TryParse(value, out result);
        }

        private bool TryParseInt(string value, out int result)
        {
            return Int32.TryParse(value, out result);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
