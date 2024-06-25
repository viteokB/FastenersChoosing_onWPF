using FastenersChoosing.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastenersChoosing.Models.UnDetachableFasteners.RivetedCalcultions
{
    public class AreaOfCrumple : BaseViewModel, IDataErrorInfo
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

        #region D

        private double _d;

        public double D
        {
            get => _d;
            set
            {
                Set(ref _d, value);
            }
        }

        private bool DHasValue = false;

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

        private bool _calcA;
        public bool CalcA
        {
            get => _calcA;
            set
            {
                Set(ref _calcA, value);
                Calculate = CalculateA;
            }
        }

        private bool _calcD;
        public bool CalcD
        {
            get => _calcD;
            set
            {
                Set(ref _calcD, value);
                Calculate = CalculateD;
            }
        }

        private bool _calcS;
        public bool CalcS
        {
            get => _calcS;
            set
            {
                Set(ref _calcS, value);
                Calculate = new CalculateDelegate(CalculateS);
            }
        }

        #endregion

        public AreaOfCrumple()
        {
            CalcA = true;
        }

        #region Вычисления

        public delegate void CalculateDelegate();

        CalculateDelegate Calculate;

        private void CalculateA()
        {
            if (DHasValue && SHasValue)
            {
                Set(ref _a, Double.Round(D * S, 4), "A");
            }
        }

        private void CalculateD()
        {
            if (AHasValue && SHasValue)
            {
                Set(ref _d, Double.Round(A / S, 4), "D");
            }
        }

        private void CalculateS()
        {
            if (AHasValue && DHasValue)
            {
                Set(ref _s, Double.Round(A / D, 4), "S");
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
                    case "D":
                        CheckValidation(D, ref DHasValue, ref error);
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
