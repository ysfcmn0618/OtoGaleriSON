using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.Intrinsics.X86;

namespace OtoGaleriUygulamasi
{
    internal class Program
    {
        static Galeri OtoGaleri = new Galeri();
        static List<Araba> Arabalar = OtoGaleri.Arabalar;
        static void Main(string[] args)
        {
            SahteVeriEkle();
            Menu();
            Uygulama();
        }
        static void Uygulama()
        {
            while (true)
            {
                string secim = SecimAl();
                switch (secim)
                {
                    case "1":
                    case "K": ArabaKirala(); break;
                    case "2":
                    case "T": ArabaTeslimAl(); break;
                    case "3":
                    case "R": KiradakiArabalariListele(); break;
                    case "4":
                    case "M": GaleridekiArabalariListele(); break;
                    case "5":
                    case "A": TümArabalariListele(); break;
                    case "6":
                    case "I": KiralamaIptal(); break;
                    case "7":
                    case "Y": ArabaEkle(); break;
                    case "8":
                    case "S": ArabaSil(); break;
                    case "9":
                    case "G": BilgileriGöster(); break;
                    case "C": Console.Clear(); break;
                    case "L": Menu(); break;
                }
            }
        }
        private static void BilgileriGöster()
        {
            Console.WriteLine("-Galeri Bilgileri-");
            Console.WriteLine("Toplam araba sayısı: " + OtoGaleri.ToplamAracSayisi);
            Console.WriteLine("Kiradaki araba sayısı: " + OtoGaleri.KiradakiAracSayisi);
            Console.WriteLine("Bekleyen araba sayısı: " + OtoGaleri.GaleridekiAracSayisi);
            Console.WriteLine("Toplam araba kiralama süresi: " + OtoGaleri.ToplamAracKiralamaSuresi);
            Console.WriteLine("Toplam araba kiralama adedi: " + OtoGaleri.ToplamAracKiralamaAdedi);
            Console.WriteLine("Ciro: " + OtoGaleri.Ciro);
        }
        private static void ArabaSil()
        {
            Console.WriteLine("-Araba Sil-");
            Console.WriteLine();
            if (Arabalar.Count == 0)
            {
                Console.WriteLine("Galeride silinecek araba yok.");
                return;
            }
            string plaka;
            do
            {
                plaka = GirdiAl("Silmek istediğiniz arabanın plakasını giriniz: ");
                if (!PlakaDogruMu(plaka))
                {
                    PlakaHata();
                    continue;
                }
                Araba car = null;
                foreach (Araba a in Arabalar)
                {
                    if (a.Plaka == plaka)
                    {
                        car = a;
                        break;
                    }
                }
                if (OtoGaleri.AracGaleridemi(car))
                {
                    continue;
                }
                if (car.Durum == "Kirada")
                {
                    Console.WriteLine("Araba kirada olduğu için silme işlemi gerçekleştirilemedi.");
                    continue;
                }
                Console.WriteLine();
                Arabalar.Remove(car);
                Console.WriteLine("Araba silindi.");
                return;
            } while (true);
        }       
        public static void KiralamaIptal()
        {
            Console.WriteLine("-Kiralama İptali-");
            Console.WriteLine();
            bool kiradaArabaVar = false;
            foreach (Araba a in Arabalar)
            {
                if (a.Durum == "Kirada")
                {
                    kiradaArabaVar = true;
                    break;
                }
            }
            if (!kiradaArabaVar)
            {
                Console.WriteLine("Kirada araba yok.");
                return;
            }
            Araba car = null;
            string plaka;
            do
            {
                plaka = GirdiAl("Kiralaması iptal edilecek arabanın plakası: ");

                if (!PlakaDogruMu(plaka))
                {
                    PlakaHata();
                    continue;
                }
                foreach (Araba a in Arabalar)
                {
                    if (a.Plaka == plaka)
                    {
                        car = a;
                        break;
                    }
                }
                if (OtoGaleri.AracGaleridemi(car))
                {
                    continue;
                }
                if (car.Durum == "Galeride")
                {
                    Console.WriteLine("Hatalı giriş yapıldı. Araba zaten galeride.");
                    continue;
                }
                car.Durum = "Galeride";
                Console.WriteLine();
                Console.WriteLine("İptal gerçekleştirildi.");
                OtoGaleri.KiralamaIptal(plaka);
                break;
            } while (true);
        }
        private static void TümArabalariListele()
        {
            Console.WriteLine("-Tüm Arabalar-");
            Console.WriteLine();
            if (Arabalar.Count == 0)
            {
                Console.WriteLine("Listelenecek araç yok.");
                return;
            }
            Console.WriteLine("Plaka".PadRight(14) + "Marka".PadRight(12) + "K. Bedeli".PadRight(12) + "Araba Tipi".PadRight(12) + "K. Sayısı".PadRight(12) + "Durum".PadRight(12));
            Console.WriteLine("----------------------------------------------------------------------");
            foreach (var araba in Arabalar)
            {
                Console.WriteLine(araba.Plaka.PadRight(14) +
                araba.Marka.PadRight(12) +
                araba.KiralamaBedeli.ToString().PadRight(12) +
                araba.AracTipi.PadRight(12) +
                araba.KiralanmaSayisi.ToString().PadRight(12) +
                araba.Durum.PadRight(10)
            );
            }
        }
        private static void GaleridekiArabalariListele()
        {
            Console.WriteLine("-Galerideki Arabalar-");
            Console.WriteLine();
            bool galerideArabaVar = false;
            foreach (var araba in Arabalar)
            {
                if (araba.Durum == "Galeride")
                {
                    if (!galerideArabaVar)
                    {
                        Console.WriteLine("Plaka".PadRight(14) + "Marka".PadRight(12) + "K. Bedeli".PadRight(12) + "Araba Tipi".PadRight(12) + "K. Sayısı".PadRight(12) + "Durum".PadRight(12));
                        Console.WriteLine("----------------------------------------------------------------------");
                        galerideArabaVar = true;
                    }
                    Console.WriteLine(
                    araba.Plaka.PadRight(14) +
                    araba.Marka.PadRight(12) +
                    araba.KiralamaBedeli.ToString().PadRight(12) +
                    araba.AracTipi.PadRight(12) +
                    araba.KiralanmaSayisi.ToString().PadRight(12) +
                    araba.Durum.PadRight(10));
                }
            }
            if (!galerideArabaVar)
            {
                Console.WriteLine("Listelenecek araç yok.");
                return;
            }
        }
        private static void KiradakiArabalariListele()
        {
            Console.WriteLine("-Kiradaki Arabalar-");
            Console.WriteLine();
            bool kiradaArabaVar = false;
            foreach (Araba a in Arabalar)
            {
                if (a.Durum == "Kirada")
                {
                    if (!kiradaArabaVar)
                    {
                        Console.WriteLine("Plaka".PadRight(14) + "Marka".PadRight(12) + "K. Bedeli".PadRight(12) + "Araba Tipi".PadRight(12) + "K. Sayısı".PadRight(12) + "Durum".PadRight(12));
                        Console.WriteLine("----------------------------------------------------------------------");
                        kiradaArabaVar = true;
                    }
                    Console.WriteLine(
                    a.Plaka.PadRight(14) +
                    a.Marka.PadRight(12) +
                    a.KiralamaBedeli.ToString().PadRight(12) +
                    a.AracTipi.PadRight(12) +
                    a.KiralanmaSayisi.ToString().PadRight(12) +
                    a.Durum.PadRight(10));
                }
            }
            if (!kiradaArabaVar)
            {
                Console.WriteLine("Listelenecek araç yok.");
                return;
            }
        }
        static void SahteVeriEkle()
        {
            Araba a = new Araba("34ARB3434", "FIAT", 70, "Sedan") { };
            Araba b = new Araba("35ARB3535", "KIA", 60, "SUV") { };
            Araba c = new Araba("34US2342", "OPEL", 50, "Hatchback") { };
            Arabalar.Add(a);
            Arabalar.Add(b);
            Arabalar.Add(c);
        }
        private static void ArabaTeslimAl()
        {
            Console.WriteLine("-Araba Teslim Al-");
            Console.WriteLine();
            if (Arabalar.Count == 0)
            {
                Console.WriteLine("Galeride hiç araba yok.");
                return;
            }
            bool kiradaArabaVar = false;
            foreach (Araba a in Arabalar)
            {
                if (a.Durum == "Kirada")
                {
                    kiradaArabaVar = true;
                    break;
                }
            }
            if (!kiradaArabaVar)
            {
                Console.WriteLine("Kirada hiç araba yok.");
                return;
            }
            string plaka;
            Araba car = null;
            do
            {
                plaka = GirdiAl("Teslim edilecek arabanın plakası: ");

                if (!PlakaDogruMu(plaka))
                {
                    PlakaHata();
                    continue;
                }
                foreach (Araba a in Arabalar)
                {
                    if (a.Plaka == plaka)
                    {
                        car = a;
                        break;
                    }
                }
                if (OtoGaleri.AracGaleridemi(car))
                {
                    continue;
                }
                if (car.Durum == "Galeride")
                {
                    Console.WriteLine("Hatalı giriş yapıldı. Araba zaten galeride.");
                    continue;
                }
                car.Durum = "Galeride";
                Console.WriteLine();
                Console.WriteLine("Araba galeride beklemeye alındı.");
                break; ;
            } while (true);
        }
        static string SecimAl()
        {
            string karakterler = "123456789KTRMAIYSGXCL";
            int sayac = 0;
            while (true)
            {
                sayac++;
                Console.WriteLine();
                Console.Write("Seçiminiz : ");
                string giris = Console.ReadLine().ToUpper();
                Console.WriteLine();
                int index = karakterler.IndexOf(giris);
                if (giris.Length == 1 && index >= 0)
                {
                    return giris;
                }
                else
                {
                    if (sayac == 10)
                    {
                        Environment.Exit(0);
                    }
                    Console.WriteLine("Hatalı işlem gerçekleştirildi. Tekrar deneyin.");
                }
            }
        }
        static void Menu()
        {
            Console.WriteLine("Galeri Otomasyon");
            Console.WriteLine("1- Araba Kirala (K)");
            Console.WriteLine("2- Araba Teslim Al (T)");
            Console.WriteLine("3- Kiradaki Arabaları Listele (R)");
            Console.WriteLine("4- Galerideki Arabaları Listele (M)");
            Console.WriteLine("5- Tüm Arabaları Listele (A)");
            Console.WriteLine("6- Kiralama İptali (I)");
            Console.WriteLine("7- Araba Ekle (Y)");
            Console.WriteLine("8- Araba Sil (S)");
            Console.WriteLine("9- Bilgileri Göster (G)");
        }
        static void ArabaKirala()
        {
            Console.WriteLine("-Araba Kirala-");
            Console.WriteLine();
            if (Arabalar.Count == 0)
            {
                Console.WriteLine("Galeride hiç araba yok.");
                return;
            }
            if (OtoGaleri.KiradakiAracSayisi == OtoGaleri.ToplamAracSayisi)
            {
                Console.WriteLine("Tüm araçlar kirada.");
                return;
            }
            string plaka;
            do
            {
                plaka = GirdiAl("Kiralanacak arabanın plakası: ");
                if (!PlakaDogruMu(plaka))
                {
                    PlakaHata();
                    continue;
                }
                Araba car = null;
                foreach (Araba a in Arabalar)
                {
                    if (a.Plaka == plaka)
                    {
                        car = a;
                        break;
                    }
                }
                if (car == null)
                {
                    Console.WriteLine("Galeriye ait bu plakada bir araba yok.");
                    continue;
                }
                if (car.Durum == "Kirada")
                {
                    Console.WriteLine("Araba şu anda kirada. Farklı araba seçiniz.");
                    continue;
                }
                while (true)
                {
                    string deger = GirdiAl("Kiralama süresi: ");

                    if (int.TryParse(deger, out int sure))
                    {
                        Console.WriteLine();
                        Console.WriteLine(plaka + " plakalı araba " + sure + " saatliğine kiralandı.");
                        OtoGaleri.ArabaKirala(plaka, sure);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                    }
                }
            } while (true);
        }
        static void PlakaHata()
        {
            Console.WriteLine("Bu şekilde plaka girişi yapamazsınız. Tekrar deneyin.");
        }
        static bool PlakaDogruMu(string plaka)
        {
            if (plaka.Length < 7 || plaka.Length > 9)
            {
                return false;
            }
            string sehir = plaka.Substring(0, 2);
            if (!int.TryParse(sehir, out int sayi))
            {
                return false;
            }
            string kalan = plaka.Substring(2);
            int sayac = 0;
            while (sayac < kalan.Length && char.IsLetter(kalan[sayac]))
            {
                sayac++;
            }
            string harfler = kalan.Substring(0, sayac);
            if (harfler.Length < 1 && harfler.Length > 3)
            {
                return false;
            }
            string rakamlar = kalan.Substring(sayac);
            if (!int.TryParse(rakamlar, out int sayi2) || rakamlar.Length < 2 || rakamlar.Length > 4)
            {
                return false;
            }
            return true;
        }
        static void ArabaEkle()
        {
            Console.WriteLine("-Araba Ekle-");
            Console.WriteLine();
            string plaka;
            do
            {
                plaka = GirdiAl("Plaka: ");
                if (!PlakaDogruMu(plaka))
                {
                    PlakaHata();
                    continue;
                }
                bool plakaMevcutMu = false;
                foreach (Araba a in Arabalar)
                {

                    if (a.Plaka == plaka)
                    {
                        Console.WriteLine("Aynı plakada araba mevcut. Girdiğiniz plakayı kontrol edin.");
                        plakaMevcutMu = true;
                        break;
                    }
                }
                if (!plakaMevcutMu)
                {
                    break;
                }

            } while (true);
            string marka;
            do
            {
                marka = GirdiAl("Marka: ");

                if (int.TryParse(marka, out int a))
                {
                    Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                    continue;
                }
                break;

            } while (true);
            int kiralamaBedeli = 0;
            do
            {
                string bedel = GirdiAl("Kiralama bedeli: ");
                if (!int.TryParse(bedel, out kiralamaBedeli))
                {
                    Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                    continue;
                }
                break;
            } while (true);

            Console.WriteLine("Araç tipi:\r\nSUV için 1\r\nHatchback için 2\r\nSedan için 3");
            string aTipi = "";
            do
            {
                aTipi = GirdiAl("Araba Tipi: ");
                if (int.TryParse(aTipi, out int t))
                {
                    switch (aTipi)
                    {
                        case "1": aTipi = "SUV"; break;
                        case "2": aTipi = "Hatchback"; break;
                        case "3": aTipi = "Sedan"; break;
                        default:
                            Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                            continue;
                    }
                }
                else
                {
                    Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                    continue;
                }
                Console.WriteLine();
                OtoGaleri.ArabaEkle(plaka, marka, kiralamaBedeli, aTipi);
                break;
            } while (true);
        }
        static string GirdiAl(string mesaj)
        {
            Console.Write(mesaj);
            string girdi = Console.ReadLine().ToUpper();

            if (girdi == "X")
            {
                Uygulama();
            }
            return girdi;
        }
    }
}
