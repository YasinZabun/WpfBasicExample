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
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace wpfornekdetay.viewmodelfolder
{
    class EkleViewModel : ViewModelBase
    {
        public ICommand OnSave { get; set; }
        public ICommand OnOpenFile { get; set; }
        public ModelClass nesne { get; set; }
        public event EventHandler<ModelClass> ekleveri;
        private Window2 w2;
        public EkleViewModel(Window2 w2)
        {
            this.w2 = w2;
            nesne = new ModelClass();
            OnSave = new RelayCommand<object>((_) => kaydet());
            OnOpenFile = new RelayCommand<object>((_) => openfiledialog());
        }

        ///<summary>
        ///resmi bulup o resmin base64 halini alıp buradaki nesne örneğinin Image kısmını 40.satırdaki gibi ekleveri?.Invoke kısmında nesne ile birlikte yani nesne.Image ile birlikte göndereceğim.
        /// </summary>
        public void openfiledialog()
        {
            OpenFileDialog filedialog = new OpenFileDialog();
            filedialog.Multiselect = false;
            filedialog.Filter= "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (filedialog.ShowDialog() == true)
            {
                byte[] imageArray = System.IO.File.ReadAllBytes(filedialog.FileName);
                string base64Text = Convert.ToBase64String(imageArray);
                nesne.Image = base64Text;
            }
            
        }

        public void kaydet()
        {
            if (nesne.LastName != null && nesne.FirstName != null && nesne.Gender != null && nesne.Adress != null && nesne.Image != null)
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
