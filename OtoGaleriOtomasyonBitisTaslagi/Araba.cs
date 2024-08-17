using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoGaleriUygulamasi
{
    internal class Araba
    {


        public string Plaka { get; set; }
        public string Marka { get; set; }
        public float KiralamaBedeli { get; set; }
        public string AracTipi { get; set; }
        public string Durum = "Galeride";
        public List<int> KiralamaSureleri = new List<int>();


        public Araba(string plaka, string marka, float kiralamaBedeli, string aractipi)
        {
            Plaka = plaka;
            Marka = marka;
            KiralamaBedeli = kiralamaBedeli;
            AracTipi = aractipi;
        }


        public int KiralanmaSayisi
        {
            get
            {
                return this.KiralamaSureleri.Count;
            }
        }

        public int ToplamKiralanmaSuresi
        {
            get
            {
                //int toplam = 0;
                //foreach (int item in KiralamaSureleri)
                //{
                //    toplam += item;
                //}
                //return toplam;
                return this.KiralamaSureleri.Sum();
            }
        }




    }
}
