using HotelProject.BL.Managers;
using HotelProject.BL.Model;
using HotelProject.UI.CustomerWPF.Model;
using HotelProject.Util;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace HotelProject.UI.CustomerWPF
{
    public partial class CustomerWindow : Window
    {
        private CustomerManager customerManager;
        private ObservableCollection<CustomerUI> customersUIs;

        public CustomerWindow()
        {
            InitializeComponent();
            InitializeCustomerDataGrid();
        }

        private void InitializeCustomerDataGrid()
        {
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
            customersUIs = new ObservableCollection<CustomerUI>(customerManager.GetCustomers(null).Select(MapToCustomerUI));
            CustomerDataGrid.ItemsSource = customersUIs;
        }

        private CustomerUI MapToCustomerUI(Customer customer)
        {
            return new CustomerUI(customer.Id, customer.Name, customer.ContactInfo.Email, customer.ContactInfo.Phone, customer.ContactInfo.Address.ToString(), customer.GetMembers().Count);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            customersUIs = new ObservableCollection<CustomerUI>(customerManager.GetCustomers(SearchTextBox.Text).Select(MapToCustomerUI));
            CustomerDataGrid.ItemsSource = customersUIs;
        }

        private void MenuItemAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow2 w = new CustomerWindow2(false, null);
            if (w.ShowDialog() == true)
            {
                customersUIs.Add(w.customerUI);
                RefreshCustomerDataGrid();
            }
        }

        private void MenuItemDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Customer not selected", "Delete");
            }
            else
            {
                CustomerUI selectedCustomerUI = (CustomerUI)CustomerDataGrid.SelectedItem;
                Customer customer = customerManager.GetCustomerById((int)selectedCustomerUI.Id);
                customerManager.DeleteCustomer(customer);
                customersUIs.Remove(selectedCustomerUI);
            }
        }

        private void MenuItemUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Customer not selected", "Update");
            }
            else
            {
                CustomerWindow2 w = new CustomerWindow2(true, (CustomerUI)CustomerDataGrid.SelectedItem);
                w.ShowDialog();
                RefreshCustomerDataGrid();
            }
        }

        private void MenuItemAddMember_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Customer not selected", "AddMember");
            }
            else
            {
                MemberWindow2 w = new MemberWindow2((CustomerUI)CustomerDataGrid.SelectedItem);
                w.MemberAdded += MemberAddedHandler;
                w.ShowDialog();
            }
        }

        private void MemberAddedHandler(object sender, EventArgs e)
        {
            RefreshCustomerDataGrid();
        }

        private void MenuItemViewActivities_Click(object sender, EventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Customer not selected", "ViewActivities");
            }
            else
            {
                CustomerUI selectedCustomer = (CustomerUI)CustomerDataGrid.SelectedItem;
                ActivitiesWindow w = new ActivitiesWindow(selectedCustomer);
                w.Show();
                Close();
            }
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            StartWindow s = new StartWindow();
            s.Show();
            Close();
        }

        private void RefreshCustomerDataGrid()
        {
            customersUIs = new ObservableCollection<CustomerUI>(customerManager.GetCustomers(null).Select(MapToCustomerUI));
            CustomerDataGrid.ItemsSource = customersUIs;
        }
    }
}
