using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using SignInResponseClasses;
using TableauServerRESTAPIWrapper.API_Response_Classes;
using System.Reflection;
using CsvHelper;

namespace TableauServerRESTAPIWrapper.Examples
{
    public static class GetADGroupsandUserInfo
    {
        static void Main(string[] args)
        {
            //https://onlinehelp.tableau.com/v2019.1/api/rest_api/en-us/REST/rest_api_ref.htm
            string APIVersion = "";
            string Username = "";
            string Password = "";
            string server = "";
            string siteName = ""; //This is only needed to Authenticate, you can then use any site you are an admin of

            var tsConn = new TableauServerConnection(server, siteName, APIVersion, Username, Password);

            GetUserandADGroupsInfo(tsConn);
        }

        static void GetUserandADGroupsInfo(TableauServerConnection tsConn)
        {
           
            if (tsConn.SignIn())
            {
                //Get Site ID
                var siteID = GetSiteID(tsConn, tsConn.siteName);

                //Get_Group IDs
                var groups_response = callAPI(tsConn, "/sites/" + siteID + "/groups/");
                var groups = GenericXMLParser<Group>.ParseResponse(groups_response, "group");
                var filteredGroups = groups.Where(g => g.Name != "All Users").ToList();

                var userInfo_Plus = new List<UserInfo_Plus>();
                foreach (var group in filteredGroups)
                {
                    //Get Users --Use page-size param, default 100,  max 1000 users - if any group has > 1000 then rewrite code
                    var users_response = callAPI(tsConn, "/sites/" + siteID + "/groups/" + group.Id + "/users?pageSize=1000&pageNumber=1");
                    var users = GenericXMLParser<UserInfo_ViaGroupCall>.ParseResponse(users_response, "user");
                    var query = from user in users
                                select new UserInfo_Plus
                                {
                                    Id = user.Id,
                                    Name = user.Name,
                                    SiteRole = user.SiteRole,
                                    ADGroup = @group.Name
                                };
                    var result = query.ToList();
                    userInfo_Plus.AddRange(result);
                }

                //Get UserInfo_Full for entire site
                if (GetPageCount(tsConn, "/sites/" + siteID + "/users?pageSize=1000") > 1)
                {
                    throw new NotImplementedException("Logic not implemented for Sites with >1000 users");
                }
                else
                {
                    var userinfo_Full_response = callAPI(tsConn, "/sites/" + siteID + "/users?pageSize=1000&pageNumber=1");
                    var fullUsers = GenericXMLParser<UserInfo_Full>.ParseResponse(userinfo_Full_response, "user");

                    //Merge CWIDS into MyUser class
                    var query = from uip in userInfo_Plus
                                join fu in fullUsers
                                on uip.Id equals fu.Id
                                select new UserInfo_Plus
                                {
                                    Id = uip.Id,
                                    ADGroup = uip.ADGroup,
                                    Name = uip.Name,
                                    SiteRole = uip.SiteRole,
                                    CWID = uip.Name,
                                    LastLogin = fu.LastLogin
                                };

                    var userlist = query.ToList();
                    
                    using (var writer = new StreamWriter("output.csv"))
                    using (var csv = new CsvWriter(writer))
                    {
                        csv.WriteRecords(userlist);
                    }
                }              
            }
            
        }

        public static string GetSiteID(TableauServerConnection tsConn, string siteName)
        {
            var sites_response = callAPI(tsConn, "/sites/");
            var sites = GenericXMLParser<Site>.ParseResponse(sites_response, "site");
            return sites.Where(s => s.Name == siteName).First().Id;
        }
        
        public static string callAPI(TableauServerConnection tsconn, string endpoint)
        {
             return tsconn.client.DownloadString("https://" + tsconn.server + "/api/" + tsconn.APIVersion + endpoint);           
        }

        public static int GetPageCount(TableauServerConnection tsconn, string endpoint)
        {
            var response = tsconn.client.DownloadString("https://" + tsconn.server + "/api/" + tsconn.APIVersion + endpoint);
            var xDoc = new XmlDocument();
            xDoc.LoadXml(response);

            var pagination = xDoc.DocumentElement.GetElementsByTagName("pagination")[0];
            return int.Parse(pagination.Attributes.GetNamedItem("pageNumber").InnerText);

        }
    }
}
