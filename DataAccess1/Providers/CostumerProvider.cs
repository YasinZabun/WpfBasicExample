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
                    new ModelClass { Id = reader.GetInt32(0), FirstName = reader.GetString(1), LastName = reader.GetString(2), Gender = reader.GetString(3), Adress = reader.GetString(4) }
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
            string myCommand = "update notTablosu SET Id = @param6,FirstName = @param7,LastName = @param8,Gender=@param9, Adress=@param10 where Id=@param1 or FirstName=@param2 or LastName=@param3 or Gender=@param4 or Adress=@param5 ";
            s = new SQLiteCommand(myCommand, connect);
            s.Parameters.AddWithValue("@param1", eski.Id);
            s.Parameters.AddWithValue("@param2", eski.FirstName);
            s.Parameters.AddWithValue("@param3", eski.LastName);
            s.Parameters.AddWithValue("@param4", eski.Gender);
            s.Parameters.AddWithValue("@param5", eski.Adress);
            s.Parameters.AddWithValue("@param6", yeni.Id);
            s.Parameters.AddWithValue("@param7", yeni.FirstName);
            s.Parameters.AddWithValue("@param8", yeni.LastName);
            s.Parameters.AddWithValue("@param9", yeni.Gender);
            s.Parameters.AddWithValue("@param10", yeni.Adress);
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
            string myCommand = "insert into notTablosu values(@param1,@param2,@param3,@param4,@param5)";
            s = new SQLiteCommand(myCommand, connect);
            s.Parameters.AddWithValue("@param1", ++enbuyuk);
            s.Parameters.AddWithValue("@param2", ornek.FirstName);
            s.Parameters.AddWithValue("@param3", ornek.LastName);
            s.Parameters.AddWithValue("@param4", ornek.Gender);
            s.Parameters.AddWithValue("@param5", ornek.Adress);
            s.ExecuteNonQuery();
            connect.Close();

        }
    }
}
