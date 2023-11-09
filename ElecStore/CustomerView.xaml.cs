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
using System.Windows.Shapes;

namespace ElecStore
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : Window
    {
        private readonly ElectricStore1Context _context;
        public CustomerView(ElectricStore1Context context)
        {
            InitializeComponent();
            this._context = context;
            loadWindow();
        }
        public void loadWindow()
        {
            listViewProduct.ItemsSource = _context.Customers.ToList();
        }
        private void ListViewProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewProduct.SelectedItem != null)
            {
                Models.Customer selectedCustomer = (Models.Customer)listViewProduct.SelectedItem;

            }
        }
    }
}
