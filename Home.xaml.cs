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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dewey_Decimal_Training_App
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }

        private void StartReplacingBooksTask_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("ReplacingBooks.xaml", UriKind.Relative));
        }

        private void StartIdentifyingTask_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("IdentifyAreas.xaml", UriKind.Relative));
        }

        private void StartFindingCallNumbers_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This feature is not available yet.");
        }
    }
}
