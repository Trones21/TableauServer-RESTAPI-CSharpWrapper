using System;
using System.Xml.Serialization;

namespace TableauServerRESTAPIWrapper.API_Response_Classes
{
    [Serializable, XmlRoot(Namespace = "http://tableau.com/api", ElementName = "user")]
    public class UserInfo_ViaGroupCall
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "siteRole")]
        public string SiteRole { get; set; }

    }
    /// <summary>
    /// Normally the logic would be kept in a DB, but since this app in DB free, we will just duplicate the user when they are in multiple groups, 
    /// and grab the extra attributes via LINQ join with UserInfo_Full class
    /// Technical Debt Note: We could also create a custom user class that has List<Group>, but this is easier for now
    /// </summary>
    public class UserInfo_Plus : UserInfo_ViaGroupCall
    {
        public string ADGroup { get; set; }
        public string LastLogin { get; set; }
        public string CWID { get; set; }
    }
}
