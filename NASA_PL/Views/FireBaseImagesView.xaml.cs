using NASA_PL.ViewModels;
using System.Windows.Controls;

namespace NASA_PL.Views
{
    /// <summary>
    /// Interaction logic for FireBaseImagesView.xaml
    /// </summary>
    public partial class FireBaseImagesView : Page
    {
        public FireBaseImagesView()
        {
            InitializeComponent();
            var ViewModel = new FireBaseImagesViewModel();
            DataContext = ViewModel;
        }
    }
}
