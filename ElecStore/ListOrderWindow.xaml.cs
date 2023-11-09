using ElecStore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for ListOrderWindow.xaml
    /// </summary>
    public partial class ListOrderWindow : Page
    {
        private readonly ElectricStore1Context _context;
        private readonly User _loggedInUser;
        public ListOrderWindow(ElectricStore1Context context, User loggedInUser)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            _context = context;
            LoadOrder();
        }
        public void LoadOrder()
        {
            lstOrders.ItemsSource = _context.Orders.ToList();
        }
        private void lstOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Order selectedOrder = (Order)lstOrders.SelectedItem;

            if (selectedOrder != null)
            {
                Debug.WriteLine($"Selected Order ID: {selectedOrder.OrderId}");

                // Truy vấn cơ sở dữ liệu để lấy danh sách OrderDetails tương ứng với OrderID
                List<OrderDetail> orderDetails = _context.OrderDetails.Where(od => od.OrderId == selectedOrder.OrderId).ToList();

                // Gán danh sách OrderDetails cho ItemsSource của lstOrderDetails
                lstOrderDetails.ItemsSource = orderDetails;

                // Gán selectedOrder làm DataContext cho lstOrderDetails
                lstOrderDetails.DataContext = selectedOrder;
            }
        }

    }
}
