using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    // Klasa u kojoj grupisemo uloge u zavisnosti od predstave.
    // Potrebno za prvi TreeView u Tab2.
    public class Predstava : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private string naziv;
        private ObservableCollection<Uloga> sveUloge = new ObservableCollection<Uloga>();

        #region KONSTRUKTOR
        public Predstava(string naziv)
        {
            this.naziv = naziv;
        }
        #endregion

        #region PROPERTIES
        public string Naziv
        {
            get { return this.naziv; }
            set
            {
                if (this.naziv != value)
                {
                    this.naziv = value;
                    this.NotifyPropertyChanged("Naziv");
                }
            }
        }
        public ObservableCollection<Uloga> SveUloge
        {
            get { return this.sveUloge; }
            set
            {
                if (this.sveUloge != value)
                {
                    this.sveUloge = value;
                    this.NotifyPropertyChanged("SveUloge");
                }
            }
        }
        #endregion

        public void DodajUlogu(Uloga u)
        {
            sveUloge.Add(u);
        }
    }
}
