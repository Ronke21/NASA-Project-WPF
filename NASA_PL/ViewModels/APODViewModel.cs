using NASA_BE;
using NASA_BE.Annotations;
using NASA_PL.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NASA_PL.ViewModels
{
    public class APODViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private APOD _apod;
        public APOD Apod
        {
            get => _apod;
            set
            {
                _apod = value;
                OnPropertyChanged(nameof(Apod));
                OnPropertyChanged(nameof(ImageUrl));
                OnPropertyChanged(nameof(Title));
                OnPropertyChanged(nameof(Explanation));
            }
        }

        public APODViewModel()
        {
            Task.Run(InitViewModel);
        }

        private async Task InitViewModel()
        {
            var model = new APODModel();
            Apod = await model.GetImageOfTheDay();
        }

        public string ImageUrl => Apod == null ? "" : Apod.Url;
        public string Title => Apod == null ? "loading image" : Apod.Title;
        public string Explanation => Apod == null ? "" : Apod.Explanation;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
