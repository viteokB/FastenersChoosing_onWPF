using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace FastenersChoosing.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if(Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }

        public static BitmapImage SetImage(string localPath)
        {
            Uri uri = new Uri(GetAbsolutPath(localPath), UriKind.Absolute);

            return new BitmapImage(uri);
        }

        private static string GetAbsolutPath(string localPath)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string absolutPath = currentDirectory + localPath;

            return absolutPath;
        }
    }
}
