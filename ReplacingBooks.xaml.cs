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
    /// Interaction logic for ReplacingBooks.xaml
    /// </summary>
    public partial class ReplacingBooks : Page
    {
        DeweyDecimalGenerator ddg = new DeweyDecimalGenerator();

        public ReplacingBooks()
        {
            InitializeComponent();
            ddg.UserPoints = 0;
            UserOrderTextBox.Clear();
            CallNumberTextBox.Text = ddg.GenerateCallNumbers();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Home.xaml", UriKind.Relative));
        }

        private void CheckOrderButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> userOrderedNumbers = UserOrderTextBox.Text.Split(new[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries).ToList();

            if (userOrderedNumbers.SequenceEqual(ddg.SortedCallNumbers))
            {
                ddg.UserPoints += 10;
                PointsLabel.Content = $"Points: {ddg.UserPoints}";
                MessageBox.Show("Congratulations! You got the order right.");
                MessageBox.Show("You have successfully replaced the books!");
            }
            else
            {
                MessageBox.Show("Sorry, the order is incorrect. Try Again!");
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            UserOrderTextBox.Clear();
            CallNumberTextBox.Text = ddg.GenerateCallNumbers();
        }
    }
}
