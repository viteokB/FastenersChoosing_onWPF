using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace FastenersChoosing.Models.DetachableFasteners
{
    public class DetachableModel
    {
        public static string connectChoose = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = chooseGost.mdb";
        private OleDbConnection chooseDb;
        public static string connectGosts = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Gosts.mdb";
        private OleDbConnection gostsDb;

        public DetachableModel()
        {
            chooseDb = new OleDbConnection(connectChoose);
            gostsDb = new OleDbConnection(connectGosts);
        }

    }
}
