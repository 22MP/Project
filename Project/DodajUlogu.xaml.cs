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

namespace Project
{
    /// <summary>
    /// Interaction logic for DodajUlogu.xaml
    /// </summary>
    public partial class DodajUlogu : Window
    {

        private string putanja = "";
        private bool zatvori;
        private Uloga u;
        public DodajUlogu()
        {
            InitializeComponent();
            u = new Uloga();
        }

        private void dodajBtn_Click(object sender, RoutedEventArgs e)
        {
            zatvori = true;
            if (sifra.Text.Equals("") || naziv.Text.Equals("") || predstava.Text.Equals("") || tipUloge.Equals("") || putanja.Equals(""))
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

                u.Sifra = broj;
                u.Naziv = naziv.Text;
                u.Predstava = predstava.Text;
                u.TipUloge = tipUloge.Text;
                u.Ikonica = putanja;

               


                if (zatvori)
                {
                    App.Podaci.Add(u);
                    this.Close();
                }

            }
        }
        private void ikonica_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(.*jpg)|*.jpg| PNG files(.*png)|*.png";

                if (dialog.ShowDialog() == true)
                {
                    putanja = dialog.FileName;
                    slika.Source = slika.Source = new BitmapImage(new Uri(putanja, UriKind.Absolute));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Niste odabrali sliku!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
