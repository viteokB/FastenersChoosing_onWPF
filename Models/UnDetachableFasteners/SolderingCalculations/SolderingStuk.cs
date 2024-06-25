using FastenersChoosing.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastenersChoosing.Models.UnDetachableFasteners.SolderingCalculations
{
    public class SolderingStuk : BaseViewModel, IDataErrorInfo
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

        #region Q

        private double _q;

        public double Q
        {
            get => _q;
            set
            {
                Set(ref _q, value);
            }
        }

        private bool QHasValue = false;

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

        private bool _calcQ;
        public bool CalcQ
        {
            get => _calcQ;
            set
            {
                Set(ref _calcQ, value);
                Calculate = new CalculateDelegate(CalculateQ);
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

        public SolderingStuk()
        {
            CalcS = true;
        }

        #region Вычисления

        public delegate void CalculateDelegate();

        CalculateDelegate Calculate;

        private void CalculateS()
        {
            if (FHasValue && QHasValue && BHasValue)
            {
                Set(ref _s, Double.Round(F / (Q * B), 4), "S");
            }
        }

        private void CalculateF()
        {
            if (SHasValue && QHasValue && BHasValue)
            {
                Set(ref _f, Double.Round(S * Q * B, 4), "F");
            }
        }

        private void CalculateQ()
        {
            if (FHasValue && SHasValue && BHasValue)
            {
                Set(ref _q, Double.Round(F / (S * B), 4), "Q");
            }
        }

        private void CalculateB()
        {
            if (FHasValue && QHasValue && SHasValue)
            {
                Set(ref _b, Double.Round(F / (Q * S), 4), "B");
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
                    case "Q":
                        CheckValidation(Q, ref QHasValue, ref error);
                        break;
                    case "B":
                        CheckValidation(B, ref BHasValue, ref error);
                        break;
                    case "S":
                        CheckValidation(S, ref SHasValue, ref error);
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
