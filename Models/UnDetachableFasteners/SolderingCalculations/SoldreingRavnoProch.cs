using FastenersChoosing.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastenersChoosing.Models.UnDetachableFasteners.SolderingCalculations
{
    public class SoldeingRavnoProch : BaseViewModel, IDataErrorInfo
    {
        #region Q

        private double _q;

        public double Q
        {
            get
            {
                return _q;
            }
            set
            {
                Set(ref _q, value);
            }
        }

        private bool QHasValue = false;

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

        #region S

        private double _s;

        public double S
        {
            get => _s;
            set
            {
                Set(ref _s, value);
            }
        }

        private bool SHasValue = false;

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

        private bool _calcS;
        public bool CalcS
        {
            get => _calcS;
            set
            {
                Set(ref _calcS, value);
                Calculate = CalculateS;
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

        private bool _calcQ;
        public bool CalcQ
        {
            get => _calcQ;
            set
            {
                Set(ref _calcQ, value);
                Calculate = CalculateQ;
            }
        }

        #endregion

        public SoldeingRavnoProch()
        {
            CalcL = true;
        }

        #region Вычисления

        public delegate void CalculateDelegate();

        CalculateDelegate Calculate;

        private void CalculateT()
        {
            if (QHasValue && LHasValue && SHasValue)
            {
                Set(ref _t, Double.Round(S * Q / L, 4), "T");
            }
        }

        private void CalculateQ()
        {
            if (THasValue && LHasValue && SHasValue)
            {
                Set(ref _q, Double.Round(T * L / S, 4), "Q");
            }
        }

        private void CalculateS()
        {
            if (LHasValue && QHasValue && THasValue)
            {
                Set(ref _s, Double.Round(L * T / Q, 4), "S");
            }
        }

        private void CalculateL()
        {
            if (SHasValue && QHasValue && THasValue)
            {
                Set(ref _l, Double.Round(S * Q / T, 4), "L");
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
                    case "S":
                        CheckValidation(S, ref SHasValue, ref error);
                        break;
                    case "T":
                        CheckValidation(T, ref THasValue, ref error);
                        break;
                    case "Q":
                        CheckValidation(Q, ref QHasValue, ref error);
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
