using hasbiHAliCia.Controllers.Araclar;
using hasbiHAliCia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Sockets;
using System.Net;
using System.Web;
using System.IO;
using System.Xml;
using hasbiHAliCia.Veri.Varlik;
using hasbiHAliCia.Veri;
using static Azure.Core.HttpHeader;
using System.Dynamic;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace hasbiHAliCia.Controllers
{
    [ZiyaretActionFilter]
    public class AnasayfaController : DenetleyiciOzu
    {
        private readonly ILogger<AnasayfaController> _logger;

        public AnasayfaController(ILogger<AnasayfaController> logger)
        {
            _logger = logger;

        }


        public IActionResult Giris()
        {
            using (var VtB = new VeritabaniBaglami())
            {
                VKullanici mevcutKullanici = VeriIslemeKullanici.KimlikleGetir(VtB, ZiyaretActionFilter.ziyaret.Kimlik);
                if (mevcutKullanici != null)
                {
                    return RedirectToAction("Duvar");
                }
                else
                {
                    return View();
                }
            }
        }

        [HttpPost]
        public IActionResult Duvar(Kullanici kullanici)
        {
            using (var VtB = new VeritabaniBaglami())
            {
                bool Durum = YararliIslevler.MetindeDuzenliIfadeDenetleme(kullanici.KullaniciAdi, "^[a-zA-Z0-9_]*$", 50);


                if (Durum)
                {


                    VKullanici mevcutKullanici = VeriIslemeKullanici.KimlikleGetir(VtB, ZiyaretActionFilter.ziyaret.Kimlik);
                    if (mevcutKullanici == null)
                    {
                        var mevcutKullaniciAdi = VtB.Kullanicilar.SingleOrDefault(x => x.KullaniciAdi == kullanici.KullaniciAdi);
                        if (mevcutKullaniciAdi == null)
                        {
                            var eklenecekKullanici = new VKullanici()
                            {
                                Kimlik = ZiyaretActionFilter.ziyaret.Kimlik,
                                KullaniciAdi = kullanici.KullaniciAdi,
                                GirisZamani = ZiyaretActionFilter.ziyaret.IstekZamani
                            };

                            VeriIslemeKullanici.Ekle(VtB, eklenecekKullanici);
                            ViewBag.Kullanici = eklenecekKullanici;


                            return View();
                        }
                        else
                        {
                            return RedirectToAction("Giris", new { Hata = 2 });
                        }





                        
                    }
                    else
                    {
                        return RedirectToAction("Duvar");

                    }

                }
                else
                {
                    return RedirectToAction("Giris", new { Hata = 1 });
                }
            }

        }
        [HttpGet]
        public IActionResult Duvar()
        {
            using (var VtB = new VeritabaniBaglami())
            {
                VKullanici mevcutKullanici = VeriIslemeKullanici.KimlikleGetir(VtB, ZiyaretActionFilter.ziyaret.Kimlik);


                if (mevcutKullanici != null)
                {
                    List<VIleti> iletiler = VeriIslemeIleti.TumunuGetir(VtB);


                    List<dynamic> KullaniciAdliIletiler = new List<dynamic>();

                    foreach (VIleti ileti in iletiler)
                    {
                        dynamic GenisleyebilirIleti = new ExpandoObject();
                        GenisleyebilirIleti.Kimlik = ileti.Kimlik;
                        GenisleyebilirIleti.Icerik = ileti.Icerik;
                        GenisleyebilirIleti.KullaniciAdi = VeriIslemeKullanici.KimlikleGetir(VtB, ileti.KullaniciK).KullaniciAdi;

                        GenisleyebilirIleti.GonderilmeZamani = ileti.GonderilmeZamani.ToString();
                        KullaniciAdliIletiler.Add(GenisleyebilirIleti);
                    }

                    KullaniciAdliIletiler.Reverse();

                    ViewBag.Kullanici = mevcutKullanici;
                    ViewBag.Iletiler = KullaniciAdliIletiler;
                    return View();
                }
                else
                {
                    return RedirectToAction("Giris");
                }
            }

        }

        [HttpPost]
        public IActionResult IletiMerkezi(Ileti ileti)
        {
            if (!string.IsNullOrWhiteSpace(ileti.Icerik))
            {
                var eklenecekIleti = new VIleti()
                {
                    Icerik = ileti.Icerik,
                    GonderilmeZamani = DateTime.Now,
                    KullaniciK = ZiyaretActionFilter.ziyaret.Kimlik

                };
                using (var VtB = new VeritabaniBaglami())
                {
                    VeriIslemeIleti.Ekle(VtB, eklenecekIleti);
                }

            }
            else
            {
                ViewBag.Hata = "Hata";
            }
            return View();
        }
        public IActionResult Cikis()
        {
            HttpContext.Response.Cookies.Delete("ZiyaretciK");
            HttpContext.Response.Cookies.Delete("hasbiHAliCia");
            return RedirectToAction("Giris");
        }

        public IActionResult Hakkinda() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Hata()
        {
            return View(new HataViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}