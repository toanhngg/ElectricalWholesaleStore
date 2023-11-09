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
        private readonly ElectricStoreContext _context;
        public OrderView(ElectricStoreContext context, int selectedProductID)
        {
            InitializeComponent();
            OrderViewModel viewModel = new OrderViewModel();

            // Gán ViewModel này cho DataContext của cửa sổ
            DataContext = viewModel;
            _context = context;
            LoadDataListPromotion(selectedProductID);
        }
        public void LoadDataListPromotion(int idComppodity)
        {
            Commodity cm = _context.Commodities.FirstOrDefault(x => x.CommodityId == idComppodity);
            if (cm != null)
            {
                txtNameProduct.Text = cm.CommodityName;
                cboPromotionID.ItemsSource = _context.Promotions.ToList();
                cbopaymentMethod.Items.Add("Thanh toán khi nhận hàng");
                cbopaymentMethod.Items.Add("Thanh toán khi qua chuyển khoản");
                cbopaymentMethod.SelectedItem = "Thanh toán khi nhận hàng";

            }
        }
        public static string GenerateRandomInvoiceCode()
        {
            // Độ dài của mã hóa đơn bạn muốn tạo
            int length = 8;

            // Dùng các ký tự số và chữ cái (hoa hoặc thường) để tạo mã hóa đơn ngẫu nhiên
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            // Sử dụng đối tượng Random để tạo mã ngẫu nhiên
            Random random = new Random();

            // Tạo mã hóa đơn bằng cách chọn ngẫu nhiên các ký tự từ chuỗi `chars`
            string invoiceCode = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return invoiceCode;
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
                {   DateId = 105,
                    Day = DateTime.Now.Day,
                    Month = DateTime.Now.Month,
                    Year = DateTime.Now.Year
                };
                _context.Dates.Add(date);
                _context.SaveChanges();
                Commodity cm = _context.Commodities.FirstOrDefault(x => x.CommodityName.Equals(commodityName));
                // Nếu khách hàng chưa tồn tại, tạo một bản ghi khách hàng mới
                Customer newCustomer = new Customer
                {
                    CustomerId = 107,
                    CustomerName = "Tên khách hàng",
                    CustomerPhone = "Số điện thoại",
                    CustomerAddress = "Địa chỉ",
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
                //var user = _context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);

                //if (user != null)
                //{
                //    int userId = user.Id; // Lấy userId của người dùng
                //}
                // Tạo một đối tượng Order và thiết lập các thuộc tính
                Order order = new Order
                {
                    OrderId = 3,
                    CommodityId = cm.CommodityId,
                    DateId = date.DateId,
                    CustomerId = customerId,
                    StoreId = 1,
                    PricedProducts = Int32.Parse(txtQuantity.Text) * Int32.Parse(txtPrice.Text),
                    PromotionId = cboPromotionID.SelectedIndex,
                    PaymentMethod = paymentMethod,
                    InvoiceNumber = invoiceCode,
                    UserId = 1
                };

                    _context.Orders.Add(order);
                    _context.SaveChanges();
              
                    MessageBox.Show("Insert thành công!"); // Hiển thị thông báo khi insert thành công
                    HomePage homePage = new HomePage(_context);
                    homePage.Show();

                    this.Close(); // Đóng window hiện tại

         
            }
        }
    }
}
