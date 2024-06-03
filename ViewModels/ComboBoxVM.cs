using FastenersChoosing.Infrastructure.Commands;
using FastenersChoosing.Models.DetachableFasteners;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FastenersChoosing.ViewModels
{
    public class ComboBoxesVM : BaseViewModel
    {
        public Fastener SelectedFastener;

        public ObservableCollection<Fastener> PossibleFastners;

        #region getterы получения выбранного имени, типа и госта

        public string fastenerName 
        {
            get => SelectedFastener.Name; 
            set { Set<string>(ref SelectedFastener.Name, value); }
        }
        public string fastenerType 
        {
            get => SelectedFastener.Type;
            set { Set<string>(ref SelectedFastener.Type, value); }
        }
        public string fastenersGost 
        {
            get => SelectedFastener.Gost;
            set { Set<string>(ref SelectedFastener.Gost, value); }
        }

        #endregion

        #region Списки строк для заполнения ComboBoxesItems

        private List<string> _allNames;

        private List<string> _namesTypes;

        private List<string> _typesGosts;

        public List<string> allNames
        {
            get => _allNames;
            private set { Set<List<string>>(ref _allNames, value); }
        }

        public List<string> namesTypes
        { 
            get => _namesTypes;
            private set { Set<List<string>>(ref _namesTypes, value); } 
        }

        public List<string> typesGosts
        {
            get => _typesGosts;
            private set { Set<List<string>>(ref _typesGosts, value); }
        }

        #endregion

        #region Строка описания и путь к изображению

        public string Description
        {
            get => SelectedFastener.Description;
            set { Set<string>(ref SelectedFastener.Description, value); }
        }

        private string _standartPath = @"\Data\Photos\NULL_INPUT.png";

        public BitmapImage FastnerImage
        {
            get => SelectedFastener.Image;
            set { Set<BitmapImage>(ref SelectedFastener.Image, value); }
        }

        #endregion

        #region Объявления команд

        public LambdaCommand SelectedNameCommand { get; }
        public LambdaCommand SelectedTypeCommand { get; }
        public LambdaCommand SelectedGostCommand { get; }

        #endregion

        public ComboBoxesVM()
        {
            SelectedFastener = new Fastener(SetImage(_standartPath));

            allNames = DBModel.GetListFastenersNames();

            SelectedNameCommand = new LambdaCommand(
                (name) => 
                {
                    if(name != null)
                        namesTypes = DBModel.GetListFastenersTypes(name.ToString());
                    else
                        namesTypes = null;
                });

            SelectedTypeCommand = new LambdaCommand(
                (type) =>
                {
                    if(type != null)
                    {
                        var gostsImages = DBModel.GetPathAndGost(type.ToString());
                        typesGosts = gostsImages[0];
                        Description = DBModel.GetStringDescription(fastenerType);
                    }
                    else
                        typesGosts = null;
                });
            SelectedGostCommand = new LambdaCommand(
                (gost) =>
                {
                    if (gost != null)
                    {
                        FastnerImage = SetImage(DBModel.GetStringImagePath(gost.ToString()));
                    }
                    else
                        FastnerImage = Fastener.DefaultImage;
                });
        }

        #region Методы установки изображения по отн. пути
        public BitmapImage SetImage(string localPath)
        {
            Uri uri = new Uri(GetAbsolutPath(localPath), UriKind.Absolute);

            return new BitmapImage(uri);
        }

        private string GetAbsolutPath(string localPath)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string absolutPath = currentDirectory + localPath;

            return absolutPath;
        }

        #endregion
    }
}
