using System;
using System.Xml.Serialization;

namespace TableauServerRESTAPIWrapper.API_Response_Classes
{
    [Serializable, XmlRoot(Namespace = "http://tableau.com/api", ElementName = "domain")]
    public class Domain
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }
}
