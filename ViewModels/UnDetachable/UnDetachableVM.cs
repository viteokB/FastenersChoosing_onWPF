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

        private WeldingViewModel WeldingViewModel = null;

        private GluedViewModel GluedViewModel = null;

        private RivitedViewModel RivitedViewModel = null;

        private SolderingViewModel SolderingViewModel = null;

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
                if(WeldingViewModel is null)
                    WeldingViewModel = new();
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
                if(GluedViewModel is null)
                    GluedViewModel = new();
                CurrentView = GluedViewModel;
            }
        }

        #endregion

        #region IsCheckedRivited

        private bool _isCheckedRivited;

        public bool IsCheckedRivited
        {
            get => _isCheckedRivited;
            set
            {
                Set(ref _isCheckedRivited, value);
                if(RivitedViewModel is null)
                    RivitedViewModel = new();
                CurrentView = RivitedViewModel;
            }
        }

        #endregion

        #region IsCheckedSoldering

        private bool _isCheckedSoldering;

        public bool IsCheckedSoldering
        {
            get => _isCheckedSoldering;
            set
            {
                Set(ref _isCheckedSoldering, value);
                if(SolderingViewModel is null)
                    SolderingViewModel = new();
                CurrentView = SolderingViewModel;
            }
        }

        #endregion

        #endregion

        public UnDetachableVM()
        {
            IsCheckedWelding = true;
        }
    }
}
