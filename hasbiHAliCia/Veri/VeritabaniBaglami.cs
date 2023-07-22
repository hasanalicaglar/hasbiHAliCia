using hasbiHAliCia.Veri.Varlik;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace hasbiHAliCia.Veri
{
    public class VeritabaniBaglami : DbContext


    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VIleti>()
                .ToTable(tb => tb.HasTrigger("SomeTrigger"));
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; database=hasbiHAliCia; Integrated Security=true;");
		//optionsBuilder.UseSqlServer("Data Source=KendiSunucunuz; database=hasbiHAliCia; User ID=KullaniciAdi;Password=Sifre;Encrypt=False;");

        }

        public DbSet<VKullanici> Kullanicilar { get; set; }
        public DbSet<VIleti> VIleti { get; set; }


    }
}
