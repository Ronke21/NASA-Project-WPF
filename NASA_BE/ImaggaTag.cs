using System.Collections.Generic;

namespace NASA_BE
{
    public class ImaggaTag
    {
        public class Result
        {
            public class Tag
            {
                public class innerTag
                {
                    public string en { get; set; }
                }

                public double confidence { get; set; }
                public innerTag tag { get; set; }
            }

            public List<Tag> tags { get; set; }
        }

        public class Status
        {
            public string text { get; set; }
            public string type { get; set; }
        }

        public Result result { get; set; }
        public Status status { get; set; }
    }
}
