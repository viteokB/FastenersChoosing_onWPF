using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastenersChoosing.Models.UnDetachableFasteners.RivetedCalcultions
{
    class NeededRivets
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

        #region F_sht

        private double _f_sht;

        public double F_sht
        {
            get => _f_sht;
            set
            {
                Set(ref _f_sht, value);
            }
        }

        private bool F_shtHasValue = false;

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

        #endregion

        public RivetSrez()
        {
            CalcT = true;
        }

        #region Вычисления

        public delegate void CalculateDelegate();

        CalculateDelegate Calculate;

        private void CalculateT()
        {
            if (F_shtHasValue && DHasValue && ZHasValue)
            {
                Set(ref _t, Double.Round((4 * F_sht) / (Math.PI * D * D * Z), 4), "T");
            }
        }

        private void CalculateF()
        {
            if (THasValue && DHasValue && ZHasValue)
            {
                Set(ref _f_sht, Double.Round((T * Math.PI * D * D * Z) / 4, 4), "F_sht");
            }
        }

        private void CalculateD()
        {
            if (THasValue && F_shtHasValue && ZHasValue)
            {
                Set(ref _d, Double.Round(Math.Sqrt((4 * F_sht) / (Math.PI * T * Z)), 4), "D");
            }
        }

        private void CalculateZ()
        {
            if (THasValue && F_shtHasValue && DHasValue)
            {
                Set(ref _z, Double.Round(4 * F_sht / (Math.PI * D * D * T), 4), "Z");
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
                    case "F_sht":
                        CheckValidation(F_sht, ref F_shtHasValue, ref error);
                        break;
                    case "D":
                        CheckValidation(D, ref DHasValue, ref error);
                        break;
                    case "Z":
                        CheckValidation(Z, ref ZHasValue, ref error);
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
