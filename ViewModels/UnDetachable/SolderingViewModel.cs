using FastenersChoosing.Infrastructure.Commands;
using FastenersChoosing.Models.DetachableFasteners;
using FastenersChoosing.Models.UnDetachableFasteners.WeldingCalculations;
using FastenersChoosing.Models.UnDetachableFasteners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FastenersChoosing.ViewModels.UnDetachable
{
    public class SolderingViewModel : BaseViewModel
    {
        #region Свойства

        #region TypeName
        private string _typeName;

        public string TypeName
        {
            get { return _typeName; }
            set { Set(ref _typeName, value); }
        }

        #endregion

        #region ListTypes

        private List<string> _listTypes;

        public List<string> ListTypes
        {
            get { return _listTypes; }
            set { Set(ref _listTypes, value); }
        }

        #endregion

        #region Description

        private string _decription;

        public string Description
        {
            get { return _decription; }
            set { Set(ref _decription, value); }
        }

        #endregion

        #region Image

        private BitmapImage _image;

        public BitmapImage Image
        {
            get { return _image; }
            set { Set(ref _image, value); }
        }


        #endregion

        #region Строка-путь к стандартному изображению

        private string _standartPath = @"\Data\Photos\NULL_INPUT.png";

        #endregion

        #region Расчеты

        public WeldingSigma WeldingSigma { get; set; }

        public WeldingSrez WeldingSrez { get; set; }

        #endregion

        #endregion

        #region Команды

        #region SelectedTypeCommand
        public LambdaCommand SelectedTypeCommand { get; set; }

        public void SelectedTypeMethod(object obj)
        {
            if (!String.IsNullOrEmpty(TypeName))
            {
                Description = DBModel.GetUnDetachDescription(TypeName);
                Image = SetImage(DBModel.GetStringResource(TypeName));
            }
        }

        #endregion

        #endregion

        public SolderingViewModel()
        {
            Image = SetImage(_standartPath);

            ListTypes = DBModel.GetListUnDetachableTypes("Паяное");

            SelectedTypeCommand = new(SelectedTypeMethod);

            WeldingSigma = new();

            WeldingSrez = new();
        }
    }
}
