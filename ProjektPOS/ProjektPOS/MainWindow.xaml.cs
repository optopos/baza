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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace ProjektPOS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Zmienne
        /// <summary>
        /// This field connects to data base file or creats new one.
        /// </summary>
        SQLiteConnection _polaczenie = new SQLiteConnection("Data Source=baza.db");

      
        /// <summary>
        /// This list contains Postac class objects.
        /// </summary>
        List<Postac> listaPostaci = new List<Postac>();
        /// <summary>
        /// This field contains selected item from the listBox
        /// </summary>
        Postac wybranaPostac = null;

        #endregion Zmienne

        #region Konstruktory
        /// <summary>
        /// This is the Main Window.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            InicjalizacjaDanych();
        }

        #endregion Konstruktory

        #region Metody

        #region Ogolne
        /// <summary>
        /// This method adds Postac class objects to the listBox.
        /// It uses WczytajDane method.
        /// </summary>
        void InicjalizacjaDanych()
        {
            listaPostaci = Utils.WczytajDane(_polaczenie);
            
            foreach (Postac p in listaPostaci)
            {
                listBox.Items.Add(string.Format("{0} - {1} - {2}", int.Parse(p.Id.ToString()), p.IMIE, p.NAZWISKO));
            }
        }
   

        private void imieBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        #endregion Ogolne
        /// <summary>
        /// Button "Baza"
        /// This method refreshes listBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            listaPostaci.Clear();
            listBox.Items.Clear();
            InicjalizacjaDanych();

        }
        /// <summary>
        /// Button "Aktualizuj"
        /// This method updates selected object from the listBox in the data base.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int IndeksWybranejPostaci = listBox.SelectedIndex;

                if (wybranaPostac != null)
                {
                    if (_polaczenie.State == ConnectionState.Closed)
                    { _polaczenie.Open(); }
                    ////////////////////////////////////////////////////////////////////
                    string _zapytanieSQL = string.Format("");
                    _zapytanieSQL = string.Format( "UPDATE Tabela SET IMIE = '{0}', NAZWISKO= '{2}' WHERE Id = {1}", imieBox.Text, wybranaPostac.Id, nazwiskoBox.Text);
                    SQLiteCommand komenda = new SQLiteCommand(_zapytanieSQL, _polaczenie);
                    komenda.ExecuteNonQuery();
                    MessageBox.Show("Postać zaktualizowana.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);


                    listaPostaci.Clear();
                    listBox.Items.Clear();
                    InicjalizacjaDanych();
                    listBox.SelectedIndex = IndeksWybranejPostaci;
                }
                else
                    MessageBox.Show("Wybierz postać, którą chcesz zaktualizować.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                string byk = string.Format("Problem : \n {0}", ex.Message);
                MessageBox.Show(byk, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
  
        /// <summary>
        /// This method 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (listBox.SelectedIndex == -1)
                {
                    imieBox.Text = "";
                    nazwiskoBox.Text = "";
                    wybranaPostac = null;

                }
                else
                {

                    string cos = listBox.SelectedItem.ToString();
                    string[] tab = cos.Split('-');
                       
                    
                    Postac p = listaPostaci.FirstOrDefault(x => x.IMIE.Equals(tab[1].Trim()));
                    wybranaPostac = p;
                    if (p != null)
                    {
                        imieBox.Text = p.IMIE;
                        nazwiskoBox.Text = p.NAZWISKO;
                    }
                }
            }
            catch (Exception ex)
            {
                string byk = string.Format("Problem podczas wybierania postaci: \n", ex.Message);
                MessageBox.Show(byk, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Button "Dodaj"
        /// This method adds Postac calss object to the data base.
        /// It uses textBoxs: imieBox, nazwiskoBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (imieBox.Text != null || nazwiskoBox.Text != null)
                { 
                    if (_polaczenie.State == ConnectionState.Closed)
                    { _polaczenie.Open(); }
                    string _zapytanieSQL = string.Format("");
                    _zapytanieSQL = string.Format("INSERT INTO Tabela (IMIE, NAZWISKO, Id)"+"VALUES('{0}', '{1}', {2})", imieBox.Text, nazwiskoBox.Text, listaPostaci.MaxId()+1);
                    SQLiteCommand komenda = new SQLiteCommand(_zapytanieSQL, _polaczenie);
                    komenda.ExecuteNonQuery();
                    MessageBox.Show("Postać Dodana.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);


                    listaPostaci.Clear();
                    listBox.Items.Clear();
                    InicjalizacjaDanych();
                    
                }
                else
                    MessageBox.Show("Musisz coś wpisać.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                string byk = string.Format("Problem : \n {0}", ex.Message);
                MessageBox.Show(byk, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Button "Usuń"
        /// This method deletes Postac class object form the data base.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                

                if (wybranaPostac != null)
                {
                    if (_polaczenie.State == ConnectionState.Closed)
                    { _polaczenie.Open(); }

                   
                    ////////////////////////////////////////////////////////////////////
                    string _zapytanieSQL = string.Format("");
                    _zapytanieSQL = string.Format("DELETE FROM Tabela WHERE Id = {0}", wybranaPostac.Id);
                    SQLiteCommand komenda = new SQLiteCommand(_zapytanieSQL, _polaczenie);
                    komenda.ExecuteNonQuery();
                    MessageBox.Show("Postać usunięta.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);


                    listaPostaci.Clear();
                    listBox.Items.Clear();
                    InicjalizacjaDanych();
               
                }
                else
                    MessageBox.Show("Wybierz postać, którą chcesz usunąć.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                string byk = string.Format("Problem : \n {0}", ex.Message);
                MessageBox.Show(byk, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Button "Zamknij"
        /// This method closes the MainWindow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        #endregion Metody

       
    }
}
