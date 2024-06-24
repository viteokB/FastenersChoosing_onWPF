using FastenersChoosing.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastenersChoosing.Models.UnDetachableFasteners.GluedCalculations
{
    public class NeededForceCalculation : BaseViewModel, IDataErrorInfo
    {
        #region A

        private double _a;

        public double A
        {
            get => _a;
            set
            {
                Set(ref _a, value);
            }
        }

        private bool AHasValue = false;

        #endregion

        #region P

        private double _p;

        public double P
        {
            get => _p;
            set
            {
                Set(ref _p, value);
            }
        }

        private bool PHasValue = false;

        #endregion

        #region F

        private double _f;

        public double F
        {
            get => _f;
            set
            {
                Set(ref _f, value);
            }
        }

        private bool FHasValue = false;

        #endregion

        #region Свойства выбора RadioButton

        private bool _calcForce;
        public bool CalcForce
        {
            get => _calcForce;
            set
            {
                Set(ref _calcForce, value);
                Calculate = CalculateF;
            }
        }

        private bool _calcP;
        public bool CalcP
        {
            get => _calcP;
            set
            {
                Set(ref _calcP, value);
                Calculate = CalculateP;
            }
        }

        private bool _calcA;
        public bool CalcA
        {
            get => _calcA;
            set
            {
                Set(ref _calcA, value);
                Calculate = new CalculateDelegate(CalculateA);
            }
        }

        #endregion

        public NeededForceCalculation()
        {
            CalcForce = true;
        }

        #region Вычисления

        public delegate void CalculateDelegate();

        CalculateDelegate Calculate;

        private void CalculateP()
        {
            if (FHasValue && AHasValue)
            {
                Set(ref _p, Double.Round(F * A, 4), "P");
            }
        }

        private void CalculateF()
        {
            if (PHasValue && AHasValue)
            {
                Set(ref _f, Double.Round(P / A, 4), "F");
            }
        }

        private void CalculateA()
        {
            if (PHasValue && AHasValue)
            {
                Set(ref _a, Double.Round(P / F, 4), "A");
            }
        }

        #endregion

        #region IDataErrorRealization

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;

                switch (columnName)
                {
                    case "A":
                        CheckValidation(A, ref AHasValue, ref error);
                        break;
                    case "F":
                        CheckValidation(F, ref FHasValue, ref error);
                        break;
                    case "P":
                        CheckValidation(P, ref PHasValue, ref error);
                        break;
                }

                Calculate.Invoke();
                return error;
            }
        }

        private void CheckValidation(double doubleProperty, ref bool boolProperty, ref string errorMessage)
        {
            if (doubleProperty > 0)
            {
                boolProperty = true;
            }
            else
            {
                boolProperty = false;
                errorMessage = "Значение должно быть больше нуля";
            }
        }

        #endregion
    }
}
