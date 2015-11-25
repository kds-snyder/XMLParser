using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using XMLParser.BlogData;
using XMLParser.BlogWordPress;
using XMLParser.Constant;

namespace XMLParser.Services
{
    // Parse XML of WordPress blog
    public class ParseXMLWordPress
    {

        // Parse XML data from Word Press input URL, using XMLDocument
        public static PostList ParseXMLWordPressXMLDoc(string inputUrl)
        {
            // Load XML document from Word Press blog 
            string XMLUrl = getXMLUrlWordPress(inputUrl);
            XmlDocument xmlData= loadXMLDocFeed(XMLUrl);

            // Get the blog information and posts                   
            BlogInfo blogInfo = getBlogXMLDocInfo(xmlData);

            PostList postList = getBlogXMLDocPosts(xmlData, blogInfo);

            return postList;

        }

        // Load the XML data at the the input URL
        private static XmlDocument loadXMLDocFeed(string inputUrl)
        {                    

            XmlDocument xmlDoc = new XmlDocument(); // Create an XML document object
            try
            {
                xmlDoc.Load(inputUrl); // Load the XML document from the specified file
            }           
            catch (Exception e)
            {
                throw new Exception("Unable to read RSS feed", e);
            }
            
            return xmlDoc;
        }
       
        // Get the blog information from the XML data via XMLDocument,
        //  and set it in the returned blog info object
        private static BlogInfo getBlogXMLDocInfo(XmlDocument xmlData)
        {
            var blogInfo = new BlogInfo();

            // Get the blog information XML node
            XmlNode blogInfoXMLNode = xmlData.SelectSingleNode(Constants.BlogInfoXMLNode);

            // Set the blog information in the blog info object
            blogInfo.Title = blogInfoXMLNode.ChildNodes.Item(Constants.BlogInfoXMLTitleIndex).InnerText;
            blogInfo.Description = blogInfoXMLNode.ChildNodes.Item(Constants.BlogInfoXMLDescriptionIndex).InnerText;
            blogInfo.Link = blogInfoXMLNode.ChildNodes.Item(Constants.BlogInfoXMLLinkIndex).InnerText;

            string blogInfoXMLNodeName = blogInfoXMLNode.Name;
            string blogInfoTitleXMLNodeName = blogInfoXMLNode.ChildNodes.Item(Constants.BlogInfoXMLTitleIndex).Name;
            string blogInfoDescrXMLNodeName = blogInfoXMLNode.ChildNodes.Item(Constants.BlogInfoXMLDescriptionIndex).Name;
            string blogInfoLinkXMLNodeName = blogInfoXMLNode.ChildNodes.Item(Constants.BlogInfoXMLLinkIndex).Name;

            return blogInfo;
        }

        // Get the blog posts from the XML data via XMLDocument,
        //  and return a PostList object
        private static PostList getBlogXMLDocPosts(XmlDocument xmlData, BlogInfo blogInfo)
        {
            var postList = new PostList();

            // Get the list of blog post XML nodes
            XmlNodeList blogPostXMLNodes = xmlData.SelectNodes(Constants.BlogPostXMLNode); 

            // Set up a post object from each blog post data
            foreach (XmlNode blogPostXML in blogPostXMLNodes)
            {
                //DateTime publicationDate = 
                //    Convert.ToDateTime(blogPostXML.ChildNodes.Item(Constants.BlogPostXMLPublicationDateIndex).InnerText);
                /*
                var post = new Post
                {
                    BlogInformation = blogInfo,
                    Content = blogPostXML.ChildNodes.Item(Constants.BlogPostXMLContentIndex).InnerText,
                    Description = blogPostXML.ChildNodes.Item(Constants.BlogPostXMLDescriptionIndex).InnerText,
                    Link = blogPostXML.ChildNodes.Item(Constants.BlogPostXMLLinkIndex).InnerText,
                    PublicationDate = publicationDate,
                    Title = blogPostXML.ChildNodes.Item(Constants.BlogPostXMLTitleIndex).InnerText,
                };
                postList.Posts.Add(post);
                */
               
                postList.Posts.Add(new Post
                {
                    BlogInformation = blogInfo,                  
                    Content = blogPostXML.ChildNodes.Item(Constants.BlogPostXMLContentIndex).InnerText,
                    Description = blogPostXML.ChildNodes.Item(Constants.BlogPostXMLDescriptionIndex).InnerText,
                    Link = blogPostXML.ChildNodes.Item(Constants.BlogPostXMLLinkIndex).InnerText,
                    PublicationDate = Convert.ToDateTime(blogPostXML.ChildNodes.Item(Constants.BlogPostXMLPublicationDateIndex).InnerText),
                    Title = blogPostXML.ChildNodes.Item(Constants.BlogPostXMLTitleIndex).InnerText
                });
                
                string blogPostPubDateXMLNodeName =
                        blogPostXML.ChildNodes.Item(Constants.BlogPostXMLPublicationDateIndex).Name;
                string blogPostTitleXMLNodeName =
                        blogPostXML.ChildNodes.Item(Constants.BlogPostXMLTitleIndex).Name;
                string blogPostLinkXMLNodeName =
                        blogPostXML.ChildNodes.Item(Constants.BlogPostXMLLinkIndex).Name;
                string blogPostDescrXMLNodeName =
                        blogPostXML.ChildNodes.Item(Constants.BlogPostXMLDescriptionIndex).Name;
                string blogPostContentXMLNodeName =
                        blogPostXML.ChildNodes.Item(Constants.BlogPostXMLContentIndex).Name;               
            }

            return postList;
        }

        // Parse XML data from Word Press input URL, using Linq to XML
        public static PostList ParseXMLWordPressLinq(string inputUrl)
        {
            // Get XML string from Word Press blog 
            string xmlUrl = getXMLUrlWordPress(inputUrl);
            string xmlData = WebData.getWebData(xmlUrl);


            // Parse the blog XML data into a BlogXML object,
            //  by converting it to a stream and deserializing it
            PostList postList = null;
            using (var xmlStream = xmlData.ToStream())
            {
                
            }

            return postList;
        }

        // Parse XML data from Word Press input URL, using XMLSerialize
        public static BlogXML ParseXMLWordPressXMLSerialize(string inputUrl)
        {
            // Get XML string from Word Press blog 
            string xmlUrl = getXMLUrlWordPress(inputUrl);
            string xmlData = WebData.getWebData(xmlUrl);

            //Console.WriteLine("XML data:");
            //Console.WriteLine(xmlData);

            // Get the blog information and posts
            //var blogXML = new BlogXML();

            // Parse the blog XML data into a BlogXML object,
            //  by converting it to a stream and deserializing it
            var xmlSerializer = new XmlSerializer(typeof(BlogXML));
            using (var xmlStream = xmlData.ToStream())
            {
                var blogXML = (BlogXML)xmlSerializer.Deserialize(xmlStream);
                return blogXML;
            }
        }

        // Return the URL of the XML for the input URL of a Word Press blog
        // The XML URL is: <Word-Press-Url>/feed       
        private static string getXMLUrlWordPress(string inputUrl)
        {
            string XMLUrl;

            // Append 'feed' or '/feed', depending on whether input ends with '/'
            if (inputUrl[inputUrl.Length - 1] == '/')
            {
                XMLUrl = inputUrl + "feed";
            }
            else
            {
                XMLUrl = inputUrl + "/feed";
            }

            return XMLUrl;
        }
    }
}
