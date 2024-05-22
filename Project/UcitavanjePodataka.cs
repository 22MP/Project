using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace Project
{
    internal class UcitavanjePodataka
    {
        public UcitavanjePodataka()
        {

        }
        public static ObservableCollection<Uloga> Ucitaj_listu(string file)
        {
            ObservableCollection<Uloga> lista = new ObservableCollection<Uloga>();
            Uloga temp = null;
            string linija;
            TextReader tr = null;
            try
            {
                tr = new StreamReader(File.Open(file, FileMode.Open));

                while ((linija = tr.ReadLine()) != null)
                {
                    string[] delovi = linija.Split('|');

                    try
                    {
                        int sifra = int.Parse(delovi[0]);
                        
                        temp = new Uloga(sifra, delovi[1], delovi[2], delovi[3], delovi[4]);
                        lista.Add(temp);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (tr != null)
            {
                tr.Close();
            }
            return lista;
        }


        public static void ExportUloge(string file, ObservableCollection<Uloga> uloge)
        {
            StreamWriter sw = null;

            string linija = null;
            try
            {
                sw = new StreamWriter(file);
                foreach(Uloga u in uloge) { 

                    linija= u.Ikonica+","+u.Sifra+","+u.Naziv+","+u.Predstava+","+u.TipUloge+","+u.Dostupnost+"\n";
                    sw.WriteLine(linija);
                    
                }

                sw.Close();
                MessageBox.Show("Uspesno sacuvani podaci!");
            }
            catch (Exception e)
            {
                MessageBox.Show("Greska prilikom exporta!");
            }
        }

        public static void ExportGlumci(string file, ObservableCollection<Glumac> glumci)
        {
            StreamWriter sw = null;

            string linija = null;
            try
            {
                sw = new StreamWriter(file);
                foreach (Glumac glumac in glumci)
                {
                    linija = glumac.Ime + "," + glumac.Prezime + "," + glumac.JMBG;
                    foreach (Uloga u in glumac.DodeljeneUloge)
                        linija += "," + u.Naziv;

                    sw.WriteLine(linija);
                }

                sw.Close();
                MessageBox.Show("Uspesno sacuvani podaci!");
            }
            catch (Exception e)
            {
                MessageBox.Show("Greska prilikom exporta!");
            }
        }

    }
}
