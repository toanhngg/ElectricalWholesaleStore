using ElecStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private readonly ElectricStore1Context _context;

        public ViewCommodity(ElectricStore1Context context)
        {
            InitializeComponent();
            this._context = context;
            LoadDataListCommodity();
        }
        public void LoadDataListCommodity()
        {
          
            //    var commodityList = _context.Commodities
            //       .Join(_context.CommodityCategories,
            //commodity => commodity.CategoryId,
            //category => category.CategoryId,
            //(commodity, category) => new
            //{
            //    CommodityID = commodity.CommodityId,
            //    CommodityName = commodity.CommodityName,
            //    UnitPrice = commodity.UnitPrice,
            //    UnitInStock = commodity.UnitInStock,
            //    CategoryName = category.CategoryName
            //})
            //  .ToList();
                listViewCommodity.ItemsSource = _context.Commodities.Include(x => x.Category).ToList();
            cboCategoryID.ItemsSource = _context.CommodityCategories.ToList();

        }
        public Commodity getInfor()
        {
            Commodity commodity = null;
            try
            {

                commodity = new Commodity
                {
                    CommodityId = string.IsNullOrEmpty(CommodityId.Text) ? 0 : int.Parse(CommodityId.Text),
                    CategoryId = Int32.Parse(cboCategoryID.SelectedValue.ToString()),
                    CommodityName = CommodityName.Text,
                    UnitPrice = decimal.Parse(UnitPrice.Text),
                    UnitInStock = Int32.Parse(UnitInStock.Text)
                };

            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot get Commidity", "Get Commidity");
            }
            return commodity;
        }
        private void UnitPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void UnitInStock_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //private void listViewCommodity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    //if (listViewCommodity.SelectedIndex != -1)
        //    //{
        //    //    Commodity selectedItem = listViewCommodity.SelectedItem as Commodity;

        //    //    CommodityId.Text = selectedItem.ToString();
        //    //    cboCategoryID.SelectedValue = selectedItem.CategoryId;
        //    //    CommodityName.Text = selectedItem.CommodityName;
        //    //    UnitPrice.Text = selectedItem.UnitPrice.ToString();
        //    //    UnitInStock.Text = selectedItem.UnitInStock.ToString();
        //    //}
        //}

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show("hii");
            try
            {

                var p = getInfor();
                if (p != null)
                {
                //     p.CommodityId = 0;
                    _context.Commodities.Add(p);
                    _context.SaveChanges();
                    LoadDataListCommodity();
                    MessageBox.Show("Add Commodity Success!", "Add Commodity");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Commodity");
            }
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Refresh(object sender, RoutedEventArgs e)
        {
            CommodityId.Text = string.Empty;
            cboCategoryID.SelectedIndex = -1;
            CommodityName.Text = string.Empty;
            UnitPrice.Text = string.Empty;
            UnitInStock.Text = string.Empty;
        }



        //private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}

        //private void listViewProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}
    }
}
