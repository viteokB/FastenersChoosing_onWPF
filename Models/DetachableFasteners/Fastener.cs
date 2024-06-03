using FastenersChoosing.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FastenersChoosing.Models.DetachableFasteners
{
    public class Fastener : BaseViewModel
    {
        private string _name;

        private string _type;

        private string _gost;

        private string _description;

        private BitmapImage _image;

        public string Name
        {
            get => _name;
            set => Set<string>(ref _name, value);
        }

        public string Type 
        {
            get => _type;
            set => Set<string>(ref _type, value);
        }

        public string Gost 
        {
            get => _gost;
            set => Set<string>(ref _gost, value);
        }

        public string Description 
        {
            get => _description;
            set => Set<string>(ref _description, value);
        } 

        public BitmapImage Image 
        {
            get => _image;
            set => Set<BitmapImage>(ref _image, value);
        }

        public static BitmapImage DefaultImage { get; set; }

        public Fastener(BitmapImage defaultImage)
        {
            DefaultImage = defaultImage;

            Image = DefaultImage;
        }

        public Fastener(string gost, BitmapImage bi)
        {
            Gost = gost;
            Image = bi;
        }
    }
}

