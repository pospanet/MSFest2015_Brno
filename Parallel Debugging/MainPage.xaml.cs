using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Parallel_Debugging
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void btnRun_Click(object sender, RoutedEventArgs e)
        {
            btnRun.IsEnabled = false;
            long from = 1000000;
            long to = 2000000;
            List<long> numberList = new List<long>();
            for (long i = from; i < to; i++)
            {
                numberList.Add(i);
            }
            Task<bool>[] tasks = numberList.Select(num => Task.Run(() => IsPrime(num))).ToArray();

            await Task.WhenAll(tasks).ContinueWith(res => EnableButton());

        }

        private async Task EnableButton()
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                btnRun.IsEnabled = true;
            });
        }

        private bool IsPrime(long i)
        {
            if (i.Equals(1))
            {
                return true;
            }
            if (i < 1)
            {
                throw new ArgumentException("Value has to be at least 1", "i");
            }
            //long j = i-1;
            long j = i;
            long ret = i%j;
            if (ret.Equals(0))
            {
                return false;
            }
                j--;
            if (j.Equals(1))
            {
                return true;
            }
            return false;
        }
    }
}
