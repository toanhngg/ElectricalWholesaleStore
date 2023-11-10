using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using ElecStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ElecStore.ViewModel
{
    public class ListOrderViewModel : INotifyPropertyChanged
    {
        private readonly ElectricStore1Context _context;
        private Order _selectedOrder;
        private ObservableCollection<OrderDetail> _orderDetails;
        private ICollectionView _ordersCollectionView;
        private ICollectionView _ordersdetailCollectionView;

        public Order SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                if (_selectedOrder != value)
                {
                    _selectedOrder = value;
                    OnPropertyChanged(nameof(SelectedOrder));
                    LoadOrderDetails();
                }
            }
        }

        public ObservableCollection<OrderDetail> OrderDetails
        {
            get { return _orderDetails; }
            set
            {
                if (_orderDetails != value)
                {
                    _orderDetails = value;
                    OnPropertyChanged(nameof(OrderDetails));

                }
            }
        }

        public ICollectionView OrdersCollectionView
        {
            get { return _ordersCollectionView; }
            set
            {
                if (_ordersCollectionView != value)
                {
                    _ordersCollectionView = value;
                    OnPropertyChanged(nameof(OrdersCollectionView));
                }
            }
        }

        public ListOrderViewModel(ElectricStore1Context context)
        {
            _context = context;
            LoadOrder();
        }

        public void LoadOrder()
        {
            var orders = _context.Orders.Include(x => x.OrderDetails).Include(x => x.Store)
                .Include(x => x.Date).Include(x => x.Commodity).Include(x => x.Customer).ToList();
            OrdersCollectionView = CollectionViewSource.GetDefaultView(orders);
            OrdersCollectionView.CurrentChanged += OrdersCollectionView_CurrentChanged;

            // Chọn đơn hàng đầu tiên nếu danh sách không rỗng
            if (orders.Any())
            {
                OrdersCollectionView.MoveCurrentTo(orders.First());
            }
        }

        private void OrdersCollectionView_CurrentChanged(object sender, EventArgs e)
        {
            // Khi đơn hàng hiện tại thay đổi, cập nhật SelectedOrder và tải chi tiết đơn hàng
            SelectedOrder = OrdersCollectionView.CurrentItem as Order;
       
        
        }

        private void LoadOrderDetails()
        {
            if (SelectedOrder != null)
            {
                // Assuming OrderDetails is a navigation property in Order class
                OrderDetails = new ObservableCollection<OrderDetail>(_context.OrderDetails.Where(od => od.OrderId == SelectedOrder.OrderId));

            }
            else
            {
                OrderDetails = new ObservableCollection<OrderDetail>();
            }

            // Kiểm tra OrderDetails trước khi gán
            if (OrderDetails != null)
            {
                // In ra để kiểm tra
                foreach (var detail in OrderDetails)
                {
                    Debug.WriteLine($"OrderDetail: {detail.OrderId}, {detail.UnitPrice}, {detail.Quantity}, {detail.TotalPrice}");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}