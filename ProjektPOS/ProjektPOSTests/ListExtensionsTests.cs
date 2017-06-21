using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjektPOS;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;


namespace ProjektPOS.Tests
{
    [TestClass()]
    public class ListExtensionsTests
    {
        [TestMethod()]
        public void MaxIdTest()
        {

            List<Postac> listatest = new List<Postac>();
            int expected = 2;
            listatest.Add(new Postac(1, "J", "R"));
            listatest.Add(new Postac(2, "A", "B"));
            int actual = listatest.MaxId();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PostacConstructorTest()
        {
            Postac postac = new Postac(1, "A", "B");
            Assert.AreEqual(postac.Id, 1);
            Assert.AreEqual(postac.IMIE, "A");
            Assert.AreEqual(postac.NAZWISKO, "B");
        }

        [TestMethod]
        public void WczytajListeTest()
        {
            SQLiteCommand komenda;
            string _zapytanieSQL = string.Format("");
            //SQLiteDataReader czytnik;
            //string zapytanieSQL = "";
            string imie = "Kacper";
            string nazwisko = "Czarnowski";
            int expected = 0;
            SQLiteConnection _polaczenie = new SQLiteConnection("Data Source=baza.db");
            _polaczenie.Open();

            new SQLiteCommand("drop table Tabela", _polaczenie).ExecuteNonQuery();
            new SQLiteCommand("create table Tabela(id INTEGER, imie TEXT, nazwisko TEXT)", _polaczenie).ExecuteNonQuery();
            //new SQLiteCommand("insert into Tabela (Id, IMIE, NAZWISKO) values (1, 'Jan', 'Kowalski')", _polaczenie).ExecuteNonQuery();
            for (int i = 1; i <= 100; i++)
            {
                _zapytanieSQL = string.Format("INSERT INTO Tabela (id, IMIE, NAZWISKO) " + "VALUES('{0}', '{1}', '{2}')",i, imie, nazwisko);
                komenda = new SQLiteCommand(_zapytanieSQL, _polaczenie);
                komenda.ExecuteNonQuery();
                expected = i;
            }


            List<Postac> listaPostaci = Utils.WczytajDane(_polaczenie);
            Assert.AreEqual(expected,listaPostaci.Count);
        }
    }
}