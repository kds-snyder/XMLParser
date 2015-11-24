using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using XMLParser.BlogData;
using XMLParser.Constant;

namespace XMLParser.Services
{
    // Parse XML of WordPress RSS feed
    public class ParseXMLWordPress
    {
        // Parse XML data from Word Press 
        public static Blog ParseXMLWordPressData(string inputUrl)
        {
            // Load XML from blog RSS feed
            XmlDocument xmlData= loadXMLFeed(inputUrl);

            // Get the blog information and posts
            var blog = new Blog();

            getBlogInfo(xmlData, blog);

            getBlogPosts(xmlData, blog);

            return blog;

        }

        // Load the XML data from the RSS feed at the input URL
        private static XmlDocument loadXMLFeed(string inputUrl)
        {
            string RSSUrl;

            // Append 'feed' or '/feed', depending on whether input ends with '/'
            if (inputUrl[inputUrl.Length - 1] == '/')
            {
                RSSUrl = inputUrl + "feed";
            }
            else
            {
                RSSUrl = inputUrl + "/feed";
            }

            XmlDocument xmlDoc = new XmlDocument(); // Create an XML document object
            try
            {
                xmlDoc.Load(RSSUrl); // Load the XML document from the specified file
            }           
            catch (Exception e)
            {
                throw new Exception("Unable to read RSS feed at " + inputUrl, e);
            }
            
            return xmlDoc;
        }

        // Get the blog information from the XML data,
        //  and set it in the blog object
        private static void getBlogInfo(XmlDocument xmlData, Blog blog)
        {
            // Get the blog information XML node
            XmlNode blogInfoXMLNode = xmlData.SelectSingleNode(BlogInfoXML.Node);

            // Set the blog information in the blog object
            blog.Title = blogInfoXMLNode.ChildNodes.Item(BlogInfoXML.TitleIndex).InnerText;
            blog.Description = blogInfoXMLNode.ChildNodes.Item(BlogInfoXML.DescriptionIndex).InnerText;
            blog.Link = blogInfoXMLNode.ChildNodes.Item(BlogInfoXML.LinkIndex).InnerText;
        }

        // Get the blog posts from the XML data
        private static void getBlogPosts(XmlDocument xmlData, Blog blog)
        {
            // Get the list of blog post XML nodes
            XmlNodeList blogPostXMLNodes = xmlData.SelectNodes(BlogPostXML.Node); 

            // Set up a post object from each blog post data, in the blog object
            foreach (XmlNode blogPostXML in blogPostXMLNodes)
            {
                var blogPost = new Post();
                blogPost.PublicationDate = 
                    Convert.ToDateTime(blogPostXML.ChildNodes.Item(BlogPostXML.PublicationDateIndex).InnerText);
                blogPost.Title = blogPostXML.ChildNodes.Item(BlogPostXML.TitleIndex).InnerText;
                blogPost.Link = blogPostXML.ChildNodes.Item(BlogPostXML.LinkIndex).InnerText;
                blogPost.Description = blogPostXML.ChildNodes.Item(BlogPostXML.DescriptionIndex).InnerText;
                blogPost.Content = blogPostXML.ChildNodes.Item(BlogPostXML.ContentIndex).InnerText;

                blog.Posts.Add(blogPost);
            }
        }
    }
}
