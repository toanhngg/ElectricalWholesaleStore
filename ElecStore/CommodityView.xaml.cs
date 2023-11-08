using ElecStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ElecStore
{
    /// <summary>
    /// Interaction logic for CommodityView.xaml
    /// </summary>
    public partial class CommodityView : Page
    {
        private readonly ElectricStoreContext _context;
        public CommodityView(ElectricStoreContext context)
        {
            InitializeComponent();
            _context = context;
            LoadDataListCommodity();
        }
        public void LoadDataListCommodity()
        {
            listViewCommodity.ItemsSource = _context.CommodityCategories.Include(x => x.Commodities).ToList();

        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage(_context);
            homePage.Show();

           // this.Close(); // Đóng window hiện tại
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filteredResults = FilterResults(searchTextBox.Text);
           
                listViewCommodity.ItemsSource = filteredResults;
            

        }
        // Set the filtered results as the source for the ListView
        private ObservableCollection<Commodity> FilterResults(string searchText)
        {
            // Filter the results based on the text
            var filteredResults = _context.Commodities.Where(p => p.CommodityName.Contains(searchText));
            ObservableCollection<Commodity> collection = new ObservableCollection<Commodity>(filteredResults.ToList());
             
            return collection;
        }

       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var filteredResults = FilterResults(searchTextBox.Text);
            if(filteredResults.Count > 0)
            {
                listViewCommodity.ItemsSource = filteredResults;

            }
            else
            {
                MessageBox.Show("KhÔng tìm thấy sản phẩm bạn yêu cầu!");
            }

        }

        //public ViewCommodity(ElectricStoreContext context)
        //{
        //    InitializeComponent();
        //    _context = context;
        //   LoadDataListCommodity();
        //}
        //public void LoadDataListCommodity()
        //{
        //    listViewCommodity.ItemsSource = _context.CommodityCategories.Include(x => x.Commodities).ToList();

        //}

    }
}
