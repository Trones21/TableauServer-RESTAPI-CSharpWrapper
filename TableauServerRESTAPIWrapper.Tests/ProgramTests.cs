using Microsoft.VisualStudio.TestTools.UnitTesting;
using TableauServerRESTAPIWrapper.Examples;
using TableauServerRESTAPIWrapper.API_Response_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using CsvHelper;

namespace TableauServerRESTAPIWrapper.Tests
{

    [TestClass()]
    public class ProgramTests
    {
        public static TableauServerConnection SignIn()
        {
            string APIVersion = "";
            string Username = "";
            string Password = "";
            string server = "";
            string siteName = "";

            var tsConn = new TableauServerConnection(server, siteName, APIVersion, Username, Password);
            if (tsConn.SignIn())
            {
                return tsConn;
            }
            else
            {
                throw new Exception("Sign in to Tableau Server failed");
            }
        }
        [TestMethod()]
        public void GetPageCountTest()
        {
            var tsConn = ProgramTests.SignIn();

            //Get Site ID
            var siteID = GetADGroupsandUserInfo.GetSiteID(tsConn, "");

            //Act - Assert
            var pagecount = GetADGroupsandUserInfo.GetPageCount(tsConn, "/sites/" + siteID + "/users?pagesize=1000");
            Console.WriteLine(pagecount);
        }

        [TestMethod()]
        public void GenericParseResponseTest()
        {
            var tsConn = ProgramTests.SignIn();
            var response = GetADGroupsandUserInfo.callAPI(tsConn, "/sites/");
            var tagname = "site";

            var ObjectList = GenericXMLParser<Site>.ParseResponse(response, tagname);
            Console.WriteLine("Set Inspect BP Here");
         }


        [TestMethod()]
        public void CSVWriteTest()
        {
            //Arrange
            var userList = new List<UserInfo_Plus>()
        {
            new UserInfo_Plus() {Name = "Bob", ADGroup = "123"},
            new UserInfo_Plus() {Name = "Sally", ADGroup = "4"}
        };

            //Act
            using (var writer = new StreamWriter("output.csv"))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(userList);
            }
        }
    }
}