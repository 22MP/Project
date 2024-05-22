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
        internal static Uloga SelektovanaUloga { get => selektovanaUloga; set => selektovanaUloga = value; }
        internal static ObservableCollection<Uloga> Podaci { get => podaci; set => podaci = value; }
       
    }


}
