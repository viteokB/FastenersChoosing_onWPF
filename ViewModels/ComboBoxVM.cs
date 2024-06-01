using FastenersChoosing.Infrastructure.Commands;
using FastenersChoosing.Models.DetachableFasteners;
using System;
using System.Collections.Generic;
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
        #region getterы получения выбранного имени, типа и госта

        public string fastenerName 
        {
            get => Fastener.Name; 
            set { Set<string>(ref Fastener.Name, value); }
        }
        public string fastenerType 
        {
            get => Fastener.Type;
            set { Set<string>(ref Fastener.Type, value); }
        }
        public string fastenersGost 
        {
            get => Fastener.Gost;
            set { Set<string>(ref Fastener.Gost, value); }
        }

        #endregion

        #region Списки строк для заполнения ComboBoxesItems

        private List<string> _allNames;

        private List<string> _namesTypes;

        private List<string> _typesGosts;

        private List<List<string>> _gostsImages;

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

        public List<List<string>> GostsImages
        {
            get => _gostsImages;
            private set { Set<List<List<string>>>(ref _gostsImages, value); }
        }


        #endregion

        #region Строка описания и путь к изображению

        public string Description
        {
            get => Fastener.Description;
            set { Set<string>(ref Fastener.Description, value); }
        }

        private string _standartPath = @"\Data\Photos\NULL_INPUT.png";

        public BitmapImage FastnerImage
        {
            get => Fastener.Image;
            set { Set<BitmapImage>(ref Fastener.Image, value); }
        }

        #endregion

        #region Объявления команд

        public LambdaCommand SelectedNameCommand { get; }
        public LambdaCommand SelectedTypeCommand { get; }
        public LambdaCommand SelectedGostCommand { get; }

        #endregion

        public ComboBoxesVM()
        {
            allNames = DBModel.GetListFastenersNames();

            SetImage(_standartPath);

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
                        GostsImages = DBModel.GetPathAndGost(type.ToString());
                        Description = DBModel.GetStringDescription(fastenerType);
                    }
                    else
                        GostsImages = null;
                });
            SelectedGostCommand = new LambdaCommand(
                (gost) =>
                {
                    if (gost != null)
                    {
                        SetImage(DBModel.GetStringImagePath(gost.ToString()));
                    }
                    else
                        SetImage(_standartPath);
                });
        }

        #region Методы установки изображения по отн. пути
        public void SetImage(string localPath)
        {
            Uri uri = new Uri(GetAbsolutPath(localPath), UriKind.RelativeOrAbsolute);

            FastnerImage = new BitmapImage(uri);
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
