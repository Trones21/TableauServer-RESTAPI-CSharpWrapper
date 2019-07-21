using System;
using System.IO;
using System.Net;
using System.Text;
using SignInResponseClasses;
using Newtonsoft.Json;

namespace TableauServerRESTAPIWrapper
{
    public class TableauServerConnection
    {
       public string APIVersion { get; set; }
       public string Username { get; set; }
       public string Password { get; set; }
       public string server { get; set; }
       public string siteName { get; set; }
       public string AuthToken { get; set; }
       public WebClient client = new WebClient();

        public TableauServerConnection(string server, string siteName, string APIVersion, string Username, string Password)
        {
            this.server = server;
            this.siteName = siteName;
            this.APIVersion = APIVersion;
            this.Username = Username;
            this.Password = Password;
        }

        public bool SignIn()
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://" + server + "/api/" + APIVersion + "/auth/signin");

                string body = String.Format(@"{{
                              ""credentials"": {{
                                            ""name"": ""{0}"",
                                            ""password"": ""{1}"",
                                            ""site"": {{
                                            ""contentUrl"": ""{2}""
                                                      }}
                                                }}
                                    }}", Username, Password, siteName);

                var data = Encoding.ASCII.GetBytes(body);

                request.ContentType = "application/json";
                request.Accept = "application/json";
                request.Method = "POST";
                request.ContentLength = data.Length;

                var newStream = request.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();

                WebResponse response = request.GetResponse();
                Stream datastream = response.GetResponseStream();

                StreamReader reader = new StreamReader(datastream);
                string responseString = reader.ReadToEnd();

                RootObject signInResponse = JsonConvert.DeserializeObject<RootObject>(responseString);
                this.AuthToken = signInResponse.credentials.token;
                this.client.Headers.Add("x-Tableau-Auth" + ":" + this.AuthToken);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }


}