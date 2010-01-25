using System.IO;
using System.Net;
using System.Web;
using System.Xml;
using Taffy.Memory;

namespace Taffy.Transform {
    public class UrlyTransformer : IUrlyTransformer {
        private readonly IApplicationCache _applicationCache;
        private const string UrlyShortenApiUrlTemplate = "http://ur.ly/new.xml?href={0}";
        private const string UrlyExpandApiUrlTemplate = "http://ur.ly/{0}.xml";
        private const string UrlyCodeXmlAttributeName = "code";
        private const string UrlyCodeHrefAttributeName = "href";

        public UrlyTransformer(IApplicationCache applicationCache) {
            _applicationCache = applicationCache;
        }

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
            var result = _applicationCache.Get(apiUrl) as UrlyData;
            if (result == null) {
                var urlyXmlDocument = GetUrlyXmlDocument(apiUrl);
                if (urlyXmlDocument != null) {
                    var documentElement = urlyXmlDocument.DocumentElement;
                    if (documentElement != null) {
                        var code = documentElement.Attributes[UrlyCodeXmlAttributeName].Value;
                        var href = documentElement.Attributes[UrlyCodeHrefAttributeName].Value;
                        result = new UrlyData { Code = code, Href = href };
                        _applicationCache.Add(apiUrl, result);
                    }
                }
            }
            return result;
        }

        private static XmlDocument GetUrlyXmlDocument(string apiUrl) {
            var request = WebRequest.Create(apiUrl);
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();
            var streamReader = new StreamReader(responseStream);
            var xml = streamReader.ReadToEnd();
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            return xmlDocument;
        }

        private static string GetShortenApiUrl(string href) {
            var urlEncodedHref = HttpUtility.UrlEncode(href);
            return GetApiUrl(urlEncodedHref, UrlyShortenApiUrlTemplate);
        }

        private static string GetExpandApiUrl(string code) {
            return GetApiUrl(code, UrlyExpandApiUrlTemplate);
        }

        private static string GetApiUrl(string href, string apiUrlTemplate) {
            var result = string.Format(apiUrlTemplate, href);
            return result;
        }
    }

    public interface IUrlyTransformer {
        string Shorten(string href);
        string Expand(string code);
    }

    internal class UrlyData {
        public string Code { get; set; }
        public string Href { get; set; }
    }
}