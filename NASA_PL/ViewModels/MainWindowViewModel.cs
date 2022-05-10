using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NASA_PL.Models;
using NASA_PL.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NASA_PL.ViewModels
{
    public class MainWindowViewModel
    {
        public PlanetsView PlanetsPage;
        public SearchView SearchPage;
        public NEOsView NEOsPage;

        public ICommand CloseWindowCommand { get; private set; }
        public ICommand RestoreWindowCommand { get; private set; }
        public ICommand MinimizeWindowCommand { get; private set; }

        public ICommand OpenApodPageCommand { get; private set; }
        public ICommand OpenSearchPageCommand { get; private set; }
        public ICommand OpenFireBasePageCommand { get; private set; }
        public ICommand OpenPlanetsPageCommand { get; private set; }
        public ICommand OpenNeosPageCommand { get; private set; }

        public ICommand OpenInfoWindowCommand { get; private set; }

        private readonly MainWindowModel _model;

        public MainWindowViewModel()
        {
            PlanetsPage = new PlanetsView();
            SearchPage = new SearchView();
            NEOsPage = new NEOsView();

            _model = new MainWindowModel();
            CloseWindowCommand = new RelayCommand<Window>(window => window?.Close());
            RestoreWindowCommand = new RelayCommand<Window>(RestoreWindow);
            MinimizeWindowCommand = new RelayCommand<Window>(window => window.WindowState = WindowState.Minimized);

            OpenApodPageCommand = new RelayCommand<Frame>(frame => frame.Content = new APODView(), frame => frame.Content is not APODView);
            OpenSearchPageCommand = new RelayCommand<Frame>(frame => frame.Content = SearchPage, frame => frame.Content is not SearchView);
            OpenFireBasePageCommand = new RelayCommand<Frame>(frame => frame.Content = new FireBaseImagesView(), frame => frame.Content is not FireBaseImagesView);
            OpenPlanetsPageCommand = new RelayCommand<Frame>(frame => frame.Content = PlanetsPage, frame => frame.Content is not PlanetsView);
            OpenNeosPageCommand = new RelayCommand<Frame>(frame => frame.Content = NEOsPage, frame => frame.Content is not NEOsView);
            OpenInfoWindowCommand = new RelayCommand(() =>
            {
                var infoWindow = new InfoView();
                infoWindow.ShowDialog();
            });
        }

        private void RestoreWindow(Window window)
        {
            switch (window.WindowState)
            {
                case WindowState.Maximized:
                    window.WindowState = WindowState.Normal;
                    break;
                case WindowState.Normal:
                    window.WindowState = WindowState.Maximized;
                    break;
            }
        }
    }
}
