using System.ComponentModel.DataAnnotations;

namespace hasbiHAliCia.Veri.Varlik
{
    public class VIleti
    {

        [Key]
        public int Kimlik { get; set; }
        public string Icerik { get; set; }
        public DateTime GonderilmeZamani { get; set; }
        public Guid KullaniciK { get; set; }

    }
}
