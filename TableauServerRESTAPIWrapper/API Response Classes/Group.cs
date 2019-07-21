using System;
using System.Xml.Serialization;

namespace TableauServerRESTAPIWrapper.API_Response_Classes
{
    [Serializable, XmlRoot(Namespace = "http://tableau.com/api", ElementName = "group")]
    public class Group
    {
        [XmlElement(ElementName = "domain")]
        public Domain Domain { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }
}
