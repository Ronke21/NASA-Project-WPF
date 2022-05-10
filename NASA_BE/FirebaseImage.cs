using System.ComponentModel.DataAnnotations;

namespace NASA_BE
{
    public class FirebaseImage
    {
        [Key]
        public long Id { get; set; }
        public string Url { get; set; }
        public string OriginalUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
