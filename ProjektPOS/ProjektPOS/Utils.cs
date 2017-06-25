using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektPOS
{
    /// <summary>
    /// This is a public class that contains WczytajDane method.
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// This method adds data form the data base to listaPostaci.
        /// </summary>
        /// <param name="polaczenie"></param>
        /// <returns>listaPostaci that has the same data as the data base.</returns>
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
              
                
                if (czytnik.HasRows)
                {
                    
                    while (czytnik.Read())
                    {
                        listaPostaci.Add(new Postac(int.Parse(czytnik["Id"].ToString()), czytnik["IMIE"].ToString(), czytnik["NAZWISKO"].ToString()));
                        
                    }
                    czytnik.Close();
                }
            }
            //catch (Exception ex)
           // {
                //string byk = string.Format("Błąd podczas pobierania danych: \n {0}", ex.Message);
                //MessageBox.Show(byk, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
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
