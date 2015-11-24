using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLParser.Constant
{
    public static class BlogInfoXML
    {
        // XML node specifications

        // Blog information:
        // <rss>
        //   <channel> 
        //     <title>
        //     <atom:link>
        //     <link>
        //     <description>
        public const string Node = "/rss/channel";

        // Element indices (under <channel>) for blog information
        public const int TitleIndex = 0;       
        public const int LinkIndex = 2;
        public const int DescriptionIndex = 3;

    }
    public static class BlogPostXML
    {
        // Blog posts:
        // <rss>
        //   <channel> 
        //     <item> 
        //       <title>
        //       <link>
        //       <comments>
        //       <pubDate>
        //       <dc:creator>
        //       <category>
        //       <guid>
        //       <description>
        //          <![CDATA[
        //       <content:encoded>
        //          <![CDATA[
        public const string Node = "/rss/channel/item";

        // Element indices (under <item>) for blog post
        public const int TitleIndex = 0;
        public const int LinkIndex = 1;
        public const int CommentsIndex = 2;
        public const int PublicationDateIndex = 3;
        public const int CreatorIndex = 4;
        public const int DescriptionIndex = 7;
        public const int ContentIndex = 8;
    }
}
