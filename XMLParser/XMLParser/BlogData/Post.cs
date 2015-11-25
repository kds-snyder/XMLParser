using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLParser.BlogData
{
    public class Post
    {
        public BlogInfo BlogInformation { get; set; }
        
        public DateTime PublicationDate { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }

        public string Content { get; set; }
    }
}
