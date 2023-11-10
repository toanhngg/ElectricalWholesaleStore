using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Wordprocessing;
using ElecStore.Models;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Linq;

namespace ElecStore
{
    /// <summary>
    /// Interaction logic for InvoiceView.xaml
    /// </summary>
    public partial class InvoiceView : Window
    {
        private readonly ElectricStore1Context _context;
        private readonly User _loggedInUser;
        // private readonly ListOrderViewModel _viewModel;
        public InvoiceView(ElectricStore1Context context, User loggedInUser, double Total)
        {
            InitializeComponent();
            _context = context;
            _loggedInUser = loggedInUser;
            //  txtQuantity.Text = Total.ToString();
            txtTotal.Text = Total.ToString();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == true)
            {
                pd.PrintVisual(this, "Print Window");
            }
        }
    }
}
