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
using System.ComponentModel;
using FastenersChoosing.Models.UnDetachableFasteners.RivetedCalcultions;

namespace FastenersChoosing.ViewModels.UnDetachable
{
    public class RivitedViewModel : BaseViewModel
    {
        #region Свойства

        #region ConnectionCharacter

        private string _connectionCharacter;

        public string ConnectionCharacter
        {
            get { return _connectionCharacter; }
            set { Set(ref _connectionCharacter, value); }
        }

        #endregion

        #region ConnectionLocation

        private string _connectionLocation;

        public string ConnectionLocation
        {
            get { return _connectionLocation; }
            set { Set(ref _connectionLocation, value); }
        }

        #endregion

        #region ConnectionClassification

        private string _connectionClassification;

        public string ConnectionClassification
        {
            get { return _connectionClassification; }
            set { Set(ref _connectionClassification, value); }
        }

        #endregion

        #region ListCharacter

        private List<string> _listCharacter;

        public List<string> ListCharacter
        {
            get { return _listCharacter; }
            set { Set(ref _listCharacter, value); }
        }

        #endregion

        #region ListLocation

        private List<string> _listLocation;

        public List<string> ListLocation
        {
            get { return _listLocation; }
            set { Set(ref _listLocation, value); }
        }

        #endregion

        #region ListClassification

        private List<string> _listClassification;

        public List<string> ListClassification
        {
            get { return _listClassification; }
            set { Set(ref _listClassification, value); }
        }

        #endregion

        #region CharacterImage

        private BitmapImage _characterImage;

        public BitmapImage CharacterImage
        {
            get { return _characterImage; }
            set { Set(ref _characterImage, value); }
        }


        #endregion

        #region LocationImage

        private BitmapImage _locationImage;

        public BitmapImage LocationImage
        {
            get { return _locationImage; }
            set { Set(ref _locationImage, value); }
        }


        #endregion

        #region ClassificationImage

        private string _classificationDescription;

        public string ClassificationDescription
        {
            get { return _classificationDescription; }
            set { Set(ref _classificationDescription, value); }
        }

        #endregion

        #region Расчеты

        public LoadPerRivet LoadPerRivet { get; set; }

        public RivetSrez RivetSrez { get; set; }

        public NeededRivetsSrez NeededRivetsSrez { get; set; }

        #endregion

        #endregion

        #region Команды

        #region SelectedCharacterCommand
        public LambdaCommand SelectedCharacterCommand { get; set; }

        public void SelectedCharacterMethod(object obj)
        {
            if (!String.IsNullOrEmpty(ConnectionCharacter))
            {
                CharacterImage = SetImage(DBModel.GetStringResource(ConnectionCharacter));
            }
        }

        #endregion

        #region SelectedLocationCommand
        public LambdaCommand SelectedLocationCommand { get; set; }

        public void SelectedLocationMethod(object obj)
        {
            if (!String.IsNullOrEmpty(ConnectionLocation))
            {
                LocationImage = SetImage(DBModel.GetStringResource(ConnectionLocation));
            }
        }

        #endregion

        #region SelectedClassificationCommand
        public LambdaCommand SelectedClassificationCommand { get; set; }

        public void SelectedClassificationMethod(object obj)
        {
            if (!String.IsNullOrEmpty(ConnectionClassification))
            {
                ClassificationDescription = DBModel.GetStringResource(ConnectionClassification);
            }
        }

        #endregion

        #endregion

        public RivitedViewModel()
        {
            ListCharacter = DBModel.GetListSubtype("Характер соединения");
            ListLocation = DBModel.GetListSubtype("Взаимное расположение");
            ListClassification = DBModel.GetListSubtype("Классификация прочности");

            SelectedCharacterCommand = new(SelectedCharacterMethod);
            SelectedLocationCommand = new(SelectedLocationMethod);
            SelectedClassificationCommand = new(SelectedClassificationMethod);

            LoadPerRivet = new();
            RivetSrez = new();
            NeededRivetsSrez = new();
        }
    }
}
