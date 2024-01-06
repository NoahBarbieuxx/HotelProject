using HotelProject.BL.Managers;
using HotelProject.BL.Model;
using HotelProject.UI.CustomerWPF.Model;
using HotelProject.Util;
using System.Windows;

namespace HotelProject.UI.CustomerWPF
{
    public partial class OrganiserWindow2 : Window
    {
        public OrganiserUI OrganiserUI { get; private set; }
        private readonly bool isUpdate;
        private readonly OrganiserManager organiserManager;

        public OrganiserWindow2(bool isUpdate, OrganiserUI organiserUI)
        {
            InitializeComponent();
            organiserManager = new OrganiserManager(RepositoryFactory.OrganiserRepository);
            OrganiserUI = organiserUI;
            this.isUpdate = isUpdate;

            if (OrganiserUI != null)
            {
                IdTextBox.Text = OrganiserUI.Id.ToString();
                NameTextBox.Text = OrganiserUI.Name;
                EmailTextBox.Text = OrganiserUI.Email;
                PhoneTextBox.Text = OrganiserUI.Phone;
            }

            IdTextBox.IsEnabled = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (isUpdate)
            {
                // Implementeer logica voor bijwerken als dat nodig is.
            }
            else
            {
                Organiser organiser = new Organiser(NameTextBox.Text, new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, new Address(CityTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text, StreetTextBox.Text)));
                organiserManager.AddOrganiser(organiser);
                OrganiserUI = new OrganiserUI(organiser.Id, organiser.Name, organiser.ContactInfo.Email, organiser.ContactInfo.Phone, organiser.ContactInfo.Address.ToString());
            }

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
