using System;
using System.Reflection;
using System.Xml;
using Elmah;
using Taffy.Configuration;
using Taffy.Data;
using Taffy.Memory;
using Taffy.Transform;

namespace Taffy.Web {
    public partial class CreateFeed : BasePage {
        private IUrlTransformer _urlTransformer;
        private IUserCache _userCache;
        protected const string PodcatcherTaffyAddressTextBoxWatermark = "http://example.com/Taffy/";
        protected const string UrlInputTextBoxWatermark = "http://example.com/path/to/rss_feed";
        private IAccelerationPercentageTransformer _accelerationPercentageTransformer;

        protected void Page_Init(object sender, EventArgs e) {
            _urlTransformer = new UrlTransformer(null);
            _userCache = new UserCache();
            _accelerationPercentageTransformer = new AccelerationPercentageTransformer();
            if (!IsPostBack) {
                SingleFeedAccelerationPercentageTextBox.Text = Constants.AccelerationPercentageDefault.ToString();
                OpmlFileAccelerationPercentageTextBox.Text = Constants.AccelerationPercentageDefault.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            RemoveWatermarks();
            if (IsPostBack) {
                HandlePostBack();
            }
        }

        private void HandlePostBack() {
            if (IsUrlAbsoluteAndValid(PodcatcherTaffyAddressTextBox.Text)) {
                if (OpmlFileUpload.HasFile) {
                    var accelerationPercentage = _accelerationPercentageTransformer.ParseAccelerationPercentage(OpmlFileAccelerationPercentageTextBox.Text);
                    ProcessOpmlFileUpload(accelerationPercentage);
                }
            }
            else {
                DisplayError(Messages.ErrorInvalidPodcatcherTaffyAddress, null);
            }
        }

        private void ProcessOpmlFileUpload(int accelerationPercentage) {
            try {
                _userCache.TaffyFeedOpml = GenerateTaffyOpmlFile(accelerationPercentage);
            }
            catch (Exception exception) {
                DisplayError(Messages.ErrorUnableToCreateOpmlFile, exception);
            }
        }

        private TaffyFeedOpml GenerateTaffyOpmlFile(int accelerationPercentage) {
            IXmlTransformer xmlTransformer = new XmlTransformer(_urlTransformer);
            var opmlXmlDocument = new XmlDocument();
            opmlXmlDocument.Load(OpmlFileUpload.FileContent);
            var transformedOpmlXmlDocument = xmlTransformer.GetTransformedOpmlXmlDocument(opmlXmlDocument, PodcatcherTaffyAddressTextBox.Text, accelerationPercentage);
            var result = new TaffyFeedOpml { Filename = Constants.OpmlFilenamePrefix + OpmlFileUpload.FileName, Contents = transformedOpmlXmlDocument.InnerXml };
            return result;
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
            try {
                UrlEncodeResults.Text = GetEncodedUrl();
            }
            catch (Exception exception) {
                DisplayError(Messages.ErrorUnableToCreateTaffyFeed, exception);
            }
        }

        private string GetEncodedUrl() {
            string result;
            var urlInputText = UrlInputTextBox.Text;
            if (IsUrlAbsoluteAndValid(urlInputText)) {
                var feedPathAbsoluteUrl = _urlTransformer.ConvertRelativeUrlToAbsoluteUrl("~/Feed.aspx");
                var urlEncodedUrl = Server.UrlEncode(urlInputText);
                var accelerationPercentage = _accelerationPercentageTransformer.ParseAccelerationPercentage(SingleFeedAccelerationPercentageTextBox.Text);
                result = feedPathAbsoluteUrl + Constants.UrlQueryStringDesignator + "source" + Constants.QueryStringNameValuePairDelimiter + urlEncodedUrl + Constants.QueryStringNameValuePairsDelimiter + Constants.AccelerationPercentageParameterName + Constants.QueryStringNameValuePairDelimiter + accelerationPercentage;
            }
            else if (string.Empty.Equals(urlInputText)) {
                result = string.Empty;
            }
            else {
                result = Messages.ErrorUrlNotWellFormed;
            }
            return result;
        }

        protected void OpmlDownloadLinkButton_Click(object sender, EventArgs e) {
            var taffyFeedOpml = _userCache.TaffyFeedOpml;
            try {
                ServeTaffyFeedOpmlFile(taffyFeedOpml.Filename, taffyFeedOpml.Contents);
            }
            catch (Exception exception) {
                DisplayError(Messages.ErrorUnableToServeFile, exception);
            }
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
