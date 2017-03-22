using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Xml;

namespace MyHealth
{
    public static class WebPage
    {
        public static string LoadPage(string data)
        {
            string output = "";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);

            XmlNodeList documents = doc.SelectNodes("//document");

            if (documents.Count < 1)
            {
                output = "<h4>There are entries</h4>";
            }
            else
            {
                output = "<h4>There are " + documents.Count + " entries</h4>";
            }

            foreach (XmlNode node in documents)
            {
                output += "<p><b>Title: </b>" + node.SelectSingleNode("content[@name = 'title']").InnerText +"</p>";
                output += "<ul>" +
                          "<li><b>Summary: </b>" + node.SelectSingleNode("content[@name = 'FullSummary']").InnerText + "</li>" +
                          "<li><b>Link: </b>" + node.SelectSingleNode("@url").InnerText + "</li>" +
                          "</ul>";
            }

            return output;
        }
    }
}
