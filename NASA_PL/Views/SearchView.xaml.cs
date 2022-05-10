using NASA_PL.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;



namespace NASA_PL.Views
{
    /// <summary>
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class SearchView : Page
    {
        private SearchViewModel _searchViewModel;

        public SearchView()
        {
            InitializeComponent();
            _searchViewModel = new SearchViewModel();
            DataContext = _searchViewModel;
            QueryStringText.Focus();
        }

        private void QueryStringText_OnKeyDown(object sender, KeyEventArgs e)
        {
            // If the user presses enter, execute the search command from the view model
            if (e.Key == Key.Enter)
            {
                _searchViewModel.SearchCommand.Execute(null);
            }
        }
    }
}
