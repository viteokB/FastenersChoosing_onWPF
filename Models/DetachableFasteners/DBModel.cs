using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace FastenersChoosing.Models.DetachableFasteners
{
    public class DBModel
    {
        public static string connectChoose = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\code\\C#\\KompasAPI\\Realization\\FastenersChoosing\\Data\\ChooseGost.mdb";
        private OleDbConnection chooseDb;
        public static string connectGosts = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\code\\C#\\KompasAPI\\Realization\\FastenersChoosing\\Data\\GostData.mdb";
        private OleDbConnection gostsDb;

        public DBModel()
        {
            chooseDb = new OleDbConnection(connectChoose);
            chooseDb.Open();
            gostsDb = new OleDbConnection(connectGosts);
            gostsDb.Open();
        }

        public List<string> GetListFastenersNames()
        {
            List<string> fastenersNames = new List<string>();

            string query = "SELECT * FROM Fastener";
            OleDbCommand dbCommand = new OleDbCommand(query, chooseDb);
            OleDbDataReader dbReader = dbCommand.ExecuteReader();//Считываем данные

            if (dbReader.HasRows == false)
            {
                return null;
            }
            while (dbReader.Read())
            {
                fastenersNames.Add(dbReader["Fastener"].ToString());
            }

            return fastenersNames;
        }

        public List<string> GetListFastenersTypes(string fastenerName)
        {
            List<string> fastenerTypes = new List<string>();
            string query = "SELECT type FROM Fasteners_types WHERE Fastener = '" + fastenerName + "'";
            OleDbCommand dbCommand = new OleDbCommand(query, chooseDb);
            OleDbDataReader dbReader = dbCommand.ExecuteReader();//Считываем данные

            //Проверяем данные
            if (dbReader.HasRows == false)
            {
                MessageBox.Show("Данные не найдены");
            }
            while (dbReader.Read())
            {
                fastenerTypes.Add(dbReader["type"].ToString());
            }

            return fastenerTypes;
        }

        public List<string> GetListGostNumbers(string fastenerType)
        {
            List<string> gostNumbers = new List<string>();
            string query = "SELECT Gost FROM Fasteners_gosts WHERE type = '" + fastenerType + "'";
            OleDbCommand dbCommand = new OleDbCommand(query, chooseDb);
            OleDbDataReader dbReader = dbCommand.ExecuteReader();//Считываем данные

            if (dbReader.HasRows == false)
            {
                return null;
            }
            while (dbReader.Read())
            {
                gostNumbers.Add(dbReader["Gost"].ToString());
            }

            return gostNumbers;
        }


    }
}
