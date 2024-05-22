using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static Uloga selektovanaUloga = null;

        static ObservableCollection<Uloga> podaci;
        static ObservableCollection<Predstava> predstave;
        static ObservableCollection<Glumac> glumci;
        public static Uloga SelektovanaUloga { get => selektovanaUloga; set => selektovanaUloga = value; }
        public static ObservableCollection<Uloga> Podaci { get => podaci; set => podaci = value; }
        public static ObservableCollection<Predstava> Predstave { get => predstave; set => predstave = value; }
        public static ObservableCollection<Glumac> Glumci { get => glumci; set => glumci = value; }

    }


}
