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
        private readonly ElectricStoreContext _context;

        public HomePage(ElectricStoreContext context)
        {
            InitializeComponent();
            _context = context;
        }
      
        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
            frMain.Content = new CommodityView(_context);



            if (tabControl.SelectedItem is TabItem selectedTab)
            {
                // Bây giờ bạn có thể sử dụng selectedTab mà không cần ép kiểu lại.
               
                if (selectedTab.Name == "Tab2")
                {
                    MessageBox.Show("hihi");
                   // frMain.Content = new CommoditView(_context);
                    // Thực hiện các hành động khi người dùng chuyển đến Tab 2
                }
                // Thêm các điều kiện cho các tab khác nếu cần thiết.
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
    }
}
