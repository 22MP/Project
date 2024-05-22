using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UcitavanjePodataka pd = new UcitavanjePodataka();
        public MainWindow()
        {
            InitializeComponent();

            // Tab1
            App.Podaci = UcitavanjePodataka.Ucitaj_listu("UlazniPodaci/podaci.txt");
            
            this.DataContext = this;
            tab1Uloge.ItemsSource = App.Podaci;

            // Tab2
            Tab2_ListaUloga.ItemsSource = App.Podaci;

            // Tab3
            tab3Uloge.ItemsSource = App.Podaci;
        }

        private void tab1Pretraga_TextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<Uloga> rezultati = new ObservableCollection<Uloga>();

            string unos = (sender as TextBox).Text;
            unos = Regex.Replace(unos, @"\s+", " ");

            foreach (Uloga u in App.Podaci)
            {
                string tekst = u.Naziv.ToLower() + " " + u.Predstava.ToLower() + " " + u.Dostupnost.ToLower();

                if (tekst.Contains(unos.ToLower()))
                {
                    rezultati.Add(u);
                }
            }

            tab1Uloge.ItemsSource = rezultati;

        }

        private void tab1Uloge_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            App.SelektovanaUloga = tab1Uloge.SelectedItem as Uloga;
            IzmjeniUkloniUlogu izmjeniUkloniUlogu = new IzmjeniUkloniUlogu();
            izmjeniUkloniUlogu.Show();

        }

        private void tab1Dodaj_Click(object sender, RoutedEventArgs e)
        {
            DodajUlogu dodajUlogu = new DodajUlogu();
            dodajUlogu.Show();
        }

        private void tab3Export_Click(object sender, RoutedEventArgs e)
        {
            UcitavanjePodataka.Export("UlazniPodaci/export.txt", App.Podaci);
        }
    }
}