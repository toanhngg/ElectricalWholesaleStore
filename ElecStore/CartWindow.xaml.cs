using ElecStore.Models;
using ElecStore.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
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
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        private readonly ElectricStore1Context _context;
        private readonly User _loggedInUser;
        private CartViewModel _viewModel;

        public CartWindow(ElectricStore1Context context, User loggedInUser)
        {
            InitializeComponent();
            _context = context;
            _loggedInUser = loggedInUser;
            _viewModel = new CartViewModel(_context);
            DataContext = _viewModel;
            LoadCart();
        }
        public void LoadCart() {
                // Lấy danh sách cart của user đăng nhập
              //  var cartList = _context.Carts.Include(x => x.CommodityId).Where(c => c.UserId == _loggedInUser.UserId).ToList();
            //var query = from cart in _context.Carts
            //            join user in _context.Users on cart.UserId equals user.UserId
            //            join commodity in _context.Commodities on cart.CommodityId equals commodity.CommodityId
            //            where cart.UserId == _loggedInUser.UserId
            //            select new
            //            {
            //                CommodityName = commodity.CommodityName,
                           
            //                // Các thông tin khác từ bảng Cart, User, và Commodity
            //            };
            var query = from cart in _context.Carts
                        join user in _context.Users on cart.UserId equals user.UserId
                        join commodity in _context.Commodities on cart.CommodityId equals commodity.CommodityId
                        where cart.UserId == _loggedInUser.UserId
                        select new 
                        {
                            CommodityName = commodity.CommodityName,
                            UnitPrice = cart.UnitPrice, 
                            Quantity = cart.Quantity 
                        };



            var result = query.ToList();

            // Gán vào ItemsSource của ListView
            lvCart.ItemsSource = result;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double Total = double.Parse(lblTotal.Text.ToString());
       //     double Quantity = double.Parse(lblTotal.Text.ToString());

            //   int commodityId = selectedCommodity.CommodityId;
            InvoiceView invoice = new InvoiceView(_context, _loggedInUser, Total);
            invoice.Show();
        }
    }
}
