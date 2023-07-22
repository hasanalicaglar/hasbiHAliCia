using System.ComponentModel.DataAnnotations;

namespace hasbiHAliCia.Veri.Varlik
{
    public class VKullanici
    {
        [Key]
        public Guid Kimlik { get; set; }
        public string KullaniciAdi { get; set; }
        public DateTime GirisZamani { get; set; }

    }
}
