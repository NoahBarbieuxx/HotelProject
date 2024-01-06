using HotelProject.BL.Managers;
using HotelProject.UI.CustomerWPF.Model;
using HotelProject.Util;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace HotelProject.UI.CustomerWPF
{
    public partial class OrganiserWindow : Window
    {
        private OrganiserManager organiserManager;
        private ObservableCollection<OrganiserUI> organiserUIs = new ObservableCollection<OrganiserUI>();

        public OrganiserWindow()
        {
            InitializeComponent();
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            organiserManager = new OrganiserManager(RepositoryFactory.OrganiserRepository);
            UpdateOrganiserUIs();
        }

        private void UpdateOrganiserUIs()
        {
            organiserUIs = new ObservableCollection<OrganiserUI>(organiserManager.GetOrganisers()
                .Select(x => new OrganiserUI(x.Id, x.Name, x.ContactInfo.Email, x.ContactInfo.Phone, x.ContactInfo.Address.ToString())));
            OrganiserDataGrid.ItemsSource = organiserUIs;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateOrganiserUIs();
        }

        private void MenuItemAddOrganiser_Click(object sender, RoutedEventArgs e)
        {
            OrganiserWindow2 w = new OrganiserWindow2(false, null);
            if (w.ShowDialog() == true)
            {
                UpdateOrganiserManagerAndUIs();
            }
        }

        private void UpdateOrganiserManagerAndUIs()
        {
            organiserManager = new OrganiserManager(RepositoryFactory.OrganiserRepository);
            UpdateOrganiserUIs();
        }

        private void MenuItemAddActivity_Click(object sender, RoutedEventArgs e)
        {
            if (OrganiserDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Organiser not selected", "Update");
            }
            else
            {
                ActivitiesWindow2 w = new ActivitiesWindow2(null);
                w.ShowDialog();
            }
        }

        private void MenuItemViewActivity_Click(object sender, RoutedEventArgs e)
        {
            if (OrganiserDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Organiser not selected", "ViewActivities");
            }
            else
            {
                ActivitiesWindow w = new ActivitiesWindow(null);
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
    }
}