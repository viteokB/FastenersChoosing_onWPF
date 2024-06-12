using FastenersChoosing.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastenersChoosing.ViewModels
{
    public class Parametr : BaseViewModel
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private List<string> _values;
        public List<string> ListValues
        {
            get => _values;
            set => Set(ref _values, value);
        }

        private string _selectedValue;
        public string SelectedValue
        {
            get => _selectedValue;
            set => Set(ref _selectedValue, value);
        }

        public Parametr(string name, List<string> values)
        {
            this.Name = name;
            this.ListValues = values;
            _selectedValue = null;
        }
    }
}
