using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FastenersChoosing.ViewModels
{
    public class UnDetachableVM : BaseViewModel
    {
		public WeldingViewModel WeldingViewModel { get; set; }

        private object currentView;

		public object CurrentView
		{
			get { return currentView; }
			set { Set(ref currentView, value); }
		}
        public UnDetachableVM()
        {
            WeldingViewModel = new();

            CurrentView = WeldingViewModel;
        }
    }
}
