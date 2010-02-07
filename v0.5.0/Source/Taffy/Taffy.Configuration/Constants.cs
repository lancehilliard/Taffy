using System;
using System.Reflection;

namespace Taffy.Configuration {
    public class Constants {
        public const bool ErrorFeedbackEnabledDefault = false;
        public const int ErrorFeedbackConnectionTimeoutInSecondsDefault = 5;
        public const string ErrorFeedbackUrlTemplate = "http://taffy.digitalcreations.cc/error_service.aspx?message={0}&stacktrace={1}&asset={2}";
        public const string AlertScriptKey = "AlertScript";
        public const string OpmlFilenamePrefix = "Taffy_";
        public const string DownloadResponseContentType = "application/octet-stream";
        public const string XmlResponseContentType = "application/xml";
        public const string DependencyDownloadsUrl = "http://taffy.codeplex.com/wikipage?title=DependencyDownloads";
        public const string FeedFileName = "Feed.aspx";
        public const string ElmahFilename = "elmah.axd";
        public const string LameVirtualPathFileName = "~/tools/lame.exe";
        public const string SoundStretchVirtualPathFileName = "~/tools/soundstretch.exe";
        public const string Mpg123VirtualPathFileName = "~/tools/mpg123.exe";
        public const string WebResourceFilename = "WebResource.axd";
        public const TransformerTypes TransformerTypeDefault = TransformerTypes.CommandLine;
        public const int NumberOfHoursToCacheStretchedPodcastsDefault = 0;
        public const string FavIconFilename = "favicon.ico";
        public const string RssUnknownLength = "0";
        public const string DebugUrlParameterName = "debug";
        public const string UrlQueryStringDesignator = "?";
        public const string QueryStringNameValuePairDelimiter = "=";
        public const string ErrorMessageUnknownTransformerType = "Unknown transformer type.";
        public const string ResponseContentLengthHeaderName = "Content-Length";
        public const string FilenameResponseContentDispositionHeaderValueTemplate = "attachment; filename={0}";
        public const string ResponseContentDispositionHeaderName = "Content-Disposition";
        public const string Mp3ResponseContentType = "audio/mpeg";
        public const string FilePageVirtualPath = "~/File.aspx";
        public const string FileSourceParameterName = "source";
        public const string UrlPathSeparator = "/";
        public const string AssemblyVersion = "0.5.*";
        public const string AssemblyFileVersion= "0.5.0.0";
        public const bool IsBetaRelease = true;

        public const string BuildDateMessageDateFormatString = "yyyyMMddHHmm";
    }
}