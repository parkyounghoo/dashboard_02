using KPC_Monitoring.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Open_Api_Collection_Module.Parser
{
    class XmlParser
    {
        public string getXml(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/xml; charset:utf-8";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36";

            string results = "";
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                Encoding encode;

                if (response.CharacterSet == "utf-8") { encode = Encoding.UTF8; }

                else { encode = Encoding.Default; }

                StreamReader reader = new StreamReader(response.GetResponseStream(), encode);
                results = reader.ReadToEnd();

                return results;
            }
        }

        public Dictionary<string, List<string>> getXmlList(string xml, List<TableModel> fieldList)
        {
            XDocument doc = XDocument.Parse(xml);
            List<TableModel> TableList = new List<TableModel>();

            foreach (var item in doc.Descendants())
            {
                for (int i = 0; i < fieldList.Count; i++)
                {
                    if (item.Name.ToString().ToLower() == fieldList[i].ColumnName.ToLower())
                    {
                        TableModel model = new TableModel();
                        model.ColumnName = fieldList[i].ColumnName;
                        model.Value = item.Value;

                        TableList.Add(model);
                    }
                }
            }

            Dictionary<string, List<string>> dicJson = new Dictionary<string, List<string>>();

            for (int i = 0; i < fieldList.Count; i++)
            {
                IEnumerable<string> itemList = from model in TableList
                                               where model.ColumnName == fieldList[i].ColumnName
                                               select model.Value;

                dicJson.Add(fieldList[i].ColumnName, itemList.ToList());
            }

            return dicJson;
        }
    }
}
