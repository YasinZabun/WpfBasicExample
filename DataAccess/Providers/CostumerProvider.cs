using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.Collections.ObjectModel;
using Data.Models;
using System.Windows;

namespace DataAccess.Providers
{
    public class CostumerProvider
    {
        private static CostumerProvider instance = null;
        private string cString = @"Data Source=C:\Users\atillapcw\Desktop\notlarDb.db;Version=3;";
        private SQLiteConnection connect;
        private SQLiteCommand s;
        private CostumerProvider() { }
        public static CostumerProvider Instance
        {
            get
            {
                if (instance == null)
                    instance = new CostumerProvider();
                return instance;
            }
        }

        public ObservableCollection<ModelClass>GetAll()
        {
            ObservableCollection<ModelClass> Costumers = new ObservableCollection<ModelClass> { };
            connect = new SQLiteConnection(cString);
            DataTable dt = new DataTable();
            connect.Open();
            string myCommand = "select * from notTablosu";
            s = new SQLiteCommand(myCommand, connect);
            SQLiteDataReader reader = s.ExecuteReader();
            while (reader.Read())
            {
                Costumers.Add(
                    new ModelClass { Id = reader.GetInt32(0), FirstName = reader.GetString(1), LastName = reader.GetString(2), Gender = reader.GetString(3), Adress = reader.GetString(4), Image=reader.GetString(5) }
                );

            }
            reader.Close();
            connect.Close();
            return Costumers;
        }
        public void update(ModelClass eski,ModelClass yeni)
        {
            ///<summary>
            /// eski ve yeni almak için güncelle view modelde bir event oluşturup güncelleme işlemi yapıldıktan sonra bir event
            /// ile viewmodelclassda o eventa abona methot içinde gelen parametre modelclass eski ve modelclass yeni yi 
            /// buraya eski ve yeni parametreleri olarak alacağım. ardından update edeceğim. eskide olanları yenide değiştireceğim.
            ///</summary>

            connect = new SQLiteConnection(cString);
            connect.Open();
            string myCommand = "update notTablosu SET Id = @param7,FirstName =@param8,LastName=@param9,Gender=@param10,Adress=@param11,Image=@param12 where Id=@param1";
            //string myCommand = "update notTablosu SET Image=@param12 where Id=@param1";

            s = new SQLiteCommand(myCommand, connect);
            s.Parameters.AddWithValue("@param1", eski.Id);
            s.Parameters.AddWithValue("@param2", eski.FirstName);
            s.Parameters.AddWithValue("@param3", eski.LastName);
            s.Parameters.AddWithValue("@param4", eski.Gender);
            s.Parameters.AddWithValue("@param5", eski.Adress);
            s.Parameters.AddWithValue("@param6", eski.Image);
            s.Parameters.AddWithValue("@param7", yeni.Id);
            s.Parameters.AddWithValue("@param8", yeni.FirstName);
            s.Parameters.AddWithValue("@param9", yeni.LastName);
            s.Parameters.AddWithValue("@param10", yeni.Gender);
            s.Parameters.AddWithValue("@param11", yeni.Adress);
            s.Parameters.AddWithValue("@param12",yeni.Image);
            s.ExecuteNonQuery();
            connect.Close();




        }
        public void delete(int id)
        {
            connect = new SQLiteConnection(cString);
            connect.Open();
            string myCommand = "delete from notTablosu where Id=@param1";
            s = new SQLiteCommand(myCommand, connect);
            s.Parameters.AddWithValue("@param1", id);
            s.ExecuteNonQuery();
            connect.Close();
            

        }
        public void add(ModelClass ornek)
        {
            ObservableCollection<ModelClass> liste = new ObservableCollection<ModelClass>();
            liste = GetAll();
            int enbuyuk = 0;
            for (int i = 0; i < liste.Count; i++)
            {
                if (liste[i].Id > enbuyuk) enbuyuk = liste[i].Id;
            }
            connect = new SQLiteConnection(cString);
            connect.Open();
            string myCommand = "insert into notTablosu values(@param1,@param2,@param3,@param4,@param5,@param6)";
            s = new SQLiteCommand(myCommand, connect);
            s.Parameters.AddWithValue("@param1", ++enbuyuk);
            s.Parameters.AddWithValue("@param2", ornek.FirstName);
            s.Parameters.AddWithValue("@param3", ornek.LastName);
            s.Parameters.AddWithValue("@param4", ornek.Gender);
            s.Parameters.AddWithValue("@param5", ornek.Adress);
            s.Parameters.AddWithValue("@param6", ornek.Image);
            s.ExecuteNonQuery();
            connect.Close();

        }
    }
}
