using System;
using System.Collections.Generic;
using System.Xml;
using Taffy.Configuration;
using Taffy.Memory;
using Taffy.Transform;

namespace Taffy.Web {
    public partial class Feed : BasePage {
        private IXmlTransformer _xmlTransformer;
        private readonly Dictionary<string, string> _xmlNamespacesToUseWhenSelectingNodes = new Dictionary<string, string> { { "media", "http://search.yahoo.com/mrss/" } };
        private readonly List<string> _xPathsOfNodesToRedirect = new List<string> { "//*/enclosure", "//*/media:content" };

        protected void Page_Init(object sender, EventArgs e) {
            IApplicationCache applicationCache = new ApplicationCache();
            IUrlyTransformer urlyTransformer = new UrlyTransformer(applicationCache);
            IUrlTransformer urlTransformer = new UrlTransformer(urlyTransformer);
            _xmlTransformer = new XmlTransformer(urlTransformer);
        }

        protected void Page_Load(object sender, EventArgs e) {
            var feedUrl = Request[Constants.FileSourceParameterName];
            if (feedUrl == null && IsDebugRequest()) {
                feedUrl = "http://www.npr.org/rss/podcast.php?id=510208"; // "http://feeds.theonion.com/theonion/radionews"; // "http://feeds.waywordradio.org/awwwpodcast"; // "http://www.pwop.com/feed.aspx?show=dotnetrocks&filetype=master"; // "http://feeds.thisamericanlife.org/talpodcast";
            }
            var feedXmlDocument = GetFeedXmlDocument(feedUrl);
            var transformedFeedXml = _xmlTransformer.GetTransformedXmlDocument(feedXmlDocument, _xmlNamespacesToUseWhenSelectingNodes, _xPathsOfNodesToRedirect, "~/File.aspx");
            Response.Clear();
            Response.ContentType = "application/xml";
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