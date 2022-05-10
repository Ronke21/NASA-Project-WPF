using CommunityToolkit.Mvvm.Input;
using NASA_BE;
using NASA_PL.Models;
using NASA_PL.Views;
using Syncfusion.Windows.Shared;
using System.Collections.Generic;
using System.Windows.Input;


namespace NASA_PL.ViewModels
{
    public class PlanetsViewModel
    {
        private readonly PlanetsModel _model;

        public ICommand OpenPlanetCardCommand { get; private set; }
        public ICommand MovePlanetRightCommand { get; private set; }
        public ICommand MovePlanetLeftCommand { get; private set; }

        public PlanetsViewModel()
        {
            _model = new PlanetsModel();
            OpenPlanetCardCommand = new RelayCommand<Carousel>(OpenPlanetCard, o => true);
            MovePlanetRightCommand = new RelayCommand<Carousel>(MovePlanetRight, o => true);
            MovePlanetLeftCommand = new RelayCommand<Carousel>(MovePlanetLeft, o => true);
        }

        public List<Planet> GetPlanetsList => _model.GetSolarSystem();

        private void OpenPlanetCard(Carousel planetCarousel)
        {
            var pcv = new PlanetCardView(GetPlanetsList[planetCarousel.SelectedIndex]);
            pcv.ShowDialog();
        }

        private void MovePlanetRight(Carousel PlanetsCarousel)
        {
            var current = PlanetsCarousel.SelectedIndex;
            if (current < 7)
            {
                PlanetsCarousel.SelectedIndex = current + 1;
            }
            else
            {
                PlanetsCarousel.SelectedIndex = 0;
            }
        }

        private void MovePlanetLeft(Carousel PlanetsCarousel)
        {
            var current = PlanetsCarousel.SelectedIndex;
            if (current > 0)
            {
                PlanetsCarousel.SelectedIndex = current - 1;
            }
            else
            {
                PlanetsCarousel.SelectedIndex = 7;
            }
        }
    }
}