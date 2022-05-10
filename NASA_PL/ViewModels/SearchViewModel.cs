using CommunityToolkit.Mvvm.Input;
using NASA_BE.Annotations;
using NASA_PL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NASA_PL.ViewModels
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        private readonly SearchModel _searchModel;
        public event PropertyChangedEventHandler PropertyChanged;

        private string _queryString;
        private int _confidence;

        public ICommand UpdateQueryStringCommand { get; set; }
        public ICommand UpdateConfidenceCommand { get; set; }
        public IAsyncRelayCommand UploadImageToFireBaseCommand { get; set; }
        public IAsyncRelayCommand SearchCommand { get; set; }

        private Dictionary<string, string> _resultsDictionary;
        public Dictionary<string, string> ResultsDictionary
        {
            get => _resultsDictionary;
            set
            {
                _resultsDictionary = value;
                OnPropertyChanged(nameof(ResultsDictionary));
            }
        }

        private Visibility _showList;
        public Visibility ShowList
        {
            get => _showList;
            set
            {
                _showList = value;
                OnPropertyChanged(nameof(ShowList));
            }
        }

        public SearchViewModel()
        {
            _searchModel = new SearchModel();
            ResultsDictionary = null;

            UpdateQueryStringCommand = new RelayCommand<TextBox>(box => _queryString = box.Text);

            UpdateConfidenceCommand = new RelayCommand<Slider>(slider => _confidence = Convert.ToInt32(slider.Value));

            UploadImageToFireBaseCommand = new AsyncRelayCommand<string>(async (imageUrl) =>
            {
                await _searchModel.SaveImageToFirebase(imageUrl, ResultsDictionary.FirstOrDefault(item => item.Key == imageUrl).Value);
            });

            SearchCommand = new AsyncRelayCommand(async () =>
            {
                ShowList = Visibility.Collapsed;
                ResultsDictionary = new Dictionary<string, string>();
                ResultsDictionary = await Task.Run(() => GetSearchResult(_queryString, _confidence));
                ShowList = Visibility.Visible;
            });
        }

        public async Task<Dictionary<string, string>> GetSearchResult(string search, int confidence)
        {
            var result = await _searchModel.GetSearchResult(search, confidence);

            // if no images were found or no image matched the required confidence
            if (result.Count == 0)
            {
                result.Add("https://upload.wikimedia.org/wikipedia/commons/thumb/6/6e/NASA_Wormball_logo.svg/768px-NASA_Wormball_logo.svg.png",
                    $"No matching images for \"{_queryString}\" with confidence of {_confidence}%");
            }

            return result;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
