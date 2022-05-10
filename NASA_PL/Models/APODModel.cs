using NASA_BE;
using NASA_BL;
using System.Threading.Tasks;

namespace NASA_PL.Models
{
    public class APODModel
    {
        private readonly BL _bl;
        
        public APODModel()
        {
            _bl = new BL();
        }

        public async Task<APOD> GetImageOfTheDay()
        {
            return await _bl.GetApod();
        }
    }

}