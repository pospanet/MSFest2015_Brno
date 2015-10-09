using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Debugging_Performance
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string[] imagelist = new[]
        {
            "https://msfest2015.blob.core.windows.net/images/SAM_2336.JPG",
            "https://msfest2015.blob.core.windows.net/images/SAM_2320.JPG",
            "https://msfest2015.blob.core.windows.net/images/SAM_2318.JPG",
            "https://msfest2015.blob.core.windows.net/images/SAM_2311.JPG",
            "https://msfest2015.blob.core.windows.net/images/SAM_2309.JPG",
            "https://msfest2015.blob.core.windows.net/images/SAM_2307.JPG",
            "https://msfest2015.blob.core.windows.net/images/SAM_2294.JPG",
            "https://msfest2015.blob.core.windows.net/images/SAM_2291.JPG"
        };
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void buttonFoo_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<string> imgList;
            imgList = GetImageList().ToArray();
            imgList = GetWebImageList().ToArray();
            LoadImages(imgList);
        }

        private IEnumerable<string> GetImageList()
        {
            DirectoryInfo di = new DirectoryInfo(".\\Images");
            foreach (FileInfo fi in di.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly))
            {
                if (IsImageValid(fi.OpenRead()))
                {
                    yield return fi.FullName;
                }
            }
        }

        private IEnumerable<string> GetWebImageList()
        {
            foreach (string url in imagelist)
            {
                HttpWebRequest request = HttpWebRequest.CreateHttp(url);
                Task<WebResponse> response = request.GetResponseAsync();
                Task.WaitAll(response);
                if (IsImageValid(response.Result.GetResponseStream()))
                {
                    yield return url;
                }
            }
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

        private bool IsImageValid(Stream fileStream)
        {
            //return fi.Extension.ToLower().Equals(".jpg");
            byte[] buff = new byte[12];
            fileStream.Read(buff, 0, 12);
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
    }
}