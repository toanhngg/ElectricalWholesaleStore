using ElecStore.Models;
using ElecStore.ViewModel;
using MaterialDesignThemes.Wpf;
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
using static System.Net.Mime.MediaTypeNames;

namespace ElecStore
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderView : Window
    {
        private readonly ElectricStore1Context _context;
        private readonly User _loggedInUser;
        public OrderView(ElectricStore1Context context, int selectedProductID, User loggedInUser)
        {
            InitializeComponent();
            OrderViewModel viewModel = new OrderViewModel();

            // Gán ViewModel này cho DataContext của cửa sổ
            DataContext = viewModel;
            _context = context;
            _loggedInUser = loggedInUser;
            LoadDataListPromotion(selectedProductID);
        }
        public Commodity cm = new Commodity();

        public void LoadDataListPromotion(int idComppodity)
        {
             cm = _context.Commodities.FirstOrDefault(x => x.CommodityId == idComppodity);
            if (cm != null)
            {
                txtNameProduct.Text = cm.CommodityName;
                txtPrice.Text = cm.UnitPrice.ToString();
                  txtNameProduct.IsReadOnly = true;
                 txtPrice.IsReadOnly = true;
                SomeMethodInCodeBehind();
                cboPromotionID.ItemsSource = _context.Promotions.ToList();
                cbopaymentMethod.Items.Add("Thanh toán khi nhận hàng");
                cbopaymentMethod.Items.Add("Thanh toán khi qua chuyển khoản");
                cbopaymentMethod.SelectedItem = "Thanh toán khi nhận hàng";

            }
        }

        private void SomeMethodInCodeBehind()
        {
            // Lấy ViewModel từ DataContext
            OrderViewModel viewModel = DataContext as OrderViewModel;

            // Kiểm tra null trước khi gán giá trị
            if (viewModel != null)
            {
                // Gán giá trị vào thuộc tính của ViewModel
          //      viewModel.Price = double.Parse(cm.UnitPrice.ToString()); // Ví dụ giá trị cần gán
 
                viewModel.UpdatePrice(double.Parse(cm.UnitPrice.ToString()));

            }
        }

        public static string GenerateRandomInvoiceCode()
        {
            int length = 8;

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            Random random = new Random();

            string invoiceCode = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return invoiceCode;
        }

        private void txtQuantity_LostFocus(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtQuantity.Text, out int enteredQuantity))
            {
                if (enteredQuantity > cm.UnitInStock)
                {
                    MessageBox.Show("Số lượng nhập vào vượt quá số lượng có sẵn trong kho.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtQuantity.Text = cm.UnitInStock.ToString();
                }
                else
                {
                    // Lưu giá trị để sử dụng cho lần kiểm tra tiếp theo
                    txtQuantity.Text = enteredQuantity.ToString();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập số phù hợp.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CustomerName.Text) && string.IsNullOrEmpty(CustomerAddress.Text)
               && string.IsNullOrEmpty(CustomerPhone.Text) && string.IsNullOrEmpty(CustomerNote.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin khách hàng.");
            }
            else
            {
                string commodityName = txtNameProduct.Text; // Ví dụ: lấy giá trị từ ComboBox
                Date date = new Date
                { //  DateId = 105,
                    Day = DateTime.Now.Day,
                    Month = DateTime.Now.Month,
                    Quarter = 5,
                    Year = DateTime.Now.Year
                };
                _context.Dates.Add(date);
                _context.SaveChanges();
                Commodity cm = _context.Commodities.FirstOrDefault(x => x.CommodityName.Equals(commodityName));
                // Nếu khách hàng chưa tồn tại, tạo một bản ghi khách hàng mới
                Customer newCustomer = new Customer
                {
                  //  CustomerId = 107,
                    CustomerName = CustomerName.Text,
                    CustomerPhone = CustomerAddress.Text,
                    CustomerAddress = CustomerPhone.Text,
                    CustomerType = "vip",
                    Discount = 0.1m,
                    TotalBought = Int32.Parse(txtTotalPrice.Text),
                    Comment = CustomerNote.Text,
                    //// Các thuộc tính khác
                    //CustomerType = "kk",
                    //Discount = 0,
                    //TotalBought = "fff",
                    //Comment = "ff"
                };
                _context.Customers.Add(newCustomer);
                _context.SaveChanges(); // Lưu bản ghi khách hàng mới

                // Lấy customerId của khách hàng mới tạo
                int customerId = newCustomer.CustomerId;
                int selectedPaymentMethodIndex = cbopaymentMethod.SelectedIndex;
                string paymentMethod = cbopaymentMethod.Items[selectedPaymentMethodIndex].ToString();
                string invoiceCode = GenerateRandomInvoiceCode();

                Order order = new Order
                {
                   // OrderId = 3,
                    CommodityId = cm.CommodityId,
                    DateId = date.DateId,
                    CustomerId = customerId,
                    StoreId = _loggedInUser.StoreId,
                    PricedProducts = Int32.Parse(txtTotalPrice.Text),
                    PromotionId = cboPromotionID.SelectedIndex,
                    PaymentMethod = paymentMethod,
                    InvoiceNumber = invoiceCode,
                    UserId = _loggedInUser.UserId
                };

                    _context.Orders.Add(order);
                    _context.SaveChanges();
                OrderDetail orderdetail = new OrderDetail
                {
                    // OrderId = 3,
                    OrderId = order.OrderId,
                    UnitPrice = decimal.Parse(cm.UnitPrice.ToString()),
                    Quantity = customerId,
                    TotalPrice = Int32.Parse(txtTotalPrice.Text)

                };
                _context.OrderDetails.Add(orderdetail);
                _context.SaveChanges();
                MessageBox.Show("Đặt hàng thành công!"); // Hiển thị thông báo khi insert thành công
                    HomePage homePage = new HomePage(_context, _loggedInUser);
                    homePage.Show();

                    this.Close(); // Đóng window hiện tại

         
            }
        }

        private void cboPromotionID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as OrderViewModel;
            viewModel?.UpdateTotalPrice();
        }
    }
}
