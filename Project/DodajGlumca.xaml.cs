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
    /// Interaction logic for DodajGlumca.xaml
    /// </summary>
    public partial class DodajGlumca : Window
    {
        public DodajGlumca()
        {
            InitializeComponent();
        }

        private void Dodaj_Glumca_Click(object sender, RoutedEventArgs e)
        {
            if (UnosIme.Text == "" || UnosPrezime.Text == "" || UnosJMBG.Text == "")
            {
                MessageBox.Show("Morate popuniti sve podatke!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (UnosJMBG.Text.Length != 13)
            {
                MessageBox.Show("JMBG mora imati 13 cifara!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (Char c in UnosJMBG.Text)
            {
                if (c < '0' || c > '9')
                {
                    MessageBox.Show("JMBG mora da sadrzi iskljucivo cifre!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            // Ako smo stigli do ovog dela koda, znaci da je unos validan. Ali potrebno je proveriti da li je JMBG jedinstven medju glumcima
            foreach (Glumac glumac in App.Glumci)
            {
                if (glumac.JMBG.Equals(UnosJMBG.Text))
                {
                    MessageBox.Show("Glumac sa tim JMBG vec postoji!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            App.Glumci.Add(new Glumac(UnosIme.Text, UnosPrezime.Text, UnosJMBG.Text));
            this.Close();
        }
    }
}
