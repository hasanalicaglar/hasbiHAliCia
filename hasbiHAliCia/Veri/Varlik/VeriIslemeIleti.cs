using hasbiHAliCia.Models;
using System.Linq.Expressions;

namespace hasbiHAliCia.Veri.Varlik
{
    public static class VeriIslemeIleti
    {


        public static void Ekle(VeritabaniBaglami VtB, VIleti ileti)
        {
            VtB.Add(ileti);
            VtB.SaveChanges();
        }

        public static void Sil(VeritabaniBaglami VtB, VIleti ileti)
        {
            VtB.Remove(ileti);
            VtB.SaveChanges();
        }

        public static List<VIleti> TumunuGetir(VeritabaniBaglami VtB)
        {
            return VtB.Set<VIleti>().ToList();
        }

        public static List<VIleti> TumunuGetir(VeritabaniBaglami VtB, Expression<Func<VIleti, bool>> Suzgec)
        {
            return VtB.Set<VIleti>().Where(Suzgec).ToList();

        }

        public static VIleti KimlikleGetir(VeritabaniBaglami VtB, int Kimlik)
        {
            return VtB.Set<VIleti>().Find(Kimlik);
        }

        public static void Guncelle(VeritabaniBaglami VtB, VIleti varlik)
        {
            VtB.Update(varlik);
            VtB.SaveChanges();
        }
    }
}
