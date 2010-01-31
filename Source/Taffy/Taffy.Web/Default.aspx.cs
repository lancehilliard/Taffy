using System;
using System.Diagnostics;
using Taffy.Configuration;
using Taffy.Transform;

namespace Taffy.Web {
    public partial class _Default : BasePage {
        private string _feedPathAbsoluteUrl;
        protected void Page_Load(object sender, EventArgs e) {
            LameConfiguredPathLabel.Text = Settings.LameFileName;
            IUrlTransformer urlTransformer = new UrlTransformer(null);
            _feedPathAbsoluteUrl = urlTransformer.ConvertRelativeUrlToAbsoluteUrl("~/Feed.aspx");
            var feedPageUriBuilder = new UriBuilder(_feedPathAbsoluteUrl);
            const string sourceUrlVariableName = "rssFeedSourceUrl";
            feedPageUriBuilder.Query = "source=&lt;" + sourceUrlVariableName + "&gt;";
            FeedPageUrlLabel.Text = feedPageUriBuilder.ToString();
            SourceUrlHelpLabel.Text = "* &lt;" + sourceUrlVariableName + "&gt; must be URL-encoded.";
            const string sourceUrlExample = "http://feeds.theonion.com/theonion/radionews";
            feedPageUriBuilder.Query = "source=" + Server.UrlEncode(sourceUrlExample);
            SourceUrlExampleLabel.Text = sourceUrlExample;
            SourceUrlEncodedExampleLabel.Text = feedPageUriBuilder.ToString();
            const string dependenciesDownloadUrl = "http://taffy.codeplex.com/wikipage?title=DependencyDownloads";
            LameDownloadHyperLink.NavigateUrl = dependenciesDownloadUrl;
            //var mpg123Exists = System.IO.File.Exists(Settings.Mpg123FileName);
            //var soundstretchExists = System.IO.File.Exists(Settings.SoundStretchFileName);
            var lameMissing = !System.IO.File.Exists(Settings.LameFileName);
            LameNotInstalledContainer.Visible = lameMissing;
            //Mpg123ExistsLabel.Text = mpg123Exists ? Yes : No;
            //SoundstretchExistsLabel.Text = soundstretchExists ? Yes : No;
            //Mpg123ConfiguredPathLabel.Text = Settings.Mpg123FileName;
            //SoundstretchConfiguredPathLabel.Text = Settings.SoundStretchFileName;
            //Mpg123DownloadHyperLink.NavigateUrl = dependenciesDownloadUrl;
            //SoundstretchDownloadHyperLink.NavigateUrl = dependenciesDownloadUrl;
            //LameExistsLabel.Text = lameExists ? Yes : No;
            //NumberOfHoursToCacheStretchedPodcastsLabel.Text = Settings.NumberOfHoursToCacheStretchedPodcasts.ToString();
            //TransformerTypeLabel.Text = Enum.GetName(typeof(TransformerTypes), Settings.TransformerType);
        }

        protected void UrlEncodeButton_Click(object sender, EventArgs e) {
            string resultText;
            var urlInputText = UrlInputTextBox.Text;
            if (Uri.IsWellFormedUriString(urlInputText, UriKind.Absolute)) {
                var urlEncodedUrl = Server.UrlEncode(urlInputText);
                resultText = _feedPathAbsoluteUrl + "?source=" + urlEncodedUrl;
            }
            else {
                resultText = Messages.ErrorUrlNotWellFormed;
            }
            UrlEncodeResults.Text = resultText;
        }
    }
}