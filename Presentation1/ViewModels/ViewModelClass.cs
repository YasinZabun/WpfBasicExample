using Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Utils.Base;
using Presentation.Views;
using wpfornekdetay.viewmodelfolder;
using DataAccess.Providers;

namespace Presentation.ViewModels
{
    class ViewModelClass : ViewModelBase
    {
        public ObservableCollection<ModelClass> Costumers { get; set; }
        public ICommand OnAddCommand { get; set; }
        public ICommand OnDeleteCommand { get; set; }
        public ICommand OnUpdateCommand { get; set; }
        public ICommand OnExitCommand { get; set; }

        public ICommand OnDelete2Command { get; set; }

        public ICommand OnSave { get; set; }
        public ICommand OnDeleteRow { get; set; }

        public ModelClass forselectmodel;
        CostumerProvider Provider = CostumerProvider.Instance;

        public ViewModelClass()
        {
            Load();
            forselectmodel = new ModelClass();
            //OnAddCommand = new RelayCommand<>(ekle);
            OnAddCommand = new RelayCommand<object>((_) => Ekle());
            OnUpdateCommand = new RelayCommand<ModelClass>(Guncelle);
            OnDeleteCommand = new RelayCommand<ModelClass>(Sil);
            OnDelete2Command = new RelayCommand<object>((_) => Sil2());
            OnDeleteRow = new RelayCommand<ModelClass>(SatirSil);

        }

        public void SatirSil(ModelClass ornek)
        {
            MessageBox.Show("merhaba");
            Provider.delete(ornek.Id);
            Costumers.Remove(ornek);
        }
        public void Sil2()
        {
            Window3 w = new Window3();
            ((SilViewModel)w.DataContext).silveri += ViewModelClass_silveri;
            w.Show();
        }
        public void ViewModelClass_silveri(object sender, int e)
        {
            MessageBox.Show(e.ToString());
            for (int i = 0; i < Costumers.Count; i++)
            {
                if (Costumers[i].Id == e)
                {
                    Costumers.Remove(Costumers[i]);
                    break;
                }
            }

        }
        
        public void ViewModelClass_ekleveri(object sender, ModelClass e)
        {
            if (sender != null)
            {
                Provider.add(e);
                //ModelClass a = (ModelClass)sender;
                int a = Costumers[0].Id;
                for (int i = 0; i < Costumers.Count; i++)
                {
                    if (a < Costumers[i].Id) a = Costumers[i].Id;
                }
                Costumers.Add(new ModelClass { Id = ++a, FirstName = e.FirstName, LastName = e.LastName, Gender = e.Gender, Adress = e.Adress });
            }
            else MessageBox.Show("bos bos ");
        }
        private ModelClass selectedCostumers;
        /// <summary>
        /// secilen itemın bilgilerini alır.
        /// </summary>
        public ModelClass SelectedCostumers
        {
            get
            {
                return selectedCostumers;
            }
            set
            {
                selectedCostumers = value;
            }
        }
        public void Load()
        {
            #region getall-elle
            //Costumers = new ObservableCollection<ModelClass> { };
            //string cString = @"Data Source=C:\Users\atillapcw\Desktop\notlarDb.db;Version=3;";
            //SQLiteConnection connect = new SQLiteConnection(cString);
            //DataTable dt = new DataTable();
            //connect.Open();
            //string mycommand = "select * from notTablosu";
            //SQLiteCommand s = new SQLiteCommand(mycommand, connect);
            //SQLiteDataReader reader = s.ExecuteReader();
            //while (reader.Read())
            //{
            //    Costumers.Add(
            //        new ModelClass { Id = reader.GetInt32(0), FirstName = reader.GetString(1), LastName = reader.GetString(2), Gender = reader.GetString(3), Adress = reader.GetString(4) }
            //    );

            //}
            //reader.Close();
            //connect.Close();

            #endregion
            Costumers = Provider.GetAll();

        }

        public void Ekle()
        {

            Window2 w = new Window2();
            ((EkleViewModel)w.DataContext).ekleveri += ViewModelClass_ekleveri;
            /*yukarıdaki kod için açıklama:
                popupdaki eventa nasıl erişebilirim ? bunun için o eventı bulmam lazım fakat buna erişemeyebilirim.
                bunun için casting yapıyorum datacontexti casting ile birlikte erişebiliyorum. */

            //this.NavigationService.Navigate(w);
            /*parametre olarak view gönderip (ilaveten) yapılan işlemlerden sonra view içine 
             listbox item ekleyip yani yeni bir kayıt yeni bir satır ardından eventın yapacağı işin
            olduğu procedure de yani ekle() gibi bir procedure de yeni bir pencere oluşturup hangi türden bir 
            pencere ?? oda ilk ana viewdeki gibi yani parametre olarak gönderdiğim view türünde yani aynısı 
            olan bir pencere oluşturup new demek yerine gelen parametre viewe eşit olmasını sağlayabilirim. 
            !!!bu datacontexti bağlarken bana problem olabilir neden?? cünkü observablecollection benim
            ilgili alanlarıma bağlı. ben data context olarak ilgili viewmodel i  tekrar bağlarsam aldığım verilerde
            kayıp olmaz mı??? bunun yerine yeni oluşturduğum pencere viewi parametre gelen viewin aynısı yaparken
            ekstradan contextini farklı bir viewmodel içinde kullanablirsem bu sorun ortadan kalkar.*/

            /*viewmodelimde constructur bir obje alıyor. bu obje ilk view için boş geliyor. daha sonra 
             (ekleme sayfasındaki datacontext ile ana viewdeki datacontex yani viewmodel aynı class) ekleme 
            view ekrana gelincede boş geliyor. twoway şeklinde ilgili kısımlara bağlanıyor. değişikliği aynı anda alıyor
            bu değişiklikleri ben constructur da ki nesnenin sırtına yüklüyorum. ardından tekrar anaview oluşturulurken
            bu sefer constructorda obje olarak 2. pencere viewdeki objeyi parametre olarak gönderiyorum. sayfa yüklenirken
            viewmodelde bunun kontrolunu yapıyorum. eğer gelen parametre obje (constructor dan gelen) boş ise 
            birşey yapma fakat dolu ise observablecollection içine yeni bir obje ekle oda parametre gelen obje olsun.*/

            /*geriye dönüş mümkün ise gelen değer yani obje burada yani bu ekle methodunda customers add ile 
             obje prop ile birlikte eklenir. geri dönüşü bulamadım. geri dönüş nasıl olacak??*/


            w.ShowDialog();
            //if (w.DialogResult == true) w.Close();
        }
        public void Sil(ModelClass ornek)
        {
            if (ornek != null)
            {
                Provider.delete(ornek.Id);
                Costumers.Remove(ornek);
                forselectmodel = ornek;
                RaisePropertyChanged("forselectmodel");
            }
        }
        public void Guncelle(ModelClass ornek)
        {
            Window4 w = new Window4();
            w.DataContext = new GuncelleViewModel(ref ornek, ornek);
            w.Show();

        }
    }
}
