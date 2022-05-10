using NASA_BE;
using NASA_DAL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NASA_BL
{
    public class BL
    {
        private readonly Dal _dal = new();

        public async Task<APOD> GetApod()
        {
            return await _dal.GetApodFromNasaApi();
        }

        public async Task<List<NearEarthObject>> GetNearEarthObject(string start, string end)
        {
            var nearEarthObject = await _dal.GetNearEarthObject(start, end);
            var result = from s in nearEarthObject.near_earth_objects.Values
                         from q in s
                         select new NearEarthObject()
                         {
                             Id = q.id.ToString(),
                             Name = q.name,
                             Hazardous = q.is_potentially_hazardous_asteroid,
                             Diameter = q.estimated_diameter.meters.estimated_diameter_min,
                             Velocety = q.close_approach_data[0].relative_velocity.kilometers_per_hour,
                             MissDistance = q.close_approach_data[0].miss_distance.kilometers,
                             CloseApproach = q.close_approach_data[0].close_approach_date
                         };
            return result.ToList();
        }

        public async Task<Dictionary<string, string>> GetSearchResult(string querySearch, int confidence)
        {
            if (string.IsNullOrEmpty(querySearch))
            {
                return new Dictionary<string, string>();
            }

            var imagesAndDescription = await _dal.GetSearchResult(querySearch);
            // if the confidence is 0, return all the images
            if (confidence == 0)
            {
                return imagesAndDescription;
            }

            var res = new Dictionary<string, string>();

            Parallel.ForEach(imagesAndDescription.Keys, async image =>
            {
                var tag = await _dal.GetImageTagsFromImagga(image);
                if (tag.result == null) return;
                if (tag.result.tags.Any(resTag => resTag.confidence >= confidence && resTag.tag.en.ToLower() == "planet"))
                {
                    res.Add(image, imagesAndDescription[image]);
                }
            });

            return res;
        }

        public List<Planet> GetSolarSystem()
        {
            return _dal.GetSolarSystem();
        }

        public bool CheckUserAndPassword(string user, string password)
        {
            return _dal.CheckUserAndPassword(user, password);
        }

        // create function that send to dal imageURL and save it to firebase
        public async Task SaveImageToFirebase(string imageURL, string imageDescription)
        {
            await _dal.UploadImageToFirebase(imageURL, imageDescription);
        }

        //create function that get all images in firebase and return them
        public List<FirebaseImage> GetImagesFromFirebase()
        {
            return _dal.GetSavedImages();
        }
    }
}
