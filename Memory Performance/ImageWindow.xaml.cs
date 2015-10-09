using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Memory_Performance
{
    /// <summary>
    /// Interaction logic for ImageWindow.xaml
    /// </summary>
    public partial class ImageWindow : Window
    {
        private ListView parentView;
        public ImageWindow(ListView view)
        {
            parentView = view;
            InitializeComponent();
            image.Source = (parentView.SelectedItem as Image).Source;
            parentView.SelectionChanged += View_SelectionChanged;
        }

        private void View_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            image.Source = (parentView.SelectedItem as Image).Source;
        }

        protected override void OnClosed(EventArgs e)
        {
            parentView.SelectionChanged -= View_SelectionChanged;
            base.OnClosed(e);
        }

 
    }
}
