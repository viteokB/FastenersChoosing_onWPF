using FastenersChoosing.Infrastructure.Commands;
using FastenersChoosing.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FastenersChoosing.Models.UnDetachableFasteners
{
    public class WeldingCalculations : BaseViewModel, IDataErrorInfo
    {
        #region Sigma

        private double _sigma;

        public double Sigma
        {
            get
            {
                return _sigma;
            }
            set
            {
                Set(ref _sigma, value);
            }
        }

        private bool SigmaHasValue = false;

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

        #region Square

        private double _square;

        public double Square
        {
            get => _square;
            set
            {
                Set(ref _square, value);
            }
        }

        private bool SquareHasValue = false;

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

        private bool _calcSquare;
        public bool CalcSquare
        {
            get => _calcSquare;
            set
            {
                Set(ref _calcSquare, value);
                Calculate = CalculateSquare;
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

        private bool _calcSigma;
        public bool CalcSigma
        {
            get => _calcSigma;
            set
            {
                Set(ref _calcSigma, value);
                Calculate = CalculateSigma;
            }
        }

        #endregion

        public WeldingCalculations()
        {
            CalcSigma = true;
        }

        #region Вычисления

        public delegate void CalculateDelegate();

        CalculateDelegate Calculate;

        private void CalculateSigma()
        {
            if (ForceHasValue && SquareHasValue && LengthHasValue)
            {
                Set(ref _sigma, Force / (Square * Length), "Sigma");
            }
        }

        private void CalculateForce()
        {
            if (SigmaHasValue && SquareHasValue && LengthHasValue)
            {
                Set(ref _force, Sigma * Square * Length, "Force");
            }
        }

        private void CalculateLength()
        {
            if (SigmaHasValue && ForceHasValue && SquareHasValue)
            {
                Set(ref _length, Force / (Sigma * Square), "Length");
            }
        }

        private void CalculateSquare()
        {
            if (SigmaHasValue && ForceHasValue && LengthHasValue)
            {
                Set(ref _square, Force / (Sigma * Length), "Square");
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
                    case "Square":
                        CheckValidation(Square, ref SquareHasValue, ref error);
                        break;
                    case "Force":
                        CheckValidation(Force,ref  ForceHasValue, ref error);
                        break;
                    case "Sigma":
                        CheckValidation(Sigma, ref  SigmaHasValue, ref error);
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
