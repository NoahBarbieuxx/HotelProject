using HotelProject.BL.Managers;
using HotelProject.UI.CustomerWPF.Model;
using HotelProject.UI.CustomerWPF;
using HotelProject.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HotelProject.BL.Model;
using System.Diagnostics;
using Activity = HotelProject.BL.Model.Activity;
using System.Xml.Linq;

namespace HotelProject.UI.CustomerWPF
{
    public partial class RegistrateWindow : Window
    {
        private ActivityManager activityManager;
        private CustomerManager customerManager;
        private RegistrationManager registrationManager;
        private ActivitiesUI activitiesUI;
        private CustomerUI customerUI;

        private List<Member> selectedMembers;

        public RegistrateWindow(ActivitiesUI activitiesUI, CustomerUI customerUI)
        {
            InitializeComponent();
            activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
            registrationManager = new RegistrationManager(RepositoryFactory.RegistrationRepository);

            this.activitiesUI = activitiesUI;
            this.customerUI = customerUI;

            DataContext = activitiesUI;


            int CustomerId = (int)customerUI.Id;

            Customer customer = customerManager.GetCustomerById(CustomerId);

            List<Member> members = customer.GetMembers().ToList();

            MemberDataGrid.ItemsSource = members;
            MemberDataGrid.SelectionChanged += MemberDataGrid_SelectionChanged;
        }

        private void MemberDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedMembers = MemberDataGrid.SelectedItems.Cast<Member>().ToList();

            UpdateRegistrationCost();
        }

        private void UpdateRegistrationCost()
        {
            if (registrationManager != null && activitiesUI != null)
            {
                Customer customer = customerManager.GetCustomerById(customerUI.Id ?? 0);
                ActivityDescription activityDescription = new ActivityDescription(activitiesUI.Name, activitiesUI.Location, activitiesUI.Duration, activitiesUI.Description);
                PriceInfo priceInfo = new PriceInfo(activitiesUI.AdultCost, activitiesUI.ChildCost, activitiesUI.Discount, activitiesUI.AdultAge);
                Activity activity = new Activity(activitiesUI.Id ?? 0, activityDescription, activitiesUI.Date, activitiesUI.AvailableSpots, priceInfo);

                Registration registration = new Registration(activity, customer);

                foreach(Member member in selectedMembers)
                {
                    registration.AddMember(member);
                }

                decimal cost = registration.Cost();

                CostTextBlock.Text = $"Total Cost: {cost:C}";
            }
        }

        private void AddRegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = customerManager.GetCustomerById(customerUI.Id ?? 0);
            ActivityDescription activityDescription = new ActivityDescription(activitiesUI.Name, activitiesUI.Location, activitiesUI.Duration, activitiesUI.Description);
            PriceInfo priceInfo = new PriceInfo(activitiesUI.AdultCost, activitiesUI.ChildCost, activitiesUI.Discount, activitiesUI.AdultAge);
            Activity activity = new Activity(activitiesUI.Id ?? 0, activityDescription, activitiesUI.Date, activitiesUI.AvailableSpots, priceInfo);

            Registration registration = new Registration(activity, customer);

            var registrations = registrationManager.GetRegistrationByActivityId(activity.Id);

            if (selectedMembers == null)
            {
                MessageBox.Show("No members selected!");
                return;
            }
            else
            {
                foreach (Member member in selectedMembers)
                {
                    if (registrations.Count() == 0)
                    {
                        registration.AddMember(member);
                    }
                    else
                    {
                        foreach (Registration r in registrations)
                        {
                            var exsit = registrationManager.MemberRegistrated(member.Name, r.Id);
                            if (exsit == true)
                            {
                                MessageBox.Show($"Member {member.Name} is already registrated to this activity");
                            }
                            else
                            {
                                registration.AddMember(member);
                            }
                        }
                    }
                }
            }

            if (selectedMembers.Count > activitiesUI.AvailableSpots)
            {
                MessageBox.Show("Too many people: avalaibleSpots overrided!");
                return;
            }

            registrationManager.AddRegistration(registration);

            int newAvalaibleSpots = activitiesUI.AvailableSpots - selectedMembers.Count;
            activity.NumberOfPlaces = newAvalaibleSpots;
            activityManager.UpdateActivity(activity);

            ActivitiesWindow a = new ActivitiesWindow(null);
            a.Show();
            Close();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            StartWindow s = new StartWindow();
            s.Show();
            Close();
        }
    }
}