using NASA_BE;
using NASA_BL;
using System.Collections.Generic;

namespace NASA_PL.Models
{
    public class PlanetsModel
    {
        private readonly BL _bl;
        public PlanetsModel()
        {
            _bl = new BL();
        }

        public List<Planet> GetSolarSystem()
        {
            return _bl.GetSolarSystem();
        }
    }
}
