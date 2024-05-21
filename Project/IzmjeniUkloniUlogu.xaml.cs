using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Project
{
    /// <summary>
    /// Interaction logic for IzmjeniUkloniUlogu.xaml
    /// </summary>
    public partial class IzmjeniUkloniUlogu : Window
    {
        private string putanja = "";
        private bool zatvori;
        public IzmjeniUkloniUlogu()
        {
            InitializeComponent();
            sifra.Text = App.SelektovanaUloga.Sifra.ToString();
            naziv.Text = App.SelektovanaUloga.Naziv;
            predstava.Text = App.SelektovanaUloga.Predstava;
            tipUloge.Text = App.SelektovanaUloga.TipUloge;
            dostupnost.Text = App.SelektovanaUloga.Dostupnost;
        }

        private void ikonica_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                var dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(.*jpg)|*.jpg| PNG files(.*png)|*.png";

                if(dialog.ShowDialog() == true) 
                {
                    putanja = dialog.FileName;
                    slika.Source = new BitmapImage(new Uri(putanja, UriKind.Absolute));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Niste odabrali sliku!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void izmjeniBtn_Click(object sender, RoutedEventArgs e)
        {
            zatvori = true;
            if (putanja != "")
            {
                App.SelektovanaUloga.Ikonica = putanja;
            }
            if (sifra.Text.Equals("") || naziv.Text.Equals("") || predstava.Text.Equals("") || tipUloge.Equals("") || dostupnost.Equals(""))
            {
                MessageBox.Show("Niste popunili sva polja!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                zatvori = false;
            }
            else
            {
                int broj = 0;

                try
                {
                    broj = Int32.Parse(sifra.Text);
                }
                catch
                {
                    MessageBox.Show("Niste uneli broj!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                    zatvori = false;
                }

                foreach (Uloga u in App.Podaci)
                {
                    if (App.SelektovanaUloga != u) 
                    { 
                        if (u.Sifra == broj)
                        {
                            MessageBox.Show("Ova sifra vec postoji!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                            zatvori = false;
                        }
                        
                    }
                }

                App.SelektovanaUloga.Sifra = broj;
                App.SelektovanaUloga.Naziv =naziv.Text;
                App.SelektovanaUloga.Predstava = predstava.Text;
                App.SelektovanaUloga.TipUloge = tipUloge.Text;
                App.SelektovanaUloga.Dostupnost = dostupnost.Text ;

                if (zatvori)
                {
                    this.Close();
                }

            }
        }

        private void ukloniBtn_Click(object sender, RoutedEventArgs e)
        {
            var daNe = MessageBox.Show("Brisanje korisnika je NEPOVRATNA OPERACIJA!\nBrisanjem korisnika brišu se svi podaci o njemu!\n\nDa li ste sigurni?", "Upozorenje o brisanju!", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (daNe == MessageBoxResult.Yes)
            {
                App.Podaci.Remove(App.SelektovanaUloga);
                MessageBox.Show("Korisnik je obrisan iz kolekcije!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                zatvori = true;
            }
            else
            {
                zatvori=false;
            }

            if (zatvori)
            {
                this.Close();
            }

        }
    }
}
