using System;
using System.Collections.Generic;
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

namespace HotelProject.UI.CustomerWPF
{
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void Click_Customer(object sender, RoutedEventArgs e)
        {
            CustomerWindow customerWindow = new CustomerWindow();
            customerWindow.Show();

            Close();
        }

        private void Click_Organiser(object sender, RoutedEventArgs e)
        {
            OrganiserWindow organiserWindow = new OrganiserWindow();
            organiserWindow.Show();

            Close();
        }

        private void Click_Activities(object sender, RoutedEventArgs e)
        {
            ActivitiesWindow activitiesWindow = new ActivitiesWindow(null);
            activitiesWindow.Show();

            Close();
        }
    }
}