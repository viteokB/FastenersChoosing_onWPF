using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastenersChoosing.Models.DetachableFasteners
{
    public static partial class DBModel
    {
        public static List<string> GetListUnDetachable()
        {
            return GetListFromRequest(chooseDb, 
                "SELECT * FROM UnDetachableFasteners", 
                "Соединение");
        }

        public static List<string> GetListUnDetachableTypes(string unDetachName)
        {
            return GetListFromRequest(chooseDb,
                $"SELECT Тип_соединения " +
                $" FROM UnDetachableTypes INNER JOIN UnDetachableFasteners " +
                $" ON UnDetachableTypes.Код_Соединения = UnDetachableFasteners.Код " +
                $" WHERE UnDetachableFasteners.Соединение = '{unDetachName}';",
                "Тип");
        }

        public static string GetUnDetachDescription(string unDetachType)
        {
            return GetStringFromRequest(chooseDb,
                $" SELECT Описание " +
                $" FROM UnDetachableTypes" +
                $" WHERE UnDetachableTypes.Тип_соединения = '{unDetachType}';",
                "Описание");
        }

        public static List<List<string>> GetResourceAndSubtype(string unDetachType)
        {
            return GetLLStringFromRequest(chooseDb,
                $" SELECT Подтип_соединения, Ресурс" +
                $" FROM UnDetachableTypes INNER JOIN UnDetachableSubtypes " +
                $" ON UnDetachableTypes.Код = UnDetachableSubtypes.Код_типа " +
                $" WHERE UnDetachableTypes.Тип_соединения = '{unDetachType}';",
                "Подтип_соединения", "Ресурс");
        }

        public static string GetStringResource(string unDetachSubType)
        {
            return GetStringFromRequest(chooseDb,
                $"SELECT Ресурс FROM UnDetachableSubtypes WHERE Подтип_соединения = '{unDetachSubType}'",
                "Ресурс");
        }
    }
}
