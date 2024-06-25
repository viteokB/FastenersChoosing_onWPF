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

        #region Команды

        #region SelectWeldingСommand

        public LambdaCommand SelectWeldingСommand { get; set; }

        private void SelectWeldingMethod(object obj)
        {
            if (WeldingViewModel is null)
                WeldingViewModel = new();
            CurrentView = WeldingViewModel;
        }

        #endregion

        #region SelectGlueingСommand
        public LambdaCommand SelectGlueingСommand { get; set; }

        private void SelectGlueingMethod(object obj)
        {
            if (GluedViewModel is null)
                GluedViewModel = new();
            CurrentView = GluedViewModel;
        }

        #endregion

        #region SelectRivetedСommand

        public LambdaCommand SelectRivetedСommand { get; set; }

        private void SelectRivetedMethod(object obj)
        {
            if (RivitedViewModel is null)
                RivitedViewModel = new();
            CurrentView = RivitedViewModel;
        }

        #endregion

        #region SelectSolderingСommand

        public LambdaCommand SelectSolderingСommand { get; set; }

        private void SelectSolderingMethod(object obj)
        {
            if (SolderingViewModel is null)
                SolderingViewModel = new();
            CurrentView = SolderingViewModel;
        }

        #endregion

        #endregion

        public UnDetachableVM()
        {
            SelectWeldingСommand = new LambdaCommand(SelectWeldingMethod);
            SelectGlueingСommand = new LambdaCommand(SelectGlueingMethod);
            SelectRivetedСommand = new LambdaCommand(SelectRivetedMethod);
            SelectSolderingСommand = new LambdaCommand(SelectSolderingMethod);

            SelectWeldingСommand.Execute(null);
        }
    }
}
