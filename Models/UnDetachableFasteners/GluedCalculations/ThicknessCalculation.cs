using FastenersChoosing.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastenersChoosing.Models.UnDetachableFasteners.GluedCalculations
{
    public class ThicknessCalculation : BaseViewModel, IDataErrorInfo
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

        #region V

        private double _v;

        public double V
        {
            get => _v;
            set
            {
                Set(ref _v, value);
            }
        }

        private bool VHasValue = false;

        #endregion

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

        #region Свойства выбора RadioButton

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

        private bool _calcV;
        public bool CalcV
        {
            get => _calcV;
            set
            {
                Set(ref _calcV, value);
                Calculate = CalculateV;
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

        #endregion

        public ThicknessCalculation()
        {
            CalcT = true;
        }

        #region Вычисления

        public delegate void CalculateDelegate();

        CalculateDelegate Calculate;

        private void CalculateT()
        {
            if (VHasValue && AHasValue && PHasValue)
            {
                Set(ref _t, Double.Round(V / (A * P), 4), "T");
            }
        }

        private void CalculateV()
        {
            if (THasValue && AHasValue && PHasValue)
            {
                Set(ref _v, Double.Round(T * A * P, 4), "V");
            }
        }

        private void CalculateA()
        {
            if (VHasValue && PHasValue && AHasValue)
            {
                Set(ref _a, Double.Round(V / (T * P), 4), "A");
            }
        }

        private void CalculateP()
        {
            if (VHasValue && AHasValue && THasValue)
            {
                Set(ref _p, Double.Round(V / (A * T), 4), "P");
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
                    case "T":
                        CheckValidation(T, ref THasValue, ref error);
                        break;
                    case "V":
                        CheckValidation(V, ref VHasValue, ref error);
                        break;
                    case "A":
                        CheckValidation(A, ref AHasValue, ref error);
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
