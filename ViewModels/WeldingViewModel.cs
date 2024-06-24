using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using FastenersChoosing.Infrastructure.Commands;
using FastenersChoosing.Models.DetachableFasteners;
using FastenersChoosing.Models.UnDetachableFasteners;
using FastenersChoosing.Models.UnDetachableFasteners.WeldingCalculations;

namespace FastenersChoosing.ViewModels
{
    public class WeldingViewModel : BaseViewModel
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

        #region SubTypeName
        private string _subTypeName;

        public string SubTypeName
        {
            get { return _subTypeName; }
            set { Set(ref _subTypeName, value); }
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

        #region ListTypes

        private List<string> _listTypes;

        public List<string> ListTypes
        {
            get { return _listTypes; }
            set { Set(ref _listTypes, value); }
        }

        #endregion

        #region ListSubTypes

        private List<string> _listSubTypes;

        public List<string> ListSubTypes
        {
            get { return _listSubTypes; }
            set { Set(ref _listSubTypes, value); }
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

        public WeldingSigma WeldingSigma { get; set; }

        public WeldingSrez WeldingSrez { get; set; }

        #endregion

        #region Команды

        #region SelectedTypeCommand
        public LambdaCommand SelectedTypeCommand { get; set; }

        public void SelectedTypeMethod(object obj)
        {
            if (!String.IsNullOrEmpty(TypeName))
            {
                ListSubTypes = DBModel.GetListSubtype(TypeName);
                Description = DBModel.GetUnDetachDescription(TypeName);
            }
        }

        #endregion

        #region SelectedSubTypeCommand
        public LambdaCommand SelectedSubTypeCommand { get; set; }

        public void SelectedSubTypeMethod(object obj)
        {
            if(!String.IsNullOrEmpty(SubTypeName))
            {
                Image = SetImage(DBModel.GetStringResource(SubTypeName));
            }
        }

        #endregion

        #endregion

        public WeldingViewModel()
        {
            Image = SetImage(_standartPath);

            ListTypes = DBModel.GetListUnDetachableTypes("Сварное");

            SelectedTypeCommand = new(SelectedTypeMethod);

            SelectedSubTypeCommand = new(SelectedSubTypeMethod);

            WeldingSigma = new();

            WeldingSrez = new();
        }
    }
}
