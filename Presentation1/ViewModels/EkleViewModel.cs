using Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Utils.Base;
using Presentation.Views;
using wpfornekdetay.viewmodelfolder;

namespace wpfornekdetay.viewmodelfolder
{
    class EkleViewModel: ViewModelBase
    {
        public ICommand OnSave { get; set; }
        public ModelClass nesne { get; set; }
        public event EventHandler<ModelClass> ekleveri;
        private Window2 w2;
        public EkleViewModel(Window2 w2)
        {
            this.w2 = w2;
            nesne = new ModelClass();
            OnSave = new RelayCommand<object>((_) => kaydet());
        }
        public void kaydet()
        {
            if (nesne.LastName != null && nesne.FirstName != null && nesne.Gender != null && nesne.Adress != null)
            {
                w2.DialogResult = false;
                ekleveri?.Invoke(this, nesne);
            }
            else
            {
                MessageBox.Show("lütfen bütün alanları doldurun");
            }
            
        }

    }
}
