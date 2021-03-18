using Data.Models;
using DataAccess.Providers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Utils.Base;

namespace wpfornekdetay.viewmodelfolder
{
    class GuncelleViewModel:ViewModelBase
    {
        /// <summary>
        /// ana viewden gelen nesneyi burada new ile alıp .
        /// </summary>
        public ModelClass nesne { get; set; }
        public ModelClass nesne2 { get; set; }
        public ICommand Guncelgonder { get; set; }
        public ICommand OnOpenDialog { get; set; }

        CostumerProvider Provider = CostumerProvider.Instance;

        private string _imagename;
        public string ImageName 
        {
            get
            {
                return _imagename;
            }
            set
            {
                _imagename = value;
                RaisePropertyChanged("ImageName");
            }
        }

        public GuncelleViewModel(ref ModelClass ornek,ModelClass ornek1)
        {

           
            
            //nesne = new ModelClass();
            nesne = ornek;
            //nesne2 = new ModelClass();
            //nesne2 = ornek;
            nesne2 = new ModelClass();
            nesne2.Id = ornek.Id;
            nesne2.FirstName = ornek.FirstName;
            nesne2.LastName = ornek.LastName;
            nesne2.Gender = ornek.Gender;
            nesne2.Adress = ornek.Adress;
            nesne2.Image = ornek.Image;
            MessageBox.Show(nesne.Id.ToString());
            OnOpenDialog=new RelayCommand<object> ((_) => OpenFileDialog());
            Guncelgonder = new RelayCommand<ModelClass>(kaydet);
        }
        public void OpenFileDialog()
        {
            OpenFileDialog filedialog = new OpenFileDialog();
            filedialog.Multiselect = false;
            filedialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (filedialog.ShowDialog() == true)
            {
                byte[] imageArray = System.IO.File.ReadAllBytes(filedialog.FileName);
                string base64Text = Convert.ToBase64String(imageArray);
                ImageName = filedialog.SafeFileName;
                nesne2.Image = base64Text;
            }
        }
        public void kaydet(ModelClass ornek)
        {

            Provider.update(nesne, ornek);
            nesne.Id = ornek.Id;
            nesne.FirstName = ornek.FirstName;
            nesne.LastName = ornek.LastName;
            nesne.Gender = ornek.Gender;
            nesne.Adress = ornek.Adress;
            nesne.Image = ornek.Image;
            nesne2.Id = ornek.Id;
            nesne2.FirstName = ornek.FirstName;
            nesne2.LastName = ornek.LastName;
            nesne2.Gender = ornek.Gender;
            nesne2.Adress = ornek.Adress;
            nesne2.Image = ornek.Image;
            


        }
    }
}
