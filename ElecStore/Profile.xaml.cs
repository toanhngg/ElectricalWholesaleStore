using ElecStore.Models;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        private readonly ElectricStore1Context _context;
        private readonly User _loggedInUser;

        public Profile(ElectricStore1Context context, User loggedInUser)
        {
            InitializeComponent();
            _context = context;
            _loggedInUser = loggedInUser;
            LoadProfile();

        }
        private void LoadProfile()
        {
            if (_loggedInUser != null)
            {
                User user = _context.Users.SingleOrDefault(x => x.UserId == _loggedInUser.UserId);

                if (user != null)
                {
                    Store store = _context.Stores.SingleOrDefault(x => x.StoreId == user.StoreId);

                    if (store != null)
                    {
                        txtUser.Text = user.UserName.ToString();
                        txtEmal.Text = user.Email?.ToString();
                        txtStore.Text = store.StoreName?.ToString();
                        txtPhone.Text = user.PhoneNumber?.ToString();
                    }
                }
            }
        }



    }
}
