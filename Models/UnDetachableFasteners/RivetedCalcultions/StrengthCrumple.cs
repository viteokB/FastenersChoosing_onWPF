using FastenersChoosing.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastenersChoosing.Models.UnDetachableFasteners.RivetedCalcultions
{
    public class StrengthCrumple : BaseViewModel, IDataErrorInfo
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

        #region S

        private double _s;

        public double S
        {
            get
            {
                return _s;
            }
            set
            {
                Set(ref _s, value);
            }
        }

        private bool SHasValue = false;

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

        #region Z

        private double _z;

        public double Z
        {
            get => _z;
            set
            {
                Set(ref _z, value);
            }
        }

        private bool ZHasValue = false;

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

        private bool _calcD;
        public bool CalcD
        {
            get => _calcD;
            set
            {
                Set(ref _calcD, value);
                Calculate = new CalculateDelegate(CalculateD);
            }
        }

        private bool _calcZ;
        public bool CalcZ
        {
            get => _calcZ;
            set
            {
                Set(ref _calcZ, value);
                Calculate = CalculateZ;
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

        public StrengthCrumple()
        {
            CalcS = true;
        }

        #region Вычисления

        public delegate void CalculateDelegate();

        CalculateDelegate Calculate;

        private void CalculateS()
        {
            if (FHasValue && DHasValue && ZHasValue && QHasValue)
            {
                Set(ref _s, Double.Round(F / (Z * D * Q), 4), "S");
            }
        }

        private void CalculateF()
        {
            if (SHasValue && DHasValue && ZHasValue && QHasValue)
            {
                Set(ref _f, Double.Round(S * Z * D * Q, 4), "F");
            }
        }

        private void CalculateD()
        {
            if (SHasValue && FHasValue && ZHasValue && QHasValue)
            {
                Set(ref _d, Double.Round(F / (S * Z * Q), 4), "D");
            }
        }

        private void CalculateZ()
        {
            if (SHasValue && FHasValue && DHasValue && QHasValue)
            {
                Set(ref _z, Double.Round(F / (S * D  * Q), 4), "Z");
            }
        }

        private void CalculateQ()
        {
            if (FHasValue && SHasValue && ZHasValue && DHasValue)
            {
                Set(ref _q, Double.Round(F / (S * Z * D), 4), "Q");
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
                    case "Q":
                        CheckValidation(Q, ref QHasValue, ref error);
                        break;
                    case "F":
                        CheckValidation(F, ref FHasValue, ref error);
                        break;
                    case "D":
                        CheckValidation(D, ref DHasValue, ref error);
                        break;
                    case "Z":
                        CheckValidation(Z, ref ZHasValue, ref error);
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
