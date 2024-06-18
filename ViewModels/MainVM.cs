using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastenersChoosing.ViewModels
{
    public class MainVM: BaseViewModel
    {
        #region Свойства

        public DetachableVM DetachableVM { get; set; }

        public UnDetachableVM UnDetachableVM { get; set; }

        #endregion

        public MainVM()
        {
            DetachableVM = new();
            
            UnDetachableVM = new();
        }
    }
}
