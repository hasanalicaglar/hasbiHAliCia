using Azure.Core;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace hasbiHAliCia.Controllers.Araclar
{
    public class Ziyaret
    {
        public Ziyaret(HttpContext HttpBaglami)
        {
            Ziyaret.HttpBaglami = HttpBaglami;
            CerezAyarlari = new CookieOptions();
            CerezAyarlari.Path = "/";
            CerezAyarlari.Expires = DateTime.Now.AddDays(365);

            var kayitliKimlikOturum = HttpBaglami.Session.GetString("ZiyaretciK");
            var kayitliKimlikCerez = HttpBaglami.Request.Cookies["ZiyaretciK"];

            if (!string.IsNullOrEmpty(kayitliKimlikOturum))
            {
                List<dynamic> GUIDDonusumuOturum = YararliIslevler.MetindenGUIDDonusturucu(kayitliKimlikOturum);
                if (GUIDDonusumuOturum[0])
                {
                    Kimlik = GUIDDonusumuOturum[1];
                    HttpBaglami.Response.Cookies.Append("ZiyaretciK", Kimlik.ToString(), CerezAyarlari);
                }
                else
                {
                    YeniKimlikOlustur();
                }
            }
            else if (!string.IsNullOrEmpty(kayitliKimlikCerez))
            {
                HttpBaglami.Session.SetString("ZiyaretciK", Kimlik.ToString());
                List<dynamic> GUIDDonusumuCerez = YararliIslevler.MetindenGUIDDonusturucu(kayitliKimlikCerez);
                if (GUIDDonusumuCerez[0])
                {
                    Kimlik = GUIDDonusumuCerez[1];
                    HttpBaglami.Session.SetString("ZiyaretciK", Kimlik.ToString());
                }
                else
                {
                    YeniKimlikOlustur();
                }
            }
            else
            {
                YeniKimlikOlustur();
            }




            IPAdresi = YararliIslevler.IPAdresTespiti(HttpBaglami);
            IstekZamani = DateTime.Now;
            IstekKonum = HttpBaglami.Request.Scheme + "://" + HttpBaglami.Request.Host + HttpBaglami.Request.Path + HttpBaglami.Request.QueryString; ;
            Aygit = HttpBaglami.Request.Headers.UserAgent;
            Dil = HttpBaglami.Request.Headers.AcceptLanguage; ;
            ReferansKonum = HttpBaglami.Request.Headers.Referer;

            Bilgiler = new List<dynamic>
            {
                Kimlik,
                IPAdresi,
                IstekZamani,
                IstekKonum,
                Aygit,
                Dil,
                ReferansKonum
            };
        }

        public string BilgilerTekSatir()
        {

            List<string> stringBilgiler = YararliIslevler.SozlukIciniStringlestir(Bilgiler);
            return YararliIslevler.SozlukTekSatir(stringBilgiler);
        }
        public void Kaydet()
        {
            YararliIslevler.DosyayaYaz(Path.GetFullPath("wwwroot") + "\\Ziyaretciler.txt", BilgilerTekSatir());
        }

        private Guid YeniKimlikOlustur()
        {
            Kimlik = Guid.NewGuid();
            HttpBaglami.Session.SetString("ZiyaretciK", Kimlik.ToString());
            HttpBaglami.Response.Cookies.Append("ZiyaretciK", Kimlik.ToString(), CerezAyarlari);

            return Kimlik;
        }

        public Guid Kimlik { get; set; }
        public IPAddress IPAdresi { get; set; }
        public DateTime IstekZamani { get; set; }
        public string IstekKonum { get; set; }
        public string? Aygit { get; set; }
        public string? Dil { get; set; }
        public string? ReferansKonum { get; set; }
        public static HttpContext HttpBaglami { get; set; }
        public List<dynamic> Bilgiler { get; set; }
        public CookieOptions CerezAyarlari { get; set; }


    }
}