using ElecStore.Models;
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

namespace ElecStore
{
    /// <summary>
    /// Interaction logic for ManagerCommodityView.xaml
    /// </summary>
    public partial class ManagerCommodityView : Page
    {
        private readonly ElectricStore1Context _context;
        public ManagerCommodityView(ElectricStore1Context context)
        {
            InitializeComponent();
            _context = context;
           LoadDataListCommodity();
           // cboCategoryID.ItemsSource = _context.CommodityCategories.ToList();

        }
        public ManagerCommodityView()
        {
            InitializeComponent();
          //  LoadDataListCommodity();

        }

        public void LoadDataListCommodity()
        {
            var commodityList = _context.Commodities
               .Join(_context.CommodityCategories,
        commodity => commodity.CategoryId,
        category => category.CategoryId,
        (commodity, category) => new
        {
            CommodityID = commodity.CommodityId,
            CommodityName = commodity.CommodityName,
            UnitPrice = commodity.UnitPrice,
            UnitInStock = commodity.UnitInStock,
            CategoryName = category.CategoryName
        })
          .ToList();

            // commodityList bây giờ chứa danh sách Commodity kèm theo CategoryName
        //   listViewCommodity.ItemsSource = commodityList;
            cboCategoryID.ItemsSource = _context.CommodityCategories.ToList();
            //cboCategoryID.SelectedIndex = 1;
              //cbopaymentMethod.Items.Add("Thanh toán khi nhận hàng");
              //  cbopaymentMethod.Items.Add("Thanh toán khi qua chuyển khoản");
              //  cbopaymentMethod.SelectedItem = "Thanh toán khi nhận hàng";

        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {

        }
    }
}
