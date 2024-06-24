using System;
using System.Collections.Generic;
using System.ComponentModel;
using FastenersChoosing.ViewModels;

namespace FastenersChoosing.Models.UnDetachableFasteners.WeldingCalculations
{
    public class WeldingSrez : BaseViewModel, IDataErrorInfo
    {
        #region T

        private double _t;

        public double T
        {
            get
            {
                return _t;
            }
            set
            {
                Set(ref _t, value);
            }
        }

        private bool THasValue = false;

        #endregion

        #region Length

        private double _length;

        public double Length
        {
            get => _length;
            set
            {
                Set(ref _length, value);
            }
        }

        private bool LengthHasValue = false;

        #endregion

        #region K

        private double _k;

        public double K
        {
            get => _k;
            set
            {
                Set(ref _k, value);
            }
        }

        private bool KHasValue = false;

        #endregion

        #region Force

        private double _force;

        public double Force
        {
            get => _force;
            set
            {
                Set(ref _force, value);
            }
        }

        private bool ForceHasValue = false;

        #endregion

        #region Свойства выбора RadioButton

        private bool _calcForce;
        public bool CalcForce
        {
            get => _calcForce;
            set
            {
                Set(ref _calcForce, value);
                Calculate = CalculateForce;
            }
        }

        private bool _calcK;
        public bool CalcK
        {
            get => _calcK;
            set
            {
                Set(ref _calcK, value);
                Calculate = CalculateK;
            }
        }

        private bool _calcLength;
        public bool CalcLength
        {
            get => _calcLength;
            set
            {
                Set(ref _calcLength, value);
                Calculate = new CalculateDelegate(CalculateLength);
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

        #endregion

        public WeldingSrez()
        {
            CalcT = true;
        }

        #region Вычисления

        public delegate void CalculateDelegate();

        CalculateDelegate Calculate;

        private void CalculateT()
        {
            if (ForceHasValue && KHasValue && LengthHasValue)
            {
                Set(ref _t, Double.Round(Force / (0.7 * K * Length), 4), "T");
            }
        }

        private void CalculateForce()
        {
            if (THasValue && KHasValue && LengthHasValue)
            {
                Set(ref _force, Double.Round(T * 0.7 * K * Length, 4), "Force");
            }
        }

        private void CalculateLength()
        {
            if (KHasValue && ForceHasValue && THasValue)
            {
                Set(ref _length, Double.Round(Force / (0.7 * K * T), 4), "Length");
            }
        }

        private void CalculateK()
        {
            if (THasValue && ForceHasValue && LengthHasValue)
            {
                Set(ref _k, Double.Round(Force / (0.7 * T * Length), 4), "K");
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
                    case "Length":
                        CheckValidation(Length, ref LengthHasValue, ref error);
                        break;
                    case "K":
                        CheckValidation(K, ref KHasValue, ref error);
                        break;
                    case "Force":
                        CheckValidation(Force, ref ForceHasValue, ref error);
                        break;
                    case "T":
                        CheckValidation(T, ref THasValue, ref error);
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
