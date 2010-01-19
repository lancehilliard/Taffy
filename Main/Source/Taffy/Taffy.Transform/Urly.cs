using System;
using System.IO;
using System.Net;
using System.Web;
using System.Xml;

namespace Taffy.Transform {
    public class Urly {
        private const string UrlyShortenApiUrlTemplate = "http://ur.ly/new.xml?href={0}";
        private const string UrlyExpandApiUrlTemplate = "http://ur.ly/{0}.xml";

        public string Expand(string code) {
            var expandApiUrl = GetExpandApiUrl(code);
            var urlyData = GetUrlyData(expandApiUrl);
            return urlyData.Href;
        }

        public string Shorten(string href) {
            var shortenApiUrl = GetShortenApiUrl(href);
            var urlyData = GetUrlyData(shortenApiUrl);
            return urlyData.Code;
        }

        private UrlyData GetUrlyData(string apiUrl) {
            var urlyXmlDocument = GetUrlyXmlDocument(apiUrl);
            var code = urlyXmlDocument.DocumentElement.Attributes["code"].Value;
            var href = urlyXmlDocument.DocumentElement.Attributes["href"].Value;
            return new UrlyData{ Code = code, Href = href };
        }

        private XmlDocument GetUrlyXmlDocument(string apiUrl) {
            var request = WebRequest.Create(apiUrl);
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();
            var streamReader = new StreamReader(responseStream);
            var xml = streamReader.ReadToEnd();
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            return xmlDocument;
        }

        private string GetShortenApiUrl(string href) {
            var urlEncodedHref = HttpUtility.UrlEncode(href);
            return GetApiUrl(urlEncodedHref, UrlyShortenApiUrlTemplate);
        }

        private string GetExpandApiUrl(string code) {
            return GetApiUrl(code, UrlyExpandApiUrlTemplate);
        }

        private string GetApiUrl(string href, string apiUrlTemplate) {
            var result = string.Format(apiUrlTemplate, href);
            return result;
        }
    }

    internal class UrlyData {
        public string Code { get; set; }
        public string Href { get; set; }
    }
}