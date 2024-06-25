using FastenersChoosing.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastenersChoosing.Models.UnDetachableFasteners.RivetedCalcultions
{
    public class LoadPerRivet : BaseViewModel, IDataErrorInfo
    {
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

        private bool _calcF_sht;
        public bool CalcF_sht
        {
            get => _calcF_sht;
            set
            {
                Set(ref _calcF_sht, value);
                Calculate = CalculateF_sht;
            }
        }

        private bool _calcZ;
        public bool CalcZ
        {
            get => _calcZ;
            set
            {
                Set(ref _calcZ, value);
                Calculate = new CalculateDelegate(CalculateZ);
            }
        }

        #endregion

        public LoadPerRivet()
        {
            CalcF_sht = true;
        }

        #region Вычисления

        public delegate void CalculateDelegate();

        CalculateDelegate Calculate;

        private void CalculateF()
        {
            if (F_shtHasValue && ZHasValue)
            {
                Set(ref _f, Double.Round(F_sht * Z, 4), "F");
            }
        }

        private void CalculateZ()
        {
            if (F_shtHasValue && FHasValue)
            {
                Set(ref _z, Double.Round(F / F_sht, 4), "Z");
            }
        }

        private void CalculateF_sht()
        {
            if (FHasValue && ZHasValue)
            {
                Set(ref _f_sht, Double.Round(F / Z, 4), "F_sht");
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
                    case "F_sht":
                        CheckValidation(F_sht, ref F_shtHasValue, ref error);
                        break;
                    case "F":
                        CheckValidation(F, ref FHasValue, ref error);
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
