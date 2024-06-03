using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FastenersChoosing.Models.DetachableFasteners
{
    public class Fastener
    {
        public string Name = null;

        public string Type = null;

        public string Gost = null;

        public string Description = null;

        public BitmapImage Image;

        public static BitmapImage DefaultImage;
        public Fastener(BitmapImage defaultImage)
        {
            DefaultImage = defaultImage;

            Image = DefaultImage;
        }
    }
}

