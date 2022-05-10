using NASA_BL;

namespace NASA_PL.Models
{
    public class LoginWindowModel
    {
        private readonly BL _bl;

        public LoginWindowModel()
        {
            _bl = new BL();
        }

        public bool CheckUserAndPassword(string user, string password)
        {
            return _bl.CheckUserAndPassword(user, password);
        }
    }

}