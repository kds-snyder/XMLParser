using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLParser.BlogWordPress
{
    // Word press blog XML structure

    [XmlRootAttribute("rss")]
    public class BlogXML
    {        
        public Channel channel { get; set; }
    }

    public class Channel
    {
        public string Title { get; set; }

        [XmlElementAttribute("atom")]
        public string atom { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public string LastBuildDate { get; set; }

        public string Language { get; set; }

        public string UpdatePeriod { get; set; }

        public string UpdateFrequency { get; set; }

        public string Generator { get; set; }

        [XmlElementAttribute("cloud")]
        public Cloud cloud { get; set; }

        public ImageTag imageTag { get; set; } 
    }

    // <atom> attribute under <channel>
    public class Atom
    {
        [XmlAttributeAttribute()]
        public string Link { get; set; }

        [XmlAttributeAttribute()]
        public string Rel { get; set; }

        [XmlAttributeAttribute()]
        public string Type { get; set; }
    }

    // <cloud> attribute under <channel>
    public class Cloud
    {
        [XmlAttributeAttribute()]
        public string Domain { get; set; }

        [XmlAttributeAttribute()]
        public string Path { get; set; }

        [XmlAttributeAttribute()]
        public string RegisterProcedure { get; set; }
    }

    public class ImageTag
    {

    }

    // Word press blog post XML structure
    public class PostXML
    {
        public DateTime PublicationDate { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }

        public string Content { get; set; }
    }
}
