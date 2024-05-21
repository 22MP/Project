using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
