using System.Windows.Data;
using UserControl = System.Windows.Controls.UserControl;

namespace NASA_PL.UserControls
{
    /// <summary>
    /// Interaction logic for PlanetViewUC.xaml
    /// </summary>
    public partial class PlanetViewUC : UserControl
    {
        public PlanetViewUC()
        {
            InitializeComponent();
        }

        public Binding AverageDistanceFromTheSun { get; set; }
        public Binding Aphelion { get; set; }
        public Binding Perihelion { get; set; }
        public Binding Radius { get; set; }
        public Binding Mass { get; set; }
        public Binding AverageSurfaceTemp { get; set; }
        public Binding OrbitalPeriod { get; set; }
        public Binding AverageSpeed { get; set; }
        public Binding RotationPeriod { get; set; }
        public Binding MoonNumber { get; set; }
        public Binding ImageURL { get; set; }
    }
}
