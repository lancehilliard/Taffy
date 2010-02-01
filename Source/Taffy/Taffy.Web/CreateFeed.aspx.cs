using System;
using System.Xml;
using Taffy.Configuration;
using Taffy.Data;
using Taffy.Memory;
using Taffy.Transform;

namespace Taffy.Web {
    public partial class CreateFeed : System.Web.UI.Page {
        private IUrlTransformer _urlTransformer;
        private IUserCache _userCache;
        protected const string PodcatcherTaffyAddressTextBoxWatermark = "http://example.com/Taffy/";
        protected const string UrlInputTextBoxWatermark = "http://example.com/path/to/rss_feed";

        protected void Page_Init(object sender, EventArgs e) {
            _urlTransformer = new UrlTransformer(null);
            _userCache = new UserCache();
        }

        protected void Page_Load(object sender, EventArgs e) {
            RemoveWatermarks();
            if (OpmlFileUpload.HasFile) {
                TaffyFeedOpml result = null;
                IXmlTransformer xmlTransformer = new XmlTransformer(_urlTransformer);
                var opmlXmlDocument = new XmlDocument();
                opmlXmlDocument.Load(OpmlFileUpload.FileContent);
                var transformedOpmlXmlDocument = xmlTransformer.GetTransformedOpmlXmlDocument(opmlXmlDocument, PodcatcherTaffyAddressTextBox.Text);
                result = new TaffyFeedOpml { Filename = Constants.OpmlFilenamePrefix + OpmlFileUpload.FileName, Contents = transformedOpmlXmlDocument.InnerXml };
                _userCache.TaffyFeedOpml = result;
            }
        }

        private void RemoveWatermarks() {
            if (PodcatcherTaffyAddressTextBoxWatermark.Equals(PodcatcherTaffyAddressTextBox.Text)) {
                PodcatcherTaffyAddressTextBox.Text = string.Empty;
            }
            if (UrlInputTextBoxWatermark.Equals(UrlInputTextBox.Text)) {
                UrlInputTextBox.Text = string.Empty;
            }
        }

        protected void UrlEncodeButton_Click(object sender, EventArgs e) {
            string resultText;
            var urlInputText = UrlInputTextBox.Text;
            if (Uri.IsWellFormedUriString(urlInputText, UriKind.Absolute)) {
                var feedPathAbsoluteUrl = _urlTransformer.ConvertRelativeUrlToAbsoluteUrl("~/Feed.aspx");
                var urlEncodedUrl = Server.UrlEncode(urlInputText);
                resultText = feedPathAbsoluteUrl + "?source=" + urlEncodedUrl;
            }
            else if (string.Empty.Equals(urlInputText)) {
                resultText = string.Empty;
            }
            else {
                resultText = Messages.ErrorUrlNotWellFormed;
            }
            UrlEncodeResults.Text = resultText;
        }

        protected void OpmlDownloadLinkButton_Click(object sender, EventArgs e) {
            var taffyFeedOpml = _userCache.TaffyFeedOpml;
            ServeTaffyFeedOpmlFile(taffyFeedOpml.Filename, taffyFeedOpml.Contents);
        }

        private void ServeTaffyFeedOpmlFile(string filename, string contents) {
            var contentDisposition = string.Format(Constants.FilenameResponseContentDispositionHeaderValueTemplate, filename);
            var taffyFeedOpmlContents = contents;
            Response.Clear();
            Response.ContentType = Constants.DownloadResponseContentType;
            Response.AddHeader(Constants.ResponseContentDispositionHeaderName, contentDisposition);
            Response.AddHeader(Constants.ResponseContentLengthHeaderName, taffyFeedOpmlContents.Length.ToString());
            Response.Write(taffyFeedOpmlContents);
            Response.End();
        }

        protected void Page_PreRender(object sender, EventArgs e) {
            OpmlDownloadPanel.Visible = null != _userCache.TaffyFeedOpml;
        }

    }
}
