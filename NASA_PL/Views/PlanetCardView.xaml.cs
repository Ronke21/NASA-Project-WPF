using NASA_BE;
using System.Windows;
using System.Windows.Input;

namespace NASA_PL.Views
{
    /// <summary>
    /// Interaction logic for PlanetCardView.xaml
    /// </summary>
    public partial class PlanetCardView : Window
    {
        public PlanetCardView(Planet planet)
        {
            DataContext = planet;
            // do not allow full screen
            ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
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
