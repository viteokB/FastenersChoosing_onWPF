using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Windows;

namespace FastenersChoosing.Models.DetachableFasteners
{
    public static class DBModel
    {
        private static string connectChoose = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\\Data\\ChooseGost.mdb";
        private static OleDbConnection chooseDb;
        private static string connectGosts = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\\Data\\GostData.mdb";
        private static OleDbConnection gostsDb;

        static DBModel()
        {
            chooseDb = new OleDbConnection(connectChoose);
            chooseDb.Open();
            gostsDb = new OleDbConnection(connectGosts);
            gostsDb.Open();
        }
        public static List<string> GetListFastenersNames()
        {
            return GetListFromRequest(chooseDb, "SELECT * FROM Fastener", "Fastener");
        }

        public static List<string> GetListFastenersTypes(string fastenerName)
        {
            return GetListFromRequest(chooseDb,
                $"SELECT type FROM Fasteners_types WHERE Fastener = '{fastenerName}'",
                "type");
        }

        public static List<string> GetListGostNumbers(string fastenerType)
        {
            return GetListFromRequest(chooseDb,
                $"SELECT Gost FROM Fasteners_gosts WHERE type = '{fastenerType}'",
                "Gost");
        }

        public static string GetStringDescription(string  fastenerType)
        {
            return GetStringFromRequest(chooseDb,
                $"SELECT Description FROM Fasteners_types WHERE type = '{fastenerType}'",
                "Description");
        }

        public static string GetStringImagePath(string fastenerGost)
        {
            return GetStringFromRequest(chooseDb,
                $"SELECT Path FROM Fasteners_gosts WHERE Gost = '{fastenerGost}'",
                "Path");
        }

        private static List<string> GetListFromRequest(OleDbConnection DB, string query, string readField)
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

        private static string GetStringFromRequest(OleDbConnection DB, string query, string readField)
        {
            OleDbCommand dbCommand = new OleDbCommand(query, DB);
            OleDbDataReader dbReader = dbCommand.ExecuteReader();//Считываем данные

            if (dbReader.HasRows == false || !dbReader.Read())
            {
                MessageBox.Show($"Ошибка получения данных найдены");
                return null;
            }

            return dbReader[readField].ToString();
        }
    }
}
