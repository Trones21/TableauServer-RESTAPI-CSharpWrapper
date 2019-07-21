using System;
using System.Xml.Serialization;

namespace TableauServerRESTAPIWrapper.API_Response_Classes
{
    [Serializable, XmlRoot(Namespace = "http://tableau.com/api", ElementName = "user")]
    public class UserInfo_Full
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "siteRole")]
        public string SiteRole { get; set; }
        [XmlAttribute(AttributeName = "lastLogin")]
        public string LastLogin { get; set; }
        [XmlAttribute(AttributeName = "externalAuthUserId")]
        public string ExternalAuthUserId { get; set; }
        [XmlAttribute(AttributeName = "authSetting")]
        public string AuthSetting { get; set; }
    }
}
