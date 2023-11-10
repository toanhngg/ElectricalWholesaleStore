using DocumentFormat.OpenXml.Office.CustomUI;
using ElecStore.Models;
using ElecStore.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
       private readonly ListOrderViewModel _viewModel;

        public ListOrderWindow(ElectricStore1Context context, User loggedInUser)
        {
            InitializeComponent();
            _viewModel = new ListOrderViewModel(context);
            DataContext = _viewModel;
            _context = context;
            // Liên kết lstOrders với OrdersCollectionView từ ViewModel
            lstOrders.ItemsSource = _viewModel.OrdersCollectionView;
            // Liên kết lstOrders với OrdersCollectionView từ ViewModel
            //Order selectedOrder = (Order)lstOrders.SelectedItem;
            //   _viewModel.SelectedOrder = selectedOrder;
            //lstOrderDetails.ItemsSource = _viewModel.OrderDetails.To;
            // Khởi tạo lstOrderDetails và liên kết với OrderDetails từ ViewModel
        }

        private void lstOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Order selectedOrder = (Order)lstOrders.SelectedItem;
            _viewModel.SelectedOrder = selectedOrder;
           //lstOrderDetails.ItemsSource = _viewModel.OrderDetails;

        }
        private void ButtonDetail_Click(object sender, RoutedEventArgs e)
        {
           

        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // Lọc danh sách đơn hàng dựa trên khoảng thời gian từ startDatePicker đến endDatePicker
            DateTime startDate = startDatePicker.SelectedDate ?? DateTime.Now;
            DateTime endDate = endDatePicker.SelectedDate ?? DateTime.Now;

            // Thực hiện logic lọc dữ liệu dựa trên khoảng thời gian (startDate, endDate)
            // Ví dụ: filterOrdersByDateRange(startDate, endDate);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = startDatePicker.SelectedDate ?? DateTime.MinValue;
            DateTime endDate = endDatePicker.SelectedDate ?? DateTime.MaxValue;

            var filteredOrders = _context.Orders.Where(order =>
          (
              (order.Date.Year > startDate.Year && order.Date.Year < endDate.Year) ||
              (order.Date.Year == startDate.Year && order.Date.Month > startDate.Month) ||
              (order.Date.Year == endDate.Year && order.Date.Month < endDate.Month)
          ) ||
          (
              (order.Date.Year == startDate.Year && order.Date.Month == startDate.Month && order.Date.Day >= startDate.Day) &&
              (order.Date.Year == endDate.Year && order.Date.Month == endDate.Month && order.Date.Day <= endDate.Day)
          )
      );



            lstOrders.ItemsSource = filteredOrders.ToList();
        }

    }
}