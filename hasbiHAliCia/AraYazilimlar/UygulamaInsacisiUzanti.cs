using hasbiHAliCia.Veri;

namespace hasbiHAliCia.AraYazilimlar
{
    public static class UygulamaInsacisiUzanti
    {
        public static void IletilerTabloBagimiKullan(this IApplicationBuilder uygulamaInsacisi) {
            var HizmetSaglayici = uygulamaInsacisi.ApplicationServices;
            var Hizmet = HizmetSaglayici.GetService<IletilerAbonesi>();
            Hizmet.TabloBaginaAboneOl();
        }
    }
}
