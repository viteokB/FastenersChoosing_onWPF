using FastenersChoosing.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastenersChoosing.Models.UnDetachableFasteners.RivetedCalcultions
{
    public class SheetsRastCalc : BaseViewModel, IDataErrorInfo
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

        public SheetsRastCalc()
        {
            CalcS = true;
        }

        #region Вычисления

        public delegate void CalculateDelegate();

        CalculateDelegate Calculate;

        private void CalculateS()
        {
            if (FHasValue && QHasValue && BHasValue && ZHasValue && DHasValue)
            {
                Set(ref _s, Double.Round(F / (Q * (B - Z * D)), 4), "S");
            }
        }

        private void CalculateF()
        {
            if (SHasValue && QHasValue && BHasValue && ZHasValue && DHasValue)
            {
                Set(ref _f, Double.Round(S * Q * (B - Z * D), 4), "F");
            }
        }

        private void CalculateD()
        {
            if (SHasValue && QHasValue && BHasValue && ZHasValue && FHasValue)
            {
                Set(ref _d, Double.Round((B - F / (Q * S)) / Z, 4), "D");
            }
        }

        private void CalculateZ()
        {
            if (SHasValue && QHasValue && BHasValue && DHasValue && FHasValue)
            {
                Set(ref _z, Double.Round((B - F / (Q * S))/D, 4), "Z");
            }
        }

        private void CalculateQ()
        {
            if (SHasValue && DHasValue && BHasValue && ZHasValue && FHasValue)
            {
                Set(ref _q, Double.Round(F / (S * (B - Z * D)), 4), "Q");
            }
        }

        private void CalculateB()
        {
            if (SHasValue && QHasValue && DHasValue && ZHasValue && FHasValue)
            {
                Set(ref _b, Double.Round(F / (Q * S) + Z * D, 4), "B");
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
                    case "B":
                        CheckValidation(B, ref BHasValue, ref error);
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
