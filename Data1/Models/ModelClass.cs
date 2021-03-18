using System;
using System.Collections.Generic;
using System.Text;
using Utils.Base;

namespace Data.Models
{
    public class ModelClass : ObservableBase
    {
        private int _id;
        private string _firstname;
        private string _lastname;
        private string _gender;
        private string _adress;


        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                RaisePropertyChanged("Id");
            }
        }

        public string FirstName
        {
            get
            {
                return _firstname;
            }
            set
            {
                _firstname = value;
                RaisePropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get
            {
                return _lastname;
            }
            set
            {
                _lastname = value;
                RaisePropertyChanged("LastName");
            }
        }
        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
                RaisePropertyChanged("Gender");
            }
        }
        public string Adress
        {
            get
            {
                return _adress;
            }
            set
            {
                _adress = value;
                RaisePropertyChanged("Adress");
            }
        }
    }
}
