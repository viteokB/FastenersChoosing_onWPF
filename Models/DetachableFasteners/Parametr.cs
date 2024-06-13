using FastenersChoosing.Infrastructure.Commands;
using FastenersChoosing.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastenersChoosing.Models.DetachableFasteners
{
    /// <summary>
    /// Класс представляющий отдельный парметр изделия
    /// </summary>
    public class Parametr : BaseViewModel
    {
        #region Name

        private string _name;

        /// <summary>
        /// Представляет собой название параметра изделия
        /// </summary>
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        #endregion

        #region ListValues

        private List<string> _values;

        /// <summary>
        /// Список возможных значений параметра
        /// </summary>
        public List<string> ListValues
        {
            get => _values;
            set => Set(ref _values, value);
        }

        #endregion

        #region SelectedValue

        private string _selectedValue;

        /// <summary>
        /// Выбранное значение параметра
        /// </summary>
        public string SelectedValue
        {
            get => _selectedValue;
            set => Set(ref _selectedValue, value);
        }

        #endregion

        /// <summary>
        /// Инициализирует параметр изделия, где name - имя изделия;
        /// values - список возможных параметров изделия
        /// </summary>
        /// <param name="name">имя изделия</param>
        /// <param name="values">список возможных параметров изделия</param>
        public Parametr(string name, List<string> values)
        {
            Name = name;
            ListValues = values;
            _selectedValue = null;
        }
    }
}
