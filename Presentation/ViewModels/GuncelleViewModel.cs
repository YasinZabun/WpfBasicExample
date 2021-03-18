using Data.Models;
using DataAccess.Providers;
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

           
            ImageName = "merhaba2";
            //nesne = new ModelClass();
             nesne = ornek;
            //nesne2 = new ModelClass();
            //nesne2 = ornek;
            nesne2 = new ModelClass();
            nesne2.Id = ornek1.Id;
            nesne2.FirstName = ornek.FirstName;
            nesne2.LastName = ornek.LastName;
            nesne2.Gender = ornek.Gender;
            nesne2.Adress = ornek.Adress;
            MessageBox.Show(nesne.Id.ToString());
            Guncelgonder = new RelayCommand<ModelClass>(kaydet);
        }
        public void kaydet(ModelClass ornek)
        {
             ImageName = "selam";
            
             Provider.update(nesne, ornek);
             nesne.Id = ornek.Id;
             nesne.FirstName = ornek.FirstName;
             nesne.LastName = ornek.LastName;
             nesne.Gender = ornek.Gender;
             nesne.Adress = ornek.Adress;
             MessageBox.Show(nesne.Id.ToString());
             MessageBox.Show(ornek.Id.ToString());
             
            
        }
    }
}
