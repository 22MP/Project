using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    
    internal class Uloga : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private string _ikonica;
        private int _sifra;
        private string _naziv;
        private string _predstava;
        private string _tipUloge;
        private string _dostupnost;


        #region KONSTRUKTOR
        public Uloga(int sifra, string naziv, string predstava, string tipUloge, string ikonica)
        {
            _sifra = sifra;
            _naziv = naziv;
            _predstava = predstava;
            _tipUloge = tipUloge;
            _ikonica = ikonica;
            _dostupnost= "Dostupna";
        }

        public Uloga()
        {
            _sifra = 0;
            _naziv = "";
            _predstava = "";
            _tipUloge = "";
            _ikonica = "";
            _dostupnost = "Dostupna";
        }

        #endregion
        #region PROPERTY
        public string Ikonica
        {
            get { return _ikonica; }
            set
            {
                if (value != _ikonica)
                {
                    _ikonica = value;
                    OnPropertyChanged("Ikonica");
                }
            }

        }
        public int Sifra
        {
            get { return _sifra; }
            set {
                if (value != _sifra)
                {
                    _sifra = value;
                    OnPropertyChanged("Sifra");
                }
            }
        }
        public string Naziv
        {
            get { return _naziv; }
            set
            {
                if (value != _naziv)
                {
                    _naziv = value;
                    OnPropertyChanged("Naziv");
                }
            }
        }
        public string Predstava
        {
            get { return _predstava; }
            set
            {
                if (value != _predstava)
                {
                    _predstava = value;
                    OnPropertyChanged("Predstava");
                }
            }
        }
        public string TipUloge
        {
            get { return _tipUloge; }
            set
            {
                if (value != _tipUloge)
                {
                    _tipUloge = value;
                    OnPropertyChanged("TipUloge");
                }
            }
        }
        public string Dostupnost
        {
            get { return _dostupnost; }
            set
            {
                if (value != _dostupnost)
                {
                    _dostupnost = value;
                    OnPropertyChanged("Dostupnost");
                }
            }
        }
        #endregion
    }
}
