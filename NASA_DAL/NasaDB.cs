using NASA_BE;
using System.Data.Entity;

//https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application

namespace NASA_DAL
{
    public class NasaDB : DbContext
    {
        public NasaDB() : base()
        {

        }

        public DbSet<User> UsersAndPasswords { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<FirebaseImage> SavedImagesFB { get; set; }
    }
}