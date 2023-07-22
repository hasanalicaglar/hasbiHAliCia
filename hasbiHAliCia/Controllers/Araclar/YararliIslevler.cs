using Azure.Core;
using System.Net.Sockets;
using System.Net;
using System.IO;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text.RegularExpressions;
using Microsoft.Extensions.FileSystemGlobbing;

namespace hasbiHAliCia.Controllers.Araclar
{
    public static class YararliIslevler
    {

        public static IPAddress IPAdresTespiti(HttpContext HttpBaglami)
        {


            IPAddress? IPAdresi = null;
            var yonlendirilme = HttpBaglami.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(yonlendirilme))
            {
                var ParcalanacakIP = yonlendirilme.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                      .Select(s => s.Trim());
                foreach (var GeciciIP in ParcalanacakIP)
                {
                    if (IPAddress.TryParse(GeciciIP, out var address) &&
                        (address.AddressFamily is AddressFamily.InterNetwork
                         or AddressFamily.InterNetworkV6))
                    {
                        IPAdresi = address;
                        break;
                    }
                }
            }
            else
            {
                IPAdresi = HttpBaglami.Connection.RemoteIpAddress;
            }

            return IPAdresi;
        }

        public static List<string> SozlukIciniStringlestir(List<dynamic> donusecekSozluk)
        {
            List<string> Sozluk = new List<string>();

            for (var i = 0; i < donusecekSozluk.Count; i++)
            {
                if (donusecekSozluk[i] != null)
                {
                    if (donusecekSozluk[i] is not string)
                    {
                        donusecekSozluk[i] = donusecekSozluk[i].ToString();
                    }
                }
                else
                {
                    donusecekSozluk[i] = "";
                }

                Sozluk.Add(donusecekSozluk[i]);

            }

            return Sozluk;
        }

        public static string SozlukTekSatir(List<string> donusecekSozluk)
        {

            string TekSatirBilgi = "";
            for (var i = 0; i < donusecekSozluk.Count; i++)
            {

                string Bilgi = donusecekSozluk[i];
                if (i == donusecekSozluk.Count - 1)
                {
                    TekSatirBilgi += Bilgi + "\n";
                }
                else
                {
                    TekSatirBilgi += Bilgi + "\t";
                }
            }

            return TekSatirBilgi;
        }

        public static void DosyayaYaz(string dosyaYolu, string Icerik)
        {

            using var AkisYazicisi = new StreamWriter(dosyaYolu, true);
            AkisYazicisi.Write(Icerik);

        }

        public static bool MetindeDuzenliIfadeDenetleme(string kullaniciAdi, string doku, int karakterSayisi = 999999999)
        {

            bool gecerliMi = false;

            if (kullaniciAdi.Length <= karakterSayisi && kullaniciAdi.Length > 0)
            {
                Regex duzenliIfade = new Regex(doku, RegexOptions.IgnoreCase);
                if (duzenliIfade.IsMatch(kullaniciAdi))
                {
                    gecerliMi = true;
                }

            }
            return gecerliMi;
        }

        public static List<dynamic> MetindenGUIDDonusturucu(string hamGUID) {
            Guid donusmusGUID;
            bool donusmeDurumu = Guid.TryParse(hamGUID, out donusmusGUID);
            if (donusmeDurumu)
            {
                return new List<dynamic>() { donusmeDurumu, donusmusGUID };
            }
            else {
                return new List<dynamic>() { donusmeDurumu, "" };
            }

        }
    }
}
