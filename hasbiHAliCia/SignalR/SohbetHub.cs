using hasbiHAliCia.Controllers.Araclar;
using hasbiHAliCia.Models;
using hasbiHAliCia.Veri;
using hasbiHAliCia.Veri.Varlik;
using Microsoft.AspNetCore.SignalR;
using System.Dynamic;

namespace hasbiHAliCia.SignalR
{
    public class SohbetHub : Hub
    {
        List<VIleti> iletiler;
        public SohbetHub(IConfiguration configuration)
        {
            
        }

        public async Task IletileriAl()
        {
            using (var VtB = new VeritabaniBaglami()) {
                iletiler = VeriIslemeIleti.TumunuGetir(VtB);
            }
                
            
            List<dynamic> KullaniciAdliIletiler = new List<dynamic>();

            foreach (VIleti ileti in iletiler) {
                dynamic GenisleyebilirIleti = new ExpandoObject();
                GenisleyebilirIleti.Kimlik = ileti.Kimlik;
                GenisleyebilirIleti.Icerik = ileti.Icerik;
                using (var VtB = new VeritabaniBaglami())
                {
                    GenisleyebilirIleti.KullaniciAdi = VeriIslemeKullanici.KimlikleGetir(VtB, ileti.KullaniciK).KullaniciAdi;
                }
               
                GenisleyebilirIleti.GonderilmeZamani = ileti.GonderilmeZamani.ToString();
                KullaniciAdliIletiler.Add(GenisleyebilirIleti);
            }
            await Clients.All.SendAsync("IletiAl", KullaniciAdliIletiler);
        }

    }
}