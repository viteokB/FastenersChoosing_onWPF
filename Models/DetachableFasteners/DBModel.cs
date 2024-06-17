using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Windows;

namespace FastenersChoosing.Models.DetachableFasteners
{
    /// <summary>
    /// Singletone-класс получения запросов из базы данных
    /// </summary>
    public static class DBModel
    {
        /// <summary>
        /// Cтрока подключения базы данных выбора изделий
        /// </summary>
        private static string connectChoose = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\\Data\\ChooseGost.mdb";
        private static OleDbConnection chooseDb;

        /// <summary>
        /// Cтрока подключения к базе данных с значениями параметров изделий
        /// </summary>
        private static string connectGosts = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\\Data\\GostData.mdb";
        private static OleDbConnection gostsDb;

        static DBModel()
        {
            chooseDb = new OleDbConnection(connectChoose);
            chooseDb.Open();
            gostsDb = new OleDbConnection(connectGosts);
            gostsDb.Open();
        }

        /// <summary>
        /// Метод получения имен изделий из БД
        /// </summary>
        /// <returns>Список всех имен изделия</returns>
        public static List<string> GetListFastenersNames()
        {
            return GetListFromRequest(chooseDb, "SELECT * FROM DetachableFasteners", "Название");
        }

        /// <summary>
        /// Метод возвращий список типов изделия для данного имени
        /// </summary>
        /// <param name="fastenerName">Имя изделия</param>
        /// <returns>Список типов изделия для переданного имени</returns>
        public static List<string> GetListFastenersTypes(string fastenerName)
        {
            return GetListFromRequest(chooseDb,
                $"SELECT Тип " +
                $" FROM DetachableTypes INNER JOIN DetachableFasteners " +
                $" ON DetachableTypes.Код_имени = DetachableFasteners.Код " +
                $" WHERE DetachableFasteners.Название = '{fastenerName}';",
                "Тип");
        }

        /// <summary>
        /// Метод возвращий список ГОСТов изделия для данного типа изделия
        /// </summary>
        /// <param name="fastenerName">Тип изделия</param>
        /// <returns>Список ГОСТов изделия для переданного типа</returns>
        public static List<string> GetListGostNumbers(string fastenerType)
        {
            return GetListFromRequest(chooseDb,
                $" SELECT ГОСТ " +
                $" FROM DetachableTypes INNER JOIN DetachableGosts " +
                $" ON DetachableTypes.Код = DetachableGosts.Код_типа " +
                $" WHERE DetachableTypes.Тип = '{fastenerType}';",
                "ГОСТ");
        }

        /// <summary>
        /// Возвращает строку описания изделия
        /// </summary>
        /// <param name="fastenerType">Тип изделия</param>
        /// <returns>Строку описания</returns>
        public static string GetStringDescription(string fastenerType)
        {
            return GetStringFromRequest(chooseDb,
                $" SELECT Описание " +
                $" FROM DetachableTypes" +
                $" WHERE DetachableTypes.Тип = '{fastenerType}';",
                "Описание");
        }

        /// <summary>
        /// Метод по получению строки-пути до изображения
        /// </summary>
        /// <param name="fastenerGost">Строка-название ГОСТа</param>
        /// <returns>Строку с относительным путем изображения</returns>
        public static string GetStringImagePath(string fastenerGost)
        {
            return GetStringFromRequest(chooseDb,
                $"SELECT Ресурс FROM DetachableGosts WHERE ГОСТ = '{fastenerGost}'",
                "Ресурс");
        }

        /// <summary>
        /// Возвращает список списков отн. пути до изображения и ГОСТов для типа переданного изделия
        /// </summary>
        /// <param name="fastenerType">Строка названия типа переданного изделия</param>типа переданного изделия
        /// <returns>Список списков отн. пути до изображения и ГОСТов</returns>
        public static List<List<string>> GetPathAndGost(string fastenerType)
        {
            return GetLLStringFromRequest(chooseDb,
                $" SELECT ГОСТ, Ресурс" +
                $" FROM DetachableTypes INNER JOIN DetachableGosts " +
                $" ON DetachableTypes.Код = DetachableGosts.Код_типа " +
                $" WHERE DetachableTypes.Тип = '{fastenerType}';",
                "ГОСТ", "Ресурс");
        }

        /// <summary>
        /// Метод модификации возможных параметров изделия, для уже выбранных значений параметров
        /// </summary>
        /// <param name="fromGost">Название таблицы-ГОСТа</param>
        /// <param name="listParametrs">Список параметров</param>
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

        /// <summary>
        /// Создание последней части строки-запроса, с условиями FROM и WHERE
        /// </summary>
        /// <param name="fromGost">Название таблицы-ГОСТа</param>
        /// <param name="listParametrs">Список параметров</param>
        /// <returns>Последняя часть строки-запроса, с условиями FROM и WHERE</returns>
        private static string QuerryStringWhere(string fromGost, List<Parametr> parametrList)
        {
            StringBuilder query = new StringBuilder($" FROM [{fromGost}] WHERE ");

            foreach (Parametr parametr in parametrList)
            {
                if (!String.IsNullOrEmpty(parametr.SelectedValue))
                    query.Append($" [{parametr.Name}] = {parametr.SelectedValue.Replace(',', '.')} AND ");
            }

            query.Remove(query.Length - 5, 5);

            query.Append(';');

            return query.ToString();
        }

        /// <summary>
        /// Метод создает список всех парметров изделия, со всевозможными их значениями
        /// </summary>
        /// <param name="fastenerGost">Таблица-ГОСТ</param>
        /// <returns>Список всех парметров для изделия</returns>
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
                query = $"SELECT distinct [{fieldName}] FROM [{fastenerGost}]";
                List<string> PossibleValues = GetListFromRequest(gostsDb, query, fieldName);

                paramValues.Add(new Parametr(fieldName, PossibleValues));
            }

            return paramValues;
        }

        /// <summary>
        /// Метод проверки существования таблциы
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="tableName"></param>
        /// <returns>True если существует, иначе false</returns>
        private static bool TableExists(OleDbConnection connection, string tableName)
        {
            DataTable schema = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, tableName, "TABLE" });
            return schema.Rows.Count > 0;
        }

        /// <summary>
        /// Метод возвращающий список названий строк из заголовока таблицы, всех кроме "Код"
        /// </summary>
        /// <param name="oleDbDataReader"></param>
        /// <returns>Список названий строк из заголовока таблицы</returns>
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

        /// <summary>
        /// Метод для получения списка строк из переданной базы данных, по переданной строке запроса, и полем для чтения
        /// </summary>
        /// <param name="DB">Переданная база данных</param>
        /// <param name="query">Строка запроса</param>
        /// <param name="readField">Поле для чтения</param>
        /// <returns>Список строк в результате выполнения запроса</returns>
        private static List<string> GetListFromRequest(OleDbConnection DB, string query, string readField)
        {
            List<string> resultList = new List<string>();

            OleDbCommand dbCommand = new OleDbCommand(query, DB);
            OleDbDataReader dbReader = dbCommand.ExecuteReader();//Считываем данные

            if (CheckHasRows(dbReader))
            {
                while (dbReader.Read())
                {
                    resultList.Add(dbReader[readField].ToString());
                }
            }

            return resultList;
        }

        /// <summary>
        /// Метод для получения строки из переданной базы данных, по переданной строке запроса, и полем для чтения
        /// </summary>
        /// <param name="DB">Переданная база данных</param>
        /// <param name="query">Строка запроса</param>
        /// <param name="readField">Поле для чтения</param>
        /// <returns>Строка в результате выполнения запроса</returns>
        private static string GetStringFromRequest(OleDbConnection DB, string query, string readField)
        {
            OleDbCommand dbCommand = new OleDbCommand(query, DB);
            OleDbDataReader dbReader = dbCommand.ExecuteReader();//Считываем данные

            if (!CheckHasRows(dbReader))
            {
                return null;
            }

            dbReader.Read();
            return dbReader[readField].ToString();
        }

        /// <summary>
        /// Метод для получения списка списков строк из переданной базы данных, 
        /// по переданной строке запроса, и полем для чтения
        /// </summary>
        /// <param name="DB">Переданная база данных</param>
        /// <param name="query">Строка запроса</param>
        /// <param name="readField">Поля для чтения</param>
        /// <returns>Список списков строк в результате выполнения запроса</returns>
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

        /// <summary>
        /// Метод инициализации списка списков строк списками)
        /// </summary>
        /// <param name="list">Список списков строкк</param>
        /// <param name="count">Количество инициализируемых списков</param>
        private static void FillListListsString(ref List<List<string>> list, int count)
        {
            for (int i = 0; i < count; i++)
            {
                list.Add(new List<string>());
            }
        }

        /// <summary>
        /// Метод проверки наличия строк из полученного запроса к БД
        /// </summary>
        /// <param name="dbDataReader"></param>
        /// <returns>True если строки имеются, иначе false</returns>
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
