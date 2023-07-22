using hasbiHAliCia.SignalR;
using hasbiHAliCia.Veri.Varlik;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace hasbiHAliCia.Veri
{
    public class IletilerAbonesi

    {
        SqlTableDependency<VIleti> tabloBagimi;
        SohbetHub sohbetMerkezi;

        public IletilerAbonesi(SohbetHub sohbetMerkezi)
        {
            this.sohbetMerkezi = sohbetMerkezi;
        }

        public void TabloBaginaAboneOl()
        {
            tabloBagimi = new SqlTableDependency<VIleti>("Data Source=(localdb)\\MSSQLLocalDB; database=hasbiHAliCia; Integrated Security=true;");
            tabloBagimi.OnChanged += TabloDegisti;
            tabloBagimi.OnError += TabloHatasi;
            tabloBagimi.Start();
        }

        private void TabloHatasi(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine(nameof(VIleti) + " SqlDependency Hatası: " + e.Error.Message);
        }

        private void TabloDegisti(object sender, RecordChangedEventArgs<VIleti> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                sohbetMerkezi.IletileriAl();
            }
        }
    }
}
