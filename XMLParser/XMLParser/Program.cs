using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLParser.BlogData;
using XMLParser.BlogWordPress;
using XMLParser.Services;

namespace XMLParser
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // Input URL to read
            while (true)
            {
                Console.Write("Enter WordPress URL: ");
                string inputURL = Console.ReadLine();
                if (inputURL.Length == 0)
                {
                    break;
                }

                Console.WriteLine("Enter:");
                Console.WriteLine("1 - Parse with XMLDocument");
                Console.WriteLine("2 - Parse with XMLSerializer");
                Console.WriteLine("3 - Parse with LinqToXML");

                string parserChoice = Console.ReadLine();
                switch(parserChoice)
                {
                    case "1":
                        Console.WriteLine("Parsing with XMLDocument");
                        parseWithXMLDocument(inputURL);
                        break;
                    case "2":
                        Console.WriteLine("Parsing with XMLSerializer");
                        parseWithXMLSerializer(inputURL);
                        break;
                    case "3":
                        Console.WriteLine("Parsing with LinqToXML");
                        parseWithLinq(inputURL);
                        break;
                    default:
                        Console.WriteLine("Parsing with XMLDocument");
                        parseWithXMLDocument(inputURL);
                        break;
                }              
           }

            Console.ReadLine();
        }

        
        // Parse the XML at the input URL with XMLDocument
        public static void parseWithXMLDocument(string inputURL)
        {
            int i;

            // Parse the XML in the URL data
            try
            {
                PostList postList = ParseXMLWordPress.ParseXMLWordPressXMLDoc(inputURL);
              
                if (postList.Posts.Count() > 0)
                {
                    Console.WriteLine("# Blog Posts: {0}", postList.Posts.Count());
                    Console.WriteLine("---------------------------------------------------------------------------");
                    Console.WriteLine(" ");
                    i = 1;
                    foreach (Post post in postList.Posts)
                    {
                        Console.WriteLine("Post # {0}", i);
                        Console.WriteLine("Blog Title: {0}", post.BlogInformation.Title);
                        Console.WriteLine("Blog Link: {0}", post.BlogInformation.Link);
                        Console.WriteLine("Blog Description: {0}", post.BlogInformation.Description);
                        Console.WriteLine("Post Title: {0}", post.Title);
                        Console.WriteLine("Post Publication Date: {0}", post.PublicationDate);
                        Console.WriteLine("Post Link: {0}", post.Link);
                        Console.WriteLine("Post Description:");
                        Console.WriteLine(post.Description);
                        Console.WriteLine("Post Content:");
                        Console.WriteLine(post.Content);
                        Console.WriteLine("---------------------------------------------------------------------------");
                        Console.WriteLine(" ");

                        ++i;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        // Parse the XML at the input URL with Linq
        public static void parseWithLinq(string inputURL)
        {
            // Parse the XML in the URL data
            try
            {
                PostList postList = ParseXMLWordPress.ParseXMLWordPressLinq(inputURL);

                // Display the parsed blog data
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

        }

        // Parse the XML at the input URL with XMLSerializer
        public static void parseWithXMLSerializer(string inputURL)
        {
            // Parse the XML in the URL data
            try
            {
                BlogXML blog = ParseXMLWordPress.ParseXMLWordPressXMLSerialize(inputURL);

                // Display the parsed blog data
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

        }

    }


}
