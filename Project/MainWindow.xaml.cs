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
        Point startPoint = new Point();
        public MainWindow()
        {
            InitializeComponent();

            // Tab1
            App.Podaci = UcitavanjePodataka.Ucitaj_listu("Podaci/ulazniPodaciUloge.txt");
            
            this.DataContext = this;
            tab1Uloge.ItemsSource = App.Podaci;

            // Tab2
            App.Predstave = NapraviListuPredstava(App.Podaci);  // Pravimo listu predstava pomocu liste uloga
            App.Glumci = UcitavanjePodataka.UcitajGlumce("Podaci/ulazniPodaciGlumci.txt");

            Tab2_ListaUloga.ItemsSource = App.Predstave;
            Tab2_ListaGlumaca.ItemsSource = App.Glumci;

            // Tab3
            tab3Uloge.ItemsSource = App.Podaci;
            Tab3_ListaGlumaca.ItemsSource = App.Glumci;
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
            UcitavanjePodataka.ExportUloge("Podaci/izlazniPodaciUloge.txt", App.Podaci);
            UcitavanjePodataka.ExportGlumci("Podaci/izlazniPodaciGlumci.txt", App.Glumci);
        }

        private ObservableCollection<Predstava> NapraviListuPredstava(ObservableCollection<Uloga> listaUloga)
        {
            // Recnik nam govori da li se predstava nalazi vec u listu (key), a value predstavlja objekat te predstave
            Dictionary<string, Predstava> recnik = new Dictionary<string, Predstava>();
            ObservableCollection<Predstava> listaPredstava = new ObservableCollection<Predstava>();

            foreach (Uloga u in listaUloga)
            {
                if (!recnik.ContainsKey(u.Predstava))
                {
                    Predstava nova = new Predstava(u.Predstava);    // Pravimo novu predstavu i dodajemo ulogu u njenu listu uloga
                    nova.DodajUlogu(u);
                    listaPredstava.Add(nova);
                    recnik.Add(u.Predstava, nova);
                } else
                {
                    Predstava p = recnik[u.Predstava];
                    p.DodajUlogu(u);
                }
            }

            return listaPredstava;
        }
        private void Tab2_ListaUloga_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void Tab2_ListaUloga_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                Point mousePos = e.GetPosition(null);
                Vector diff = startPoint - mousePos;

                if (e.LeftButton == MouseButtonState.Pressed &&
                    (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
                {
                    TreeView treeView = sender as TreeView;
                    TreeViewItem treeViewItem =
                        FindAncestor<TreeViewItem>((DependencyObject)e.OriginalSource);

                    Uloga uloga = GetItemFromTreeViewItem(treeView, treeViewItem) as Uloga;

                    if (uloga.Dostupnost == "Nedostupna")
                    {
                        MessageBox.Show("Ta uloga nije dostupna!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    DataObject dragData = new DataObject("myFormat", uloga);
                    DragDrop.DoDragDrop(treeViewItem, dragData, DragDropEffects.Move);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private object GetItemFromTreeViewItem(ItemsControl container, TreeViewItem item)
        {
            object data = container.ItemContainerGenerator.ItemFromContainer(item);
            if (data == DependencyProperty.UnsetValue)
            {
                foreach (object subItem in container.Items)
                {
                    ItemsControl parent = container.ItemContainerGenerator.ContainerFromItem(subItem) as ItemsControl;
                    if (parent != null)
                    {
                        data = GetItemFromTreeViewItem(parent, item);
                        if (data != DependencyProperty.UnsetValue)
                        {
                            break;
                        }
                    }
                }
            }
            return data;
        }

        private void Tab2_ListaGlumaca_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void Tab2_ListaGlumaca_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                Uloga uloga = e.Data.GetData("myFormat") as Uloga;

                TreeView treeView = sender as TreeView;
                TreeViewItem treeViewItem =
                    FindAncestor<TreeViewItem>((DependencyObject)e.OriginalSource);

                Glumac glumac = (Glumac)treeView.ItemContainerGenerator.
                     ItemFromContainer(treeViewItem);

                if (glumac != null)
                {
                    uloga.Dostupnost = "Nedostupna";
                    ++(uloga.BrojDodjeljenihUloga);
                    glumac.DodajUlogu(uloga);
                }
            }
        }

        private void Tab2_Dodaj_Click(object sender, RoutedEventArgs e)
        {
            DodajGlumca dodajGlumca = new DodajGlumca();
            dodajGlumca.Show();
        }
    }
}