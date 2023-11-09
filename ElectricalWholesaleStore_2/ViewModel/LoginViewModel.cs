using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
//using ElectricalWholesaleStore_2.Models;



namespace ElectricalWholesaleStore_2.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
     //  // private readonly ElectricStoreContext _context;
     ////   public LoginViewModel(ElectricStoreContext context)
     //   {
     //       _context = context;
     //   }
        public bool IsLogin { get; set; }
        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        public ICommand CloseCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        // mọi thứ xử lý sẽ nằm trong này
        public LoginViewModel()
        {
            IsLogin = false;
            Password = "";
            UserName = "";
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
        }

        void Login(Window p)
        {
            if (p == null)
                return;
     //   var accCount = _context.Users.Where(x => x.UserName == UserName && x.Password == Password).Count();

        //    if (accCount > 0)
        //    {
        //        IsLogin = true;
        //        MessageBox.Show("danh nhap thanh cong!");

        //        p.Close();
        //    }
        //    else
        //    {
        //        IsLogin = false;
        //        MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
        //    }
       }

   }
    }
