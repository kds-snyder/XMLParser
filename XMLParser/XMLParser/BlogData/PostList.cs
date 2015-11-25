using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLParser.BlogData
{   
    public class PostList
    {
        public PostList()
        {
            Posts = new List<Post>();
        }            
        public List<Post> Posts { get; set; }
    }
}
