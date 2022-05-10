using NASA_BL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NASA_PL.Models
{

    public class SearchModel
    {
        private readonly BL _bl;

        public SearchModel()
        {
            _bl = new BL();
        }

        public async Task SaveImageToFirebase(string imageUrl, string imageDescription)
        {
            await _bl.SaveImageToFirebase(imageUrl, imageDescription);
        }

        public async Task<Dictionary<string, string>> GetSearchResult(string search, int confidence)
        {
            return await _bl.GetSearchResult(search, confidence);
        }
    }
}
