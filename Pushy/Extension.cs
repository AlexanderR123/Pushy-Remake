using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Pushy
{
    public static class Extension
    {
        public static Bitmap MergeImages(this Image image1, Image image2)
        {
            Bitmap bitmap = new Bitmap(Math.Max(image1.Width, image2.Width), Math.Max(image1.Height, image2.Height));
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(image1, 0, 0, image1.Width,image1.Height);
                g.DrawImage(image2, 0, 0, image2.Width, image2.Height);
            }

            return bitmap;
        }

        public static BitmapSource ToBitmapSource(this Bitmap pBitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                pBitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

    }
}
