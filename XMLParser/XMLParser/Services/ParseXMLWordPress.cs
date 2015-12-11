using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
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
            BlogInfo blogInfo = getBlogInfoXMLDoc(xmlData);

            PostList postList = getBlogPostsXMLDoc(xmlData, blogInfo);

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
        private static BlogInfo getBlogInfoXMLDoc(XmlDocument xmlData)
        {
            var blogInfo = new BlogInfo();

            // Get the blog information XML node
            XmlNode blogInfoXMLNode = xmlData.SelectSingleNode(Constants.BlogInfoXMLNode);

            // Set the blog information in the blog info object
            blogInfo.Title = blogInfoXMLNode.ChildNodes.Item(Constants.BlogInfoXMLTitleIndex).InnerText;
            blogInfo.Description = blogInfoXMLNode.ChildNodes.Item(Constants.BlogInfoXMLDescriptionIndex).InnerText;
            blogInfo.Link = blogInfoXMLNode.ChildNodes.Item(Constants.BlogInfoXMLLinkIndex).InnerText;

            /*
            string blogInfoXMLNodeName = blogInfoXMLNode.Name;
            string blogInfoTitleXMLNodeName = blogInfoXMLNode.ChildNodes.Item(Constants.BlogInfoXMLTitleIndex).Name;
            string blogInfoDescrXMLNodeName = blogInfoXMLNode.ChildNodes.Item(Constants.BlogInfoXMLDescriptionIndex).Name;
            string blogInfoLinkXMLNodeName = blogInfoXMLNode.ChildNodes.Item(Constants.BlogInfoXMLLinkIndex).Name;
            */

            return blogInfo;
        }

        // Get the blog posts from the XML data via XMLDocument,
        //  and return a PostList object
        private static PostList getBlogPostsXMLDoc(XmlDocument xmlData, BlogInfo blogInfo)
        {
            var postList = new PostList();

            // Get the list of blog post XML nodes
            XmlNodeList blogPostXMLNodes = xmlData.SelectNodes(Constants.BlogPostXMLNode); 

            // Set up a post object from each blog post data
            foreach (XmlNode blogPostXML in blogPostXMLNodes)
            {              
                postList.Posts.Add(new Post
                {
                    BlogInformation = blogInfo,                  
                    Content = blogPostXML.ChildNodes.Item(Constants.BlogPostXMLContentIndex).InnerText,
                    Description = blogPostXML.ChildNodes.Item(Constants.BlogPostXMLDescriptionIndex).InnerText,
                    Link = blogPostXML.ChildNodes.Item(Constants.BlogPostXMLLinkIndex).InnerText,
                    PublicationDate = Convert.ToDateTime(blogPostXML.ChildNodes.Item(Constants.BlogPostXMLPublicationDateIndex).InnerText),
                    Title = blogPostXML.ChildNodes.Item(Constants.BlogPostXMLTitleIndex).InnerText
                });
                
                /*
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
                */              
            }

            return postList;
        }

        // Parse XML data from Word Press input URL, using Linq to XML
        public static PostList ParseXMLWordPressLinqXML(string inputUrl)
        {
            // Get XML string from Word Press blog 
            string xmlUrl = getXMLUrlWordPress(inputUrl);
            string xmlData = WebData.getWebData(xmlUrl);


            // Parse the blog XML data 
            //  Convert the XML data string to a stream and load it into an XML document
            //  Get the blog information and the posts            
            using (var xmlStream = xmlData.ToStream())
            {
                XDocument xmlDoc = XDocument.Load(xmlStream);

                BlogInfo blogInfo = getBlogInfoLinqXML(xmlDoc);
                /*
                BlogInfo blogInfo = new BlogInfo
                {
                    Title = "Blog title",
                    Description = "Blog description",
                    Link = "Blog link"
                };
                */

                PostList postList = getBlogPostsLinqXML(xmlDoc, blogInfo);

                return postList;
            }          
        }

        // Get the blog information from the XML data via Linq to XML,
        //  and set it in the returned blog info object
        private static BlogInfo getBlogInfoLinqXML(XDocument xmlDoc)
        {
            var blogInfo = new BlogInfo();

            // Get the blog information XML node
            var blogInfoXML = xmlDoc.Element("rss").Element("channel");

            // Set the blog information in the blog info object   
            blogInfo.Description = blogInfoXML.Element("description").Value;
            blogInfo.Link = blogInfoXML.Element("link").Value;
            blogInfo.Title = blogInfoXML.Element("title").Value;                   

            return blogInfo;
        }

        // Get the blog posts from the XML data via Linq to XML,
        //  and return a PostList object
        private static PostList getBlogPostsLinqXML(XDocument xmlDoc, BlogInfo blogInfo)
        {
            var postList = new PostList();

            //XNamespace contentNameSpace = "http://purl.org/rss/1.0/modules/content/";
            XNamespace contentNameSpace = getContentNameSpace(xmlDoc);

            var docNameSpace = xmlDoc.Root.Name.Namespace;           
            

            // Get the blog posts from the XML document
            // They are in <item> tags, which are under <channel> under <rss>           
            postList.Posts =
                xmlDoc.Element("rss").Element("channel").Descendants("item").Select(post => new Post
                //xmlDoc.Descendants("item").Select(post => new Post                
                {
                    BlogInformation = blogInfo,
                    Content = post.Element(contentNameSpace + "encoded").Value,
                    Description = post.Element("description").Value,
                    Link = post.Element("link").Value,
                    PublicationDate = Convert.ToDateTime(post.Element("pubDate").Value),
                    Title = post.Element("title").Value
                }).ToList();

            /*
            var blogPosts = xmlDoc.Descendants("item");
            foreach (var post in blogPosts)
            {
                var newPost = new Post();

                newPost.BlogInformation = blogInfo;  

                XElement contentElement = post.Element(contentNameSpace + "encoded");
                newPost.Content = post.Element(contentNameSpace + "encoded").Value;
                             
                newPost.Link = post.Element("link").Value;
                newPost.PublicationDate = Convert.ToDateTime(post.Element("pubDate").Value);
                newPost.Title = post.Element("title").Value;
                newPost.Description = post.Element("description").Value;               

                postList.Posts.Add(newPost);
            }
            */

            return postList;
        }

        // Get the content namespace specified in the input XML document,
        // from the <rss> tag, attribute xmlns:content
        // If xmlns:content not found, use default value
        private static XNamespace getContentNameSpace(XDocument xmlDoc)
        {
            XNamespace contentNameSpace = "http://purl.org/rss/1.0/modules/content/";

            var contentAttrib = xmlDoc.Element("rss").Attributes()
                            .Where(attrib => attrib.IsNamespaceDeclaration &&
                                   attrib.Name.LocalName == "content").FirstOrDefault();

            if (contentAttrib != null) contentNameSpace = contentAttrib.Value;

            return contentNameSpace;
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
