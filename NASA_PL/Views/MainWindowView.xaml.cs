using MaterialDesignThemes.Wpf;
using NASA_BL;
//using NASA_PL.UserControls;
using NASA_PL.ViewModels;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace NASA_PL.Views
{

    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {

        private readonly BL _bl;
        private bool _hidden;

        public MainWindowView()
        {
            InitializeComponent();
            var lp = new LoginWindowView();
            lp.ShowDialog();
            var mainWindowViewModel = new MainWindowViewModel();
            DataContext = mainWindowViewModel;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (_hidden)
            {
                var sb = Resources["OpenMenu"] as Storyboard;
                sb?.Begin(SideBar);
                _hidden = false;
                OpenCloseButtonIcon.Kind = PackIconKind.MenuOpen;
            }
            else
            {
                var sb = Resources["CloseMenu"] as Storyboard;
                sb?.Begin(SideBar);
                _hidden = true;
                OpenCloseButtonIcon.Kind = PackIconKind.Menu;
            }
        }

        private void SideBarButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_hidden)
            {
                ButtonBase_OnClick(sender, e);
            }
        }

        private void Drag_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            // if the window is maximized, restore it to allow dragging
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }


            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}

