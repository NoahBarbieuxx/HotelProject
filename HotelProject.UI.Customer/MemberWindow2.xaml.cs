using HotelProject.BL.Managers;
using HotelProject.BL.Model;
using HotelProject.UI.CustomerWPF.Model;
using HotelProject.Util;
using System;
using System.Windows;

namespace HotelProject.UI.CustomerWPF
{
    public partial class MemberWindow2 : Window
    {
        private CustomerUI customerUI;
        private CustomerManager customerManager;

        public event EventHandler MemberAdded;

        public MemberWindow2(CustomerUI customerUI)
        {
            InitializeComponent();
            InitializeComponents(customerUI);
        }

        private void InitializeComponents(CustomerUI customerUI)
        {
            this.customerUI = customerUI;
            this.customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (TryParseBirthday(BirthdayTextBox.Text, out DateTime birthDay))
            {
                int customerId = customerUI.Id ?? 0;

                Member m = new Member(NameTextBox.Text, birthDay);

                Customer customer = RetrieveCustomer();

                customer.AddMember(m);

                customerManager.AddCustomer(customer);

                DialogResult = true;
                Close();

                OnMemberAdded(EventArgs.Empty);
            }
        }

        private bool TryParseBirthday(string input, out DateTime birthDay)
        {
            return DateTime.TryParse(input, out birthDay);
        }

        private Customer RetrieveCustomer()
        {
            var c = customerManager.GetCustomerById((int)customerUI.Id);

            Address a = new Address(c.ContactInfo.Address.Municipality, c.ContactInfo.Address.ZipCode, c.ContactInfo.Address.HouseNumber, c.ContactInfo.Address.Street);

            ContactInfo contactInfo = new ContactInfo(customerUI.Email, customerUI.Phone, a);

            return new Customer(customerUI.Name, (int)customerUI.Id, contactInfo);
        }

        protected virtual void OnMemberAdded(EventArgs e)
        {
            MemberAdded?.Invoke(this, e);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}