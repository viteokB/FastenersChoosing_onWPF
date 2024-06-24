using FastenersChoosing.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FastenersChoosing.ViewModels.UnDetachable
{
    public class UnDetachableVM : BaseViewModel
    {
        #region Приватные UsersControl

        private WeldingViewModel WeldingViewModel;

        private GluedViewModel GluedViewModel;

        #endregion

        #region CurrentView

        private object currentView;

		public object CurrentView
		{
			get { return currentView; }
			set { Set(ref currentView, value); }
		}

        #endregion

        #region IsChecked

        #region IsCheckedWelding

        private bool _isCheckedWelding;

        public bool IsCheckedWelding 
        {
            get => _isCheckedWelding;
            set
            {
                Set(ref _isCheckedWelding, value);
                CurrentView = WeldingViewModel;
            }
        }


        #endregion

        #region IsCheckedGlueing

        private bool _isCheckedGlueing;

        public bool IsCheckedGlueing
        {
            get => _isCheckedGlueing;
            set
            {
                Set(ref _isCheckedGlueing, value);
                CurrentView = GluedViewModel;
            }
        }

        #endregion

        #endregion

        public UnDetachableVM()
        {
            WeldingViewModel = new();
            GluedViewModel = new();

            IsCheckedWelding = true;
        }
    }
}
