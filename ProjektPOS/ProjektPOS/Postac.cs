using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektPOS
{
    /// <summary>
    /// This class contains three fields: int Id, string IMIE, string NAZWISKO.
    /// </summary>
    public class Postac
    {
     
        /// <summary>
        /// Gets or sets Id number for Postac class object.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets IMIE string for Postac class object.
        /// </summary>
        public string IMIE { get; set; }
        /// <summary>
        /// Gets or sets Nazwisko string for Postac class object.
        /// </summary>
        public string NAZWISKO { get; set; }
        /// <summary>
        /// Overload method Postac.
        /// </summary>
       public Postac() { }
        
        /// <summary>
        ///Overload method Postac.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="IMIE"></param>
        /// <param name="NAZWISKO"></param>
        public Postac(int Id, string IMIE, string NAZWISKO)
        {
            this.Id = Id;
            this.IMIE = IMIE;
            this.NAZWISKO = NAZWISKO;
            
        }




    }
}
