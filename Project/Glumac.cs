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
        private Uloga omiljenaUloga;

        #region KONSTRUKTORI
        public Glumac()
        {
            this.Ime = "";
            this.Prezime = "";
            this.JMBG = "";
            this.omiljenaUloga = null;
        }
        public Glumac(string ime, string prezime, string JMBG)
        {
            this.Ime = ime;
            this.Prezime = prezime;
            this.JMBG = JMBG;
            this.omiljenaUloga = null;
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

        public Uloga OmiljenaUloga
        {
            get { return this.omiljenaUloga; }
            set
            {
                if (this.omiljenaUloga != value)
                {
                    this.omiljenaUloga = value;
                    this.NotifyPropertyChanged("OmiljenaUloga");
                }
            }
        }
        #endregion

        public void DodajUlogu(Uloga nova)
        {
            DodeljeneUloge.Add(nova);
            if (OmiljenaUloga == null)   // Ako je u pitanju prva uloga glumcu -> to mu je omiljena
                OmiljenaUloga = nova;
            else
            {
                int brojOmiljeneUloge = 0;
                int brojNoveUloge= 0;

                foreach (Uloga u in DodeljeneUloge)     // Brojimo koliko puta je glumac dobio omiljenu ulogu, a koliko ovu novu koju smo dodali
                {
                    if (u.Naziv == OmiljenaUloga.Naziv)
                        ++brojOmiljeneUloge;
                    else if (u.Naziv == nova.Naziv)
                        ++brojNoveUloge;
                }

                if (brojNoveUloge > brojOmiljeneUloge)
                    OmiljenaUloga = nova;
            }
        }
    }
}
