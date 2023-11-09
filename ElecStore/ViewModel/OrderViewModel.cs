using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ElecStore.ViewModel
{
    class OrderViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                    CalculateTotalPrice();
                }
            }
        }

        private double _price;
        public double Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged(nameof(Price));
                    CalculateTotalPrice();
                }
            }
        }

        private double _totalPrice;
        public double TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                if (_totalPrice != value)
                {
                    _totalPrice = value;
                    OnPropertyChanged(nameof(TotalPrice));
                    CalculateTotalPrice();

                }
            }
        }
        private double _lasttotalPrice;
        public double LastTotalPrice
        {
            get { return _lasttotalPrice; }
            set
            {
                if (_lasttotalPrice != value)
                {
                    _lasttotalPrice = value;
                    OnPropertyChanged(nameof(LastTotalPrice));
                    CalculateTotalPrice();

                }
            }
        }
       
        double discount = 0;
        private void CalculateTotalPrice()
        {
          //  TotalPrice  = Quantity * Price * 1.5;
            if(TotalPrice > 100000000)
            {
                TotalPrice = Quantity * Price * 1.5;
                discount = 0.1; // 10% giảm giá cho mức từ 100 triệu trở lên
                double additionalDiscount = Math.Floor((TotalPrice - 100000000) / 100000000) * 0.02; // Tính giảm giá bổ sung
                discount = Math.Min(discount + additionalDiscount, 0.4); // Tối đa 40% giảm giá
                LastTotalPrice = TotalPrice - (TotalPrice * discount);
            }
            else
            {
                TotalPrice = Quantity * Price * 1.5;
                LastTotalPrice = Quantity * Price * 1.5;

            }

        }

    }
}
