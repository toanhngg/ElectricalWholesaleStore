using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ElectricalWholesaleStore_2.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand UserCommand { get; set; }


        public ICommand LoadedWindowCommand { get; set; }
        public bool Isloaded = false;

        // xử lý trong ngày
        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                Isloaded = true;
                if (p == null)
                    return;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;

                if (loginVM.IsLogin)
                {
                    p.Show();
                   // LoadTonKhoData();
                }
                else
                {
                    p.Close();
                }
            }
              );
            //UnitCommand = new RelayCommand<object>((p) => { return true; }, (p) => { UnitWindow wd = new UnitWindow(); wd.ShowDialog(); });
            //SuplierCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SuplierWindow wd = new SuplierWindow(); wd.ShowDialog(); });
            //CustomerCommand = new RelayCommand<object>((p) => { return true; }, (p) => { CustomerWindow wd = new CustomerWindow(); wd.ShowDialog(); });
            //ObjectCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ObjectWindow wd = new ObjectWindow(); wd.ShowDialog(); });
            //UserCommand = new RelayCommand<object>((p) => { return true; }, (p) => { UserWindow wd = new UserWindow(); wd.ShowDialog(); });
            //InputCommand = new RelayCommand<object>((p) => { return true; }, (p) => { InputWindow wd = new InputWindow(); wd.ShowDialog(); });
            //OutputCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OutputWindow wd = new OutputWindow(); wd.ShowDialog(); });
        }

    }
}
