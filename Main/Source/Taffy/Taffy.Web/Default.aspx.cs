using System;
using Taffy.Configuration;
using Taffy.Transform;

namespace Taffy.Web {
    public partial class _Default : BasePage {
        private const string Yes = "Yes";
        private const string No = "No";
        protected void Page_Load(object sender, EventArgs e) {
            var mpg123Exists = System.IO.File.Exists(Settings.Mpg123FileName);
            var soundstretchExists = System.IO.File.Exists(Settings.SoundStretchFileName);
            var lameExists = System.IO.File.Exists(Settings.LameFileName);
            Mpg123ExistsLabel.Text = mpg123Exists ? Yes : No;
            SoundstretchExistsLabel.Text = soundstretchExists ? Yes : No;
            LameExistsLabel.Text = lameExists ? Yes : No;
            Mpg123ConfiguredPathLabel.Text = Settings.Mpg123FileName;
            SoundstretchConfiguredPathLabel.Text = Settings.SoundStretchFileName;
            LameConfiguredPathLabel.Text = Settings.LameFileName;
            NumberOfHoursToCacheStretchedPodcastsLabel.Text = Settings.NumberOfHoursToCacheStretchedPodcasts.ToString();
            TransformerTypeLabel.Text = Enum.GetName(typeof (TransformerTypes), Settings.TransformerType);
            IUrlTransformer urlTransformer = new UrlTransformer(null);
            var feedPathAbsoluteUrl = urlTransformer.ConvertRelativeUrlToAbsoluteUrl("~/Feed.aspx");
            var feedPageUriBuilder = new UriBuilder(feedPathAbsoluteUrl);
            const string sourceUrlVariableName = "rssFeedSourceUrl";
            feedPageUriBuilder.Query = "source=&lt;" + sourceUrlVariableName + "&gt;";
            FeedPageUrlLabel.Text = feedPageUriBuilder.ToString();
            SourceUrlHelpLabel.Text = "* &lt;" + sourceUrlVariableName + "&gt; must be URL-encoded.";
            const string sourceUrlExample = "http://feeds.theonion.com/theonion/radionews";
            feedPageUriBuilder.Query = "source=" + Server.UrlEncode(sourceUrlExample);
            SourceUrlExampleLabel.Text = sourceUrlExample;
            SourceUrlEncodedExampleLabel.Text = feedPageUriBuilder.ToString();


        }
    }
}