using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLParser.Constant
{
    public static class Constants
    {
        // XML node specifications

        // Blog information:
        // <rss>
        //   <channel> 
        //     <title>
        //     <atom:link>
        //     <link>
        //     <description>
        public const string BlogInfoXMLNode = "/rss/channel";

        // Element indices (under <channel>) for blog information
        public const int BlogInfoXMLTitleIndex = 0;       
        public const int BlogInfoXMLLinkIndex = 2;
        public const int BlogInfoXMLDescriptionIndex = 3;

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
        public const string BlogPostXMLNode = "/rss/channel/item";

        // Element indices (under <item>) for blog post
        public const int BlogPostXMLTitleIndex = 0;
        public const int BlogPostXMLLinkIndex = 1;
        public const int BlogPostXMLCommentsIndex = 2;
        public const int BlogPostXMLPublicationDateIndex = 3;
        public const int BlogPostXMLCreatorIndex = 4;
        public const int BlogPostXMLDescriptionIndex = 7;
        public const int BlogPostXMLContentIndex = 8;
    }
}
