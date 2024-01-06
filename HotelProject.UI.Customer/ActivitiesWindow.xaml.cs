using HotelProject.BL.Managers;
using HotelProject.BL.Model;
using HotelProject.UI.CustomerWPF.Model;
using HotelProject.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace HotelProject.UI.CustomerWPF
{
    public partial class ActivitiesWindow : Window
    {
        private readonly ActivityManager activityManager;
        private readonly ObservableCollection<ActivitiesUI> activitiesUIs = new ObservableCollection<ActivitiesUI>();
        private readonly CustomerUI customerUI;

        public ActivitiesWindow(CustomerUI customerUI)
        {
            InitializeComponent();
            activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
            this.customerUI = customerUI;

            InitializeActivitiesDataGrid();
        }

        private void InitializeActivitiesDataGrid()
        {
            var activities = activityManager.GetAllActivities()
                .Select(x => new ActivitiesUI(x.Id, x.ActivityDescription.Description, x.ActivityDescription.Location, x.ActivityDescription.Duration, x.ActivityDescription.Name, x.Fixture, x.NumberOfPlaces, x.PriceInfo.AdultCost, x.PriceInfo.ChildCost, x.PriceInfo.AdultAge ,x.PriceInfo.Discount));


            List<ActivitiesUI> activitiesFuture = new List<ActivitiesUI>();

            foreach(ActivitiesUI activity in activities)
            {
                if (activity.Date >= DateTime.Now)
                {
                    activitiesFuture.Add(activity);
                }
            }

            ActivitiesDataGrid.ItemsSource = new ObservableCollection<ActivitiesUI>(activitiesFuture);
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            ShowStartWindow();
        }

        private void RegistrateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (ActivitiesDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Activity not selected");
            }
            else
            {
                if (customerUI == null)
                {
                    MessageBox.Show("Customer not selected", "RegistrateCustomer");
                }
                else
                {
                    OpenRegistrateWindow();
                }
            }
        }

        private void OpenRegistrateWindow()
        {
            var activitiesUI = (ActivitiesUI)ActivitiesDataGrid.SelectedItem;
            var registrateWindow = new RegistrateWindow(activitiesUI, customerUI);
            registrateWindow.Show();
            Close();
        }

        private void ShowStartWindow()
        {
            var startWindow = new StartWindow();
            startWindow.Show();
            Close();
        }
    }
}
