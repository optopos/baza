using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektPOS
{
    public class Utils
    {

        public static List<Postac> WczytajDane(SQLiteConnection polaczenie)
        {
            List<Postac> listaPostaci = new List<Postac>();
            SQLiteDataReader czytnik=null;
            try
            {
                if (polaczenie.State == ConnectionState.Closed)
                { polaczenie.Open(); }
                var zapytanieSQL = "select * from Tabela";
                var komenda = new SQLiteCommand(zapytanieSQL, polaczenie);
                czytnik = komenda.ExecuteReader();
                //int licznik = 1;


                //listBox.Items.Add(string.Format( "{0} - {1} - {2}", czytnik["Id"].ToString(), czytnik["IMIE"].ToString(), czytnik["NAZWISKO"].ToString()));
                if (czytnik.HasRows)
                {
                    //int licznik = 1;
                    while (czytnik.Read())
                    {
                        listaPostaci.Add(new Postac(int.Parse(czytnik["Id"].ToString()), czytnik["IMIE"].ToString(), czytnik["NAZWISKO"].ToString()));
                        // listBox.Items.Add(string.Format("{0} - {1} - {2}", licznik, czytnik["IMIE"].ToString(), czytnik["NAZWISKO"].ToString()));
                        //licznik++;

                    }
                    czytnik.Close();
                }
            }
            catch (Exception ex)
            {
                //string byk = string.Format("Błąd podczas pobierania danych: \n {0}", ex.Message);
                //MessageBox.Show(byk, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                polaczenie.Close();
                if (czytnik != null)
                {
                    czytnik.Dispose();
                    czytnik = null;
                }
            }

            return listaPostaci;
        }
    }
}
