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
    /// Interaction logic for ViewCommodity.xaml
    /// </summary>
    public partial class ViewCommodity : Window
    {
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        //private readonly ElectricStoreContext _context;

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



        //private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}

        //private void listViewProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}
    }
}
