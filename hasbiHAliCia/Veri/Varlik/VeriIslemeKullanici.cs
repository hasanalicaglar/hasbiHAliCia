using hasbiHAliCia.Models;
using System.Linq.Expressions;

namespace hasbiHAliCia.Veri.Varlik
{
    public static class VeriIslemeKullanici
    {

        public static void Ekle(VeritabaniBaglami VtB, VKullanici kullanici)
        {
            VtB.Add(kullanici);
            VtB.SaveChanges();
        }

        public static void Sil(VeritabaniBaglami VtB, VKullanici kullanici)
        {
            VtB.Remove(kullanici);
            VtB.SaveChanges();
        }

        public static List<VKullanici> TumunuGetir(VeritabaniBaglami VtB)
        {
            return VtB.Set<VKullanici>().ToList();
        }

        public static List<VKullanici> TumunuGetir(VeritabaniBaglami VtB, Expression<Func<VKullanici, bool>> Suzgec)
        {
            return VtB.Set<VKullanici>().Where(Suzgec).ToList();
        }

        public static VKullanici KimlikleGetir(VeritabaniBaglami VtB, Guid Kimlik)
        {
            return VtB.Set<VKullanici>().Find(Kimlik);
        }

        public static void Guncelle(VeritabaniBaglami VtB, VKullanici varlik)
        {
            VtB.Update(varlik);
            VtB.SaveChanges();
        }
    }
}
