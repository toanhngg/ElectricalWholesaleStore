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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ElecStore.Models.ElectricStore1Context _context;


        public MainWindow(ElectricStore1Context context)
        {
            InitializeComponent();
            _context = context;
        }
        public User getInfor()
        {
            User us = null;
            try
            {

                us = new User
                {
                    UserName = txtUserName.Text,
                    Password = txtPassword.Password,
                };

            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot get Employee", "Get Employee");
            }
            return us;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var u = getInfor();
                if (u != null)
                {
            var user = _context.Users.SingleOrDefault(x => x.UserName.Equals(u.UserName) && x.Password.Equals(u.Password));

                    if(user != null)
                    {
                        HomePage homePage = new HomePage(_context, user);
                        homePage.Show();

                        this.Close(); // Đóng window hiện tại

                    }
                    else
                    {
                        MessageBox.Show( "Username or pass not corect","Login");

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fail login");
            }
        }
    }
}
