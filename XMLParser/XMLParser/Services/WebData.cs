using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace XMLParser.Services
{
    public class WebData
    {
        // Read data from input webUrl
        // Return result of read as string
        public static string getWebData(string webUrl)
        {
            // Read the data at the input URL
            using (WebClient webClient = new WebClient())
            {
                return webClient.DownloadString(webUrl);
            }            
        }
    }
}
