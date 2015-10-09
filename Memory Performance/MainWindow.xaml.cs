using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Memory_Performance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<string> imgList = GetImageList();
            LoadImages(imgList);
        }

        private void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ImageWindow img = new ImageWindow(listView);
            img.Show();
        }

        private IEnumerable<string> GetImageList()
        {
            DirectoryInfo di = new DirectoryInfo(".\\Images");
            foreach (FileInfo fi in di.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly))
            {
                if (IsImageValid(fi))
                {
                    yield return fi.FullName;
                }
            }
        }

        private bool IsImageValid(FileInfo fi)
        {
            //return fi.Extension.ToLower().Equals(".jpg");
            byte[] buff = new byte[fi.Length];
            fi.OpenRead().Read(buff, 0, (int) fi.Length);
            return (
                buff.Length > 10 &&
                buff[0] == 0xFF &&
                buff[1] == 0xD8 &&
                (
                    (
                        buff[6] == 0x4A &&
                        buff[7] == 0x46 &&
                        buff[8] == 0x49 &&
                        buff[9] == 0x46
                        ) ||
                    (
                        buff[6] == 0x45 &&
                        buff[7] == 0x78 &&
                        buff[8] == 0x69 &&
                        buff[9] == 0x66
                        ) &&
                    buff[10] == 0x00
                    ));
        }

        private void LoadImages(IEnumerable<string> imgList)
        {
            foreach (string path in imgList)
            {
                Image image = new Image
                {
                    Stretch = Stretch.Fill,
                    Width = 100,
                    Source = new BitmapImage(new Uri(path))
                };
                listView.Items.Add(image);
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            decimal y = 0;
            decimal x = 5/y;
        }
    }
}