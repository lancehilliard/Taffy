using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Xml;
using Taffy.Configuration;
using Taffy.Transform;

namespace Taffy.Web {
    public partial class Feed : Page {
        readonly Dictionary<string, string> XmlNamespacesToUseWhenSelectingNodes = new Dictionary<string, string> { { "media", "http://search.yahoo.com/mrss/" } };
        readonly List<string> XPathsOfNodesToRedirect = new List<string> { "//*/enclosure", "//*/media:content" };

        protected void Page_Load(object sender, EventArgs e) {
            var feedUrl = Request[Constants.FileSourceParameterName] ?? "http://feeds.thisamericanlife.org/talpodcast"; // "http://feeds.theonion.com/theonion/radionews";
            var feedXmlDocument = GetFeedXmlDocument(feedUrl);
            var transformedFeedXml = Xml.GetTransformedXmlDocument(feedXmlDocument, XmlNamespacesToUseWhenSelectingNodes, XPathsOfNodesToRedirect, "~/File.aspx");
            Response.Clear();
            transformedFeedXml.Save(Response.OutputStream);
            Response.End();
        }

        private static XmlDocument GetFeedXmlDocument(string feedUrl) {
            var feedXmlDocument = new XmlDocument();
            feedXmlDocument.Load(feedUrl);
            return feedXmlDocument;
        }
    }
}