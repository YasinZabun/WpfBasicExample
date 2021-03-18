using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Utils.Base;

namespace wpfornekdetay.viewmodelfolder
{
    class SilViewModel:ViewModelBase
    {
        public event EventHandler<int> silveri;
        
        public ICommand Silgonder { get; set; }
        public int SilinecekId { get; set; }
        public SilViewModel()
        {
            Silgonder = new RelayCommand<object>((_) => kaydet());
        }
        public void kaydet()
        {
            if (SilinecekId != 0)
            {
                //w3.DialogResult = false;
                silveri?.Invoke(this, SilinecekId);
            }
            else
            {
                MessageBox.Show("lütfen bütün alanları doldurun");
            }

        }
    }
}
