using FastenersChoosing.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastenersChoosing.Models.UnDetachableFasteners.SolderingCalculations
{
    public class SoldeingNahl : BaseViewModel, IDataErrorInfo
    {
        #region F

        private double _f;

        public double F
        {
            get
            {
                return _f;
            }
            set
            {
                Set(ref _f, value);
            }
        }

        private bool FHasValue = false;

        #endregion

        #region T

        private double _t;

        public double T
        {
            get => _t;
            set
            {
                Set(ref _t, value);
            }
        }

        private bool THasValue = false;

        #endregion

        #region B

        private double _b;

        public double B
        {
            get => _b;
            set
            {
                Set(ref _b, value);
            }
        }

        private bool BHasValue = false;

        #endregion

        #region L

        private double _l;

        public double L
        {
            get => _l;
            set
            {
                Set(ref _l, value);
            }
        }

        private bool LHasValue = false;

        #endregion

        #region Свойства выбора RadioButton

        private bool _calcF;
        public bool CalcF
        {
            get => _calcF;
            set
            {
                Set(ref _calcF, value);
                Calculate = CalculateF;
            }
        }

        private bool _calcT;
        public bool CalcT
        {
            get => _calcT;
            set
            {
                Set(ref _calcT, value);
                Calculate = CalculateT;
            }
        }

        private bool _calcL;
        public bool CalcL
        {
            get => _calcL;
            set
            {
                Set(ref _calcL, value);
                Calculate = new CalculateDelegate(CalculateL);
            }
        }

        private bool _calcB;
        public bool CalcB
        {
            get => _calcB;
            set
            {
                Set(ref _calcB, value);
                Calculate = CalculateB;
            }
        }

        #endregion

        public SoldeingNahl()
        {
            CalcT = true;
        }

        #region Вычисления

        public delegate void CalculateDelegate();

        CalculateDelegate Calculate;

        private void CalculateT()
        {
            if (FHasValue && LHasValue && BHasValue)
            {
                Set(ref _t, Double.Round(F / (B * L), 4), "T");
            }
        }

        private void CalculateF()
        {
            if (THasValue && LHasValue && BHasValue)
            {
                Set(ref _f, Double.Round(T * L * B, 4), "F");
            }
        }

        private void CalculateB()
        {
            if (FHasValue && LHasValue && THasValue)
            {
                Set(ref _b, Double.Round(F / (T * L), 4), "B");
            }
        }

        private void CalculateL()
        {
            if (FHasValue && THasValue && BHasValue)
            {
                Set(ref _l, Double.Round(F / (B * T), 4), "L");
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
                    case "F":
                        CheckValidation(F, ref FHasValue, ref error);
                        break;
                    case "T":
                        CheckValidation(T, ref THasValue, ref error);
                        break;
                    case "B":
                        CheckValidation(B, ref BHasValue, ref error);
                        break;
                    case "L":
                        CheckValidation(L, ref LHasValue, ref error);
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
