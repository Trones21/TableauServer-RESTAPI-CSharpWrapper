using System;
using System.Xml.Serialization;

namespace TableauServerRESTAPIWrapper.API_Response_Classes
{
    [Serializable, XmlRoot(Namespace = "http://tableau.com/api", ElementName = "site")]
    public class Site
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "contentUrl")]
        public string ContentUrl { get; set; }
        [XmlAttribute(AttributeName = "adminMode")]
        public string AdminMode { get; set; }
        [XmlAttribute(AttributeName = "userQuota")]
        public string UserQuota { get; set; }
        [XmlAttribute(AttributeName = "storageQuota")]
        public string StorageQuota { get; set; }
        [XmlAttribute(AttributeName = "disableSubscriptions")]
        public string DisableSubscriptions { get; set; }
        [XmlAttribute(AttributeName = "state")]
        public string State { get; set; }
        [XmlAttribute(AttributeName = "revisionHistoryEnabled")]
        public string RevisionHistoryEnabled { get; set; }
        [XmlAttribute(AttributeName = "revisionLimit")]
        public string RevisionLimit { get; set; }
        [XmlAttribute(AttributeName = "subscribeOthersEnabled")]
        public string SubscribeOthersEnabled { get; set; }
        [XmlAttribute(AttributeName = "guestAccessEnabled")]
        public string GuestAccessEnabled { get; set; }
        [XmlAttribute(AttributeName = "cacheWarmupEnabled")]
        public string CacheWarmupEnabled { get; set; }
        [XmlAttribute(AttributeName = "commentingEnabled")]
        public string CommentingEnabled { get; set; }
        [XmlAttribute(AttributeName = "flowsEnabled")]
        public string FlowsEnabled { get; set; }
    }
}