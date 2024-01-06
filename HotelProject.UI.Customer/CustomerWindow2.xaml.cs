using HotelProject.BL.Managers;
using HotelProject.BL.Model;
using HotelProject.UI.CustomerWPF.Model;
using HotelProject.Util;
using System;
using System.Windows;

namespace HotelProject.UI.CustomerWPF
{
    public partial class CustomerWindow2 : Window
    {
        private CustomerManager customerManager;
        public CustomerUI customerUI;
        private bool isUpdate;

        public CustomerWindow2(bool isUpdate, CustomerUI customerUI)
        {
            InitializeComponent();
            InitializeComponents(isUpdate, customerUI);
        }

        private void InitializeComponents(bool isUpdate, CustomerUI customerUI)
        {
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
            this.customerUI = customerUI;
            this.isUpdate = isUpdate;

            if (customerUI != null)
            {
                IdTextBox.Text = customerUI.Id.ToString();
                NameTextBox.Text = customerUI.Name;
                EmailTextBox.Text = customerUI.Email;
                PhoneTextBox.Text = customerUI.Phone;
            }

            IdTextBox.IsEnabled = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (isUpdate)
            {
                UpdateCustomer();
            }
            else
            {
                AddNewCustomer();
            }

            DialogResult = true;
            Close();
        }

        private void UpdateCustomer()
        {
            customerUI.Name = NameTextBox.Text;
            customerUI.Email = EmailTextBox.Text;
            customerUI.Phone = PhoneTextBox.Text;

            var c = customerManager.GetCustomerById((int)customerUI.Id);

            Address a = new Address(c.ContactInfo.Address.Municipality, c.ContactInfo.Address.ZipCode, c.ContactInfo.Address.HouseNumber, c.ContactInfo.Address.Street);

            ContactInfo contactInfo = new ContactInfo(customerUI.Email, customerUI.Phone, a);

            Customer customer = new Customer(customerUI.Name, (int)customerUI.Id, contactInfo);

            customerManager.UpdateCustomer(customer);
        }

        private void AddNewCustomer()
        {
            Customer c = new Customer(NameTextBox.Text, new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, new Address(CityTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text, StreetTextBox.Text)));

            customerManager.AddCustomer(c);

            customerUI = new CustomerUI(c.Id, c.Name, c.ContactInfo.Email, c.ContactInfo.Phone, c.ContactInfo.Address.ToString(), c.GetMembers().Count);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
