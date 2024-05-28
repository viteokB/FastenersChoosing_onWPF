using FastenersChoosing.Models.DetachableFasteners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastenersChoosing.ViewModels
{
    public class ComboBoxesVM
    {
        private DBModel _DBModel = new DBModel();

        public List<string> AllNames { get; set; }

        public List<string> NamesTypes;

        public List<string> Types;

        public ComboBoxesVM()
        {
            AllNames = _DBModel.GetListFastenersNames();


        }
    }
}
