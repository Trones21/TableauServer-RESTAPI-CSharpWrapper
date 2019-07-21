using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace TableauServerRESTAPIWrapper
{
   public static class GenericXMLParser<T>
   {
        public static List<T> ParseResponse(string Response, string TagName)
        {
            var ObjectList = new List<T>();
            try
            {
                var xDoc = new XmlDocument();
                xDoc.LoadXml(Response);

                var list = xDoc.DocumentElement.GetElementsByTagName(TagName);
                var serializer = new XmlSerializer(typeof(T));

                System.Collections.IEnumerator ienum = list.GetEnumerator();
                while (ienum.MoveNext())
                {
                    var XMLNode = (XmlNode)ienum.Current;
                    var Object = (T)serializer.Deserialize(new XmlNodeReader(XMLNode));
                    ObjectList.Add(Object);
                }
                return ObjectList;
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
