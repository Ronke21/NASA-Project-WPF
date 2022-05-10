using NASA_PL.ViewModels;
using System.Windows.Controls;

namespace NASA_PL.Views
{
    /// <summary>
    /// Interaction logic for APODView.xaml
    /// </summary>

    public partial class APODView : Page
    {
        APODViewModel ViewModel = new APODViewModel();

        public APODView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}