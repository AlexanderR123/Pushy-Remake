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
        Spielfeld _Spielfeld;


        public MainWindow()
        {

            _Spielfeld = Pushy.Spielfeld.Level1;

            //https://www.youtube.com/watch?v=abIStsPhqZY
            InitializeComponent();

            DrawImage();



            
        }



        public void DrawImage()
        {
            using (MemoryStream memory = new MemoryStream())
            {
                _Spielfeld.ZeichneFeld().Save(memory, ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                Spielfeld.Source = bitmapImage;
            }

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            switch(e.Key)
            {
                case Key.W:
                    _Spielfeld.Move(Move.Up);
                    DrawImage();
                    break;
                case Key.D:
                    _Spielfeld.Move(Move.Right);
                    DrawImage();
                    break;
                case Key.S:
                    _Spielfeld.Move(Move.Down);
                    DrawImage();
                    break;
                case Key.A:
                    _Spielfeld.Move(Move.Left);
                    DrawImage();
                    break;
                case Key.N:
                    _Spielfeld.Level++;
                    DrawImage();
                    break;
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _Spielfeld.Level = _Spielfeld.Level;
            DrawImage();
        }

        private void LVL_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                _Spielfeld.Level = Convert.ToInt32(LVL.Text);
                DrawImage();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
