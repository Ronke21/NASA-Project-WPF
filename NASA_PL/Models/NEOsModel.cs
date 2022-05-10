using CommunityToolkit.Mvvm.ComponentModel;
using NASA_BE;
using NASA_BL;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;


namespace NASA_PL.Models
{
    public class NEOsModel
    {
        private readonly BL _bl;

        public ObservableCollection<NearEarthObject> NeoList;

        public NEOsModel()
        {
            _bl = new BL();
            NeoList = new ObservableCollection<NearEarthObject>();
        }

        public async Task<ObservableCollection<NearEarthObject>> GetNearEarthObject(string start, string end, double diameter)
        {
            NeoList = new ObservableCollection<NearEarthObject>(from s in await _bl.GetNearEarthObject(start, end)
                                                                where s.Diameter > diameter
                                                                select s);
            return NeoList;
        }
    }
}
