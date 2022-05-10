using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace NASA_PL.Views
{
    /// <summary>
    /// Interaction logic for BigImageView.xaml
    /// </summary>
    public partial class BigImageView : Window
    {
        public BigImageView(string imageUrl)
        {
            InitializeComponent();
            BigImage.Source = new BitmapImage(new Uri(imageUrl));
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
