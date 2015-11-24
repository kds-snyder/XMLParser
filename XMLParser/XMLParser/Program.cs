using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLParser.BlogData;
using XMLParser.Services;

namespace XMLParser
{
    class Program
    {
        static void Main(string[] args)
        {
            int i;

            // Input URL to read
            while (true)
            {
                Console.Write("Enter WordPress URL: ");
                string inputURL = Console.ReadLine();
                if (inputURL.Length == 0)
                {
                    break;
                }


                // Parse the XML in the URL data
                try
                {
                    Blog blog = ParseXMLWordPress.ParseXMLWordPressData(inputURL);

                    // Display the blog data
                    Console.WriteLine("Blog Title: {0}", blog.Title);
                    Console.WriteLine("Blog Description: {0}", blog.Description);
                    Console.WriteLine("Blog Link: {0}", blog.Link);
                    Console.WriteLine("Blog Posts: {0}", blog.Posts.Count());
                    Console.WriteLine("---------------------------------------------------------------------------");
                    Console.WriteLine(" ");


                    if (blog.Posts.Count() > 0)
                    {
                        i = 1;
                        foreach (Post post in blog.Posts)
                        {
                            Console.WriteLine("Post # {0}", i);
                            Console.WriteLine("Publication date: {0}", post.PublicationDate);
                            Console.WriteLine("Title: {0}", post.Title);
                            Console.WriteLine("Link: {0}", post.Link);
                            Console.WriteLine("Description:");
                            Console.WriteLine(post.Description);
                            Console.WriteLine("Content:");
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

            Console.ReadLine();
        }
    }
}
