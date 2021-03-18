using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;


namespace Utils.Base
{
    public class ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
           
        }
    }
}
