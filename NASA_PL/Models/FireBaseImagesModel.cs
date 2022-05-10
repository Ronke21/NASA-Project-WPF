using NASA_BE;
using NASA_BL;
using System.Collections.Generic;

namespace NASA_PL.Models
{

    public class FireBaseImagesModel
    {
        private readonly BL _bl;

        public FireBaseImagesModel()
        {
            _bl = new BL();
        }

        //create function that get all images in firebase and return them
        public List<FirebaseImage> GetImagesFromFirebase()
        {
            return _bl.GetImagesFromFirebase();
        }
    }
}