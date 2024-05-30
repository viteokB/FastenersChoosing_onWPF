using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Windows;

namespace FastenersChoosing.Models.DetachableFasteners
{
    public class DBModel
    {
        public static string connectChoose = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\\Data\\ChooseGost.mdb";
        private OleDbConnection chooseDb;
        public static string connectGosts = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\\Data\\GostData.mdb";
        private OleDbConnection gostsDb;

        public DBModel()
        {
            chooseDb = new OleDbConnection(connectChoose);
            chooseDb.Open();
            gostsDb = new OleDbConnection(connectGosts);
            gostsDb.Open();
        }

        private List<string> GetListFromRequest(OleDbConnection DB, string query, string readField)
        {
            List<string> resultList = new List<string>();

            OleDbCommand dbCommand = new OleDbCommand(query, DB);
            OleDbDataReader dbReader = dbCommand.ExecuteReader();//Считываем данные

            if (dbReader.HasRows == false)
            {
                MessageBox.Show($"Ошибка получения данных найдены");
                return null;
            }
            while (dbReader.Read())
            {
                resultList.Add(dbReader[readField].ToString());
            }

            return resultList;
        }

        public List<string> GetListFastenersNames()
        {
            return GetListFromRequest(chooseDb, "SELECT * FROM Fastener", "Fastener");
        }

        public List<string> GetListFastenersTypes(string fastenerName)
        {
            return GetListFromRequest(chooseDb, 
                $"SELECT type FROM Fasteners_types WHERE Fastener = '{fastenerName}'",
                "type");
        }

        public List<string> GetListGostNumbers(string fastenerType)
        {
            return GetListFromRequest(chooseDb, 
                $"SELECT Gost FROM Fasteners_gosts WHERE type = '{fastenerType}'",
                "Gost");
        }


    }
}
