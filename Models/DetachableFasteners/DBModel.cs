using FastenersChoosing.ViewModels;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO.Packaging;
using System.Reflection.PortableExecutable;
using System.Text;
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

        public static string GetStringDescription(string fastenerType)
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

        public static List<List<string>> GetPathAndGost(string fastenerType)
        {
            return GetLLStringFromRequest(chooseDb,
                $"SELECT Path, Gost FROM Fasteners_gosts WHERE type = '{fastenerType}'",
                "Gost", "Path");
        }

        public static void ModifyListParamsWhere(string fromGost, List<Parametr> listParametrs)
        {
            if (!TableExists(gostsDb, fromGost))
            {
                MessageBox.Show($"Не существует таблицы \"{fromGost}\"");
                return;
            }

            string querryLastPart = QuerryStringWhere(fromGost, listParametrs);

            foreach (Parametr parametr in listParametrs)
            {
                string querry = $"SELECT distinct [{parametr.Name}] " + querryLastPart;
                parametr.ListValues = GetListFromRequest(gostsDb, querry, parametr.Name);
            }
        }

        private static string QuerryStringWhere(string fromGost, List<Parametr>  parametrList)
        {
            StringBuilder query = new StringBuilder($" FROM [{fromGost}] WHERE ");

            foreach (Parametr parametr in parametrList)
            {
                if(!String.IsNullOrEmpty(parametr.SelectedValue))
                    query.Append($" [{parametr.Name}] = {parametr.SelectedValue.Replace(',', '.')} AND ");
            }

            query.Remove(query.Length - 5, 5);

            query.Append(';');

            return query.ToString();
        }

        public static List<Parametr> GetGostParametrs(string fastenerGost)
        {
            if (!TableExists(gostsDb, fastenerGost))
            {
                MessageBox.Show($"Не существует таблицы \"{fastenerGost}\"");
                return null;
            }

            List<Parametr> paramValues = new();
            string query = $"SELECT TOP 1 * FROM [{fastenerGost}]";
            OleDbCommand dbCommand = new OleDbCommand(query, gostsDb);
            OleDbDataReader dbReader = dbCommand.ExecuteReader();//Считываем данные

            List<string> paramNames = GetListHeaderParams(dbReader);

            foreach (string fieldName in paramNames)
            {
                string querryString = $"SELECT distinct [{fieldName}] FROM [{fastenerGost}]";
                List<string> PossibleValues = GetListFromRequest(gostsDb, querryString, fieldName);

                paramValues.Add(new Parametr(fieldName, PossibleValues));
            }

            return paramValues;
        }

        private static bool TableExists(OleDbConnection connection, string tableName)
        {
            DataTable schema = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, tableName, "TABLE" });
            return schema.Rows.Count > 0;
        }

        private static List<string> GetListHeaderParams(OleDbDataReader oleDbDataReader)
        {
            List<string> result = new List<string>();

            if (CheckHasRows(oleDbDataReader))
            {
                int fieldCount = oleDbDataReader.FieldCount;
                string curField = null;
                for (int i = 0; i < fieldCount; i++)
                {
                    curField = oleDbDataReader.GetName(i);
                    if (curField != "Код")
                    {
                        result.Add(curField);
                    }
                }
            }

            return result;
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

        private static List<List<string>> GetLLStringFromRequest(OleDbConnection DB, string query, params string[] readField)
        {
            if (readField.Length == 1)
                throw new Exception("Бери другой метод - GetListFromRequest!");

            List<List<string>> resultList = new List<List<string>>();
            FillListListsString(ref resultList, readField.Length);

            OleDbCommand dbCommand = new OleDbCommand(query, DB);
            OleDbDataReader dbReader = dbCommand.ExecuteReader();//Считываем данные

            if (CheckHasRows(dbReader))
            {
                while (dbReader.Read())
                {
                    for (int i = 0; i < readField.Length; i++)
                        resultList[i].Add(dbReader[readField[i]].ToString());
                }
            }

            return resultList;
        }

        private static void FillListListsString(ref List<List<string>> list, int count)
        {
            for (int i = 0; i < count; i++)
            {
                list.Add(new List<string>());
            }
        }

        private static bool CheckHasRows(OleDbDataReader dbDataReader)
        {
            if (dbDataReader.HasRows == false)
            {
                MessageBox.Show($"Ошибка получения данных найдены");
                return false;
            }

            return true;
        }

    }
}
