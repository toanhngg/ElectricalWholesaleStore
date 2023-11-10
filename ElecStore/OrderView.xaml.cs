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
            OrderViewModel viewModel = DataContext as OrderViewModel;

            // Kiểm tra null trước khi gán giá trị
            if (viewModel != null)
            {
 
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
                Commodity cm = _context.Commodities.FirstOrDefault(x => x.CommodityName.Equals(commodityName));


                if (cm != null)
                {
                    Date date = new Date
                    { //  DateId = 105,
                        Day = DateTime.Now.Day,
                        Month = DateTime.Now.Month,
                        Quarter = 5,
                        Year = DateTime.Now.Year
                    };
                    _context.Dates.Add(date);
                    _context.SaveChanges();
                    Customer newCustomer = new Customer
                    {
                        CustomerName = CustomerName.Text,
                        CustomerPhone = CustomerAddress.Text,
                        CustomerAddress = CustomerPhone.Text,
                        CustomerType = "vip",
                        Discount = 0.1m,
                        TotalBought = Int32.Parse(txtTotalPrice.Text),
                        Comment = CustomerNote.Text,
                    };
                    _context.Customers.Add(newCustomer);
                    _context.SaveChanges(); // Lưu bản ghi khách hàng mới

                    int customerId = newCustomer.CustomerId;
                    int quatity = Int32.Parse(txtQuantity.Text.ToString());
                    int selectedPaymentMethodIndex = cbopaymentMethod.SelectedIndex;
                    string paymentMethod = cbopaymentMethod.Items[selectedPaymentMethodIndex].ToString();
                    string invoiceCode = GenerateRandomInvoiceCode();

                    Order order = new Order
                    {
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
                        OrderId = order.OrderId,
                        UnitPrice = decimal.Parse(cm.UnitPrice.ToString()),
                        Quantity = quatity,
                        TotalPrice = Int32.Parse(txtTotalPrice.Text)
                    };
                    _context.OrderDetails.Add(orderdetail);
                    _context.SaveChanges();
                    if (cm.UnitInStock >= quatity)
                    {
                        cm.UnitInStock -= quatity;
                        _context.Commodities.Update(cm);
                        _context.SaveChanges();
                        MessageBox.Show("Đặt hàng thành công!"); // Hiển thị thông báo khi insert thành công

                        if (cm.UnitInStock < 100)
                        {
                            MessageBox.Show("Sản phẩm vừa rồi trogn kho chỉ còn dưới 100!"); // Hiển thị thông báo khi insert thành công
                        }
                    }
                    else
                    {
                        MessageBox.Show("Số lượng tồn kho không đủ để thực hiện đặt hàng.");
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sản phẩm trong cơ sở dữ liệu.");
                }
           
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
