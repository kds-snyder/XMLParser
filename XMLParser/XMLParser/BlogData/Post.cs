using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLParser.BlogData
{
    // Blog post object
    public class Post
    {
        public DateTime PublicationDate { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }

        public string Content { get; set; }
    }
}
