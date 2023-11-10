using ElecStore.Models;
using Microsoft.EntityFrameworkCore;
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

namespace ElecStore
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        private readonly ElectricStore1Context _context;
        private readonly User _loggedInUser;

        public HomePage(ElectricStore1Context context,User loggedInUser)
        {
            InitializeComponent();
            _context = context;
            _loggedInUser = loggedInUser;
        }
      
        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
            frMain.Content = new CommodityView(_context,_loggedInUser);



            if (tabControl.SelectedItem is TabItem selectedTab)
            {      
                
                if (selectedTab.Name == "Tab4")
                {

                    // frMain1.Content = new ManagerCommodityView(_context);
                    ViewCommodity order = new ViewCommodity(_context, _loggedInUser);

                    order.Show();
                } else if(selectedTab.Name == "Tab2")
                {
                    CustomerView cusview = new CustomerView(_context);

                    cusview.Show();
                }
                else if (selectedTab.Name == "Tab3")
                {
                    frMain1.Content = new ListOrderWindow(_context, _loggedInUser);
                }
                    
            }
        }

        private void Frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void listViewProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile(_context, _loggedInUser);

            profile.Show();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
              this.Close();
        }

        private void ChartButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            CartWindow cart = new CartWindow(_context, _loggedInUser);

            cart.Show();
        }
    }
}
