using FastenersChoosing.Infrastructure.Commands;
using FastenersChoosing.Models.DetachableFasteners;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media.Imaging;

namespace FastenersChoosing.ViewModels
{
    public class MainVM : BaseViewModel
    {
        #region Свойства
        #region SelectedFastener
        private Fastener _selectedFastener;
        public Fastener SelectedFastener
        {
            get => _selectedFastener;
            set => Set(ref _selectedFastener, value);
        }
        #endregion

        #region PossibleFastners
        private ObservableCollection<Fastener> _possibleFastners;
        public ObservableCollection<Fastener> PossibleFastners 
        {
            get => _possibleFastners;
            set => Set(ref _possibleFastners, value);
        }
        #endregion

        #region SelectedIndex для выбора изделия из ListView
        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set => Set(ref _selectedIndex, value);
        }
        #endregion

        #region SelectedTabIndex

        private int _selectedTabIndex;

        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set => Set(ref _selectedTabIndex, value);
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

        #region Строка-путь к стандартному изображению

        private string _standartPath = @"\Data\Photos\NULL_INPUT.png";

        #endregion

        #region

        private Dictionary<string, List<string>> _gostParametrs;
        public Dictionary<string, List<string>> GostParametrs 
        {
            get => _gostParametrs;
            set => Set(ref _gostParametrs, value); 
        }

        #endregion

        #endregion

        #region Объявления команд

        public LambdaCommand SelectedNameCommand { get; }
        public LambdaCommand SelectedTypeCommand { get; }
        public LambdaCommand SelectedGostCommand { get; }
        public LambdaCommand SelectedAnotherCommand { get; }

        #endregion

        public MainVM()
        {
            SelectedFastener = new Fastener(SetImage(_standartPath));

            allNames = DBModel.GetListFastenersNames();

            SelectedNameCommand = new LambdaCommand(
                (name) =>
                {
                    if (name != null)
                        namesTypes = DBModel.GetListFastenersTypes(name.ToString());
                    else
                        namesTypes = null;
                });

            SelectedTypeCommand = new LambdaCommand(
                (type) =>
                {
                    if (type != null)
                    {
                        var gostsImages = DBModel.GetPathAndGost(type.ToString());
                        typesGosts = gostsImages[0];
                        FillPossibleFasteners(typesGosts, gostsImages[1]);
                        SelectedFastener.Description = DBModel.GetStringDescription(type.ToString());
                        SelectedTabIndex = 1;
                    }
                    else
                        typesGosts = null;
                });
            SelectedGostCommand = new LambdaCommand(
                (gost) =>
                {
                    if (gost != null)
                    {
                        SelectedFastener.Image = SetImage(DBModel.GetStringImagePath(gost.ToString()));
                        GostParametrs = DBModel.GetGostParametrs(gost.ToString());
                    }
                    else
                        SelectedFastener.Image = Fastener.DefaultImage;
                });
            SelectedAnotherCommand = new LambdaCommand(
            (newFastener) =>
            {
                if (newFastener != null && SelectedIndex >= 0 && SelectedIndex < PossibleFastners.Count)
                {
                    var tmp = PossibleFastners[SelectedIndex];
                    SelectedFastener.Gost = tmp.Gost;
                    SelectedFastener.Image = tmp.Image;
                    SelectedTabIndex = 0;
                }
            });
        }

        public void FillPossibleFasteners(List<string> gosts, List<string> localPaths)
        {
            PossibleFastners = new ObservableCollection<Fastener>();

            for (int i = 0; i < gosts.Count; i++)
            {
                PossibleFastners.Add(new Fastener(gosts[i], SetImage(localPaths[i])));
            }
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
