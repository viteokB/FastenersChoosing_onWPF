using FastenersChoosing.Infrastructure.Commands;
using FastenersChoosing.Models.DetachableFasteners;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace FastenersChoosing.ViewModels
{
    public class ComboBoxesVM : BaseViewModel
    {
        #region getterы получения выбранного имени, типа и госта
        private string _fastenerName;
        private string _fastenerType;
        private string _fastenersGost;

        public string fastenerName 
        {
            get => _fastenerName; 
            set { Set<string>(ref _fastenerName, value); }
        }
        public string fastenerType 
        {
            get => _fastenerType;
            set { Set<string>(ref _fastenerType, value); }
        }
        public string fastenersGost 
        {
            get => _fastenersGost;
            set { Set<string>(ref _fastenersGost, value); }
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

        #region Строки заполнения описания и путь к изображению
        private string _description;

        private Uri _standartImageURI = new Uri(@"\Data\Photos\NULL_INPUT.png", UriKind.Relative);

        private BitmapImage _fastenerImage;

        public string Description
        {
            get => _description;
            set { Set<string>(ref _description, value); }
        }

        public BitmapImage FastnerImage
        {
            get => _fastenerImage;
            set { Set<BitmapImage>(ref _fastenerImage, value); }
        }

        #endregion

        public ComboBoxesVM()
        {
            allNames = DBModel.GetListFastenersNames();

            FastnerImage = new BitmapImage(_standartImageURI);

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
                        typesGosts = DBModel.GetListGostNumbers(type.ToString());
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
                        Uri uri = new Uri(DBModel.GetStringImagePath(gost.ToString()), UriKind.Relative);
                        FastnerImage = new BitmapImage(uri);
                    }
                    else
                        FastnerImage = new BitmapImage(_standartImageURI);
                });
        }

        #region Объявления команд

        public LambdaCommand SelectedNameCommand { get;  }
        public LambdaCommand SelectedTypeCommand { get; }
        public LambdaCommand SelectedGostCommand { get; }

        #endregion
    }
}
