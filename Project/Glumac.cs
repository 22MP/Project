using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Glumac : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private string ime;
        private string prezime;
        private string jmbg;
        private ObservableCollection<Uloga> dodeljeneUloge = new ObservableCollection<Uloga>();

        #region KONSTRUKTORI
        public Glumac()
        {
            this.Ime = "";
            this.Prezime = "";
            this.JMBG1 = "";
        }
        public Glumac(string ime, string prezime, string JMBG)
        {
            this.Ime = ime;
            this.Prezime = prezime;
            this.JMBG1 = JMBG;
        }
        #endregion

        #region PROPERTIES
        public string Ime
        {
            get { return this.ime; }
            set
            {
                if (this.ime != value)
                {
                    this.ime = value;
                    this.NotifyPropertyChanged("Ime");
                }
            }
        }

        public string Prezime
        {
            get { return this.prezime; }
            set
            {
                if (this.prezime != value)
                {
                    this.prezime = value;
                    this.NotifyPropertyChanged("Prezime");
                }
            }
        }

        public string JMBG
        {
            get { return this.jmbg; }
            set
            {
                if (this.jmbg != value)
                {
                    this.jmbg = value;
                    this.NotifyPropertyChanged("JMBG");
                }
            }
        }

        public ObservableCollection<Uloga> DodeljeneUloge
        {
            get { return this.dodeljeneUloge; }
            set
            {
                if (this.dodeljeneUloge != value)
                {
                    this.dodeljeneUloge = value;
                    this.NotifyPropertyChanged("DodeljeneUloge");
                }
            }
        }
        #endregion
    }
}
