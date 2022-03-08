using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Pushy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //https://www.youtube.com/watch?v=abIStsPhqZY
            InitializeComponent();


            //DrawImage();



        }



        public void DrawImage()
        {
            using (MemoryStream memory = new MemoryStream())
            {
                Spielfeld.ZeichneFeld().Save(memory, ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                Spielfeld1.Source = bitmapImage;
            }

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            switch(e.Key)
            {
                case Key.W:
                    Spielfeld.Move(Move.Up);
                    DrawImage();
                    break;
                case Key.D:
                    Spielfeld.Move(Move.Right);
                    DrawImage();
                    break;
                case Key.S:
                    Spielfeld.Move(Move.Down);
                    DrawImage();
                    break;
                case Key.A:
                    Spielfeld.Move(Move.Left);
                    DrawImage();
                    break;
                case Key.N:
                    Spielfeld.Level++;
                    DrawImage();
                    break;
                case Key.R:
                    Spielfeld.Level = Spielfeld.Level;
                    DrawImage();
                    break;
            }

            Level_lb.Content = "Level: " + Spielfeld.Level;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Spielfeld.Level = Spielfeld.Level;
            DrawImage();
        }

        private void LVL_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Spielfeld.Level = Convert.ToInt32(LVL.Text);
                DrawImage();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
