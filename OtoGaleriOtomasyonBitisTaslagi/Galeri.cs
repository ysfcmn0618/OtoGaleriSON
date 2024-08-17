using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoGaleriUygulamasi
{
    internal class Galeri
    {


        public List<Araba> Arabalar = new List<Araba>();

        public int ToplamAracSayisi
        {
            get
            {
                return this.Arabalar.Count;
            }
        }


        public int KiradakiAracSayisi
        {
            get
            {
                int adet = 0;

                foreach (Araba item in Arabalar)
                {
                    if (item.Durum == "Kirada")
                    {
                        adet++;
                    }
                }
                return adet;
            }
        }

        public int GaleridekiAracSayisi
        {
            get
            {
                int adet = 0;

                foreach (Araba item in Arabalar)
                {
                    if (item.Durum == "Galeride")
                    {
                        adet++;
                    }
                }
                return adet;
            }
        }
        public int ToplamAracKiralamaSuresi
        {
            get
            {
                int toplam = 0;
                foreach (Araba item in Arabalar)
                {
                    toplam += item.ToplamKiralanmaSuresi;
                }
                return toplam;
            }
        }
        public int ToplamAracKiralamaAdedi
        {
            get
            {
                int toplam = 0;
                foreach (Araba item in Arabalar)
                {
                    toplam += item.KiralamaSureleri.Count;

                }
                return toplam;
            }
        }


        public float Ciro
        {
            get
            {
                float ciro = 0;
                foreach (Araba item in Arabalar)
                {
                    foreach (int süre in item.KiralamaSureleri)
                    {
                        ciro += süre * item.KiralamaBedeli;
                    }
                }
                return ciro;
            }
        }


        public void ArabaKirala(string plaka, int sure)
        {


            Araba a = null;

            foreach (Araba item in Arabalar)
            {
                if (item.Plaka == plaka)
                {
                    a = item;
                    a.Durum = "Kirada";
                    a.KiralamaSureleri.Add(sure);
                }

            }


        }
        public bool AracGaleridemi(Araba car)
        {
            if (car == null)
            {
                Console.WriteLine("Galeriye ait bu plakada bir araba yok.");
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ArabaTeslimAl(string plaka)
        {

            Araba a = null;

            foreach (Araba item in Arabalar)
            {
                if (item.Plaka == plaka)
                {
                    a = item;
                }
            }
            if (a != null && a.Durum == "Kirada")
            {
                a.Durum = "Galeride";

            }
        }

        public void KiralamaIptal(string plaka)
        {

            foreach (Araba item in Arabalar)
            {
                if (item.Plaka == plaka)
                {

                    item.KiralamaSureleri.RemoveAt(item.KiralamaSureleri.Count - 1);

                    ArabaTeslimAl(plaka);
                    break;
                }
            }
        }
       
        public void ArabaEkle(string plaka, string marka, float kiralamaBedeli, string aTipi)
        {


            Araba a = new Araba(plaka, marka, kiralamaBedeli, aTipi);
            this.Arabalar.Add(a);
            Console.WriteLine("Araba başarılı bir şekilde eklendi.");

        }

    }
}
