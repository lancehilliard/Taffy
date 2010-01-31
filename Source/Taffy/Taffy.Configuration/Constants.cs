namespace Taffy.Configuration {
    public class Constants {
        public const string ElmahFilename = "elmah.axd";
        public const string LameVirtualPathFileName = "~/tools/lame.exe";
        public const string SoundStretchVirtualPathFileName = "~/tools/soundstretch.exe";
        public const string Mpg123VirtualPathFileName = "~/tools/mpg123.exe";
        public const string WebResourceFilename = "WebResource.axd";
        public const TransformerTypes TransformerTypeDefault = TransformerTypes.CommandLine;
        public const int NumberOfHoursToCacheStretchedPodcasts = 0;
        public const string FavIconFilename = "favicon.ico";
        public const string RssUnknownLength = "0";
        public const string DebugUrlParameterName = "debug";
        public const string UrlQueryStringDelimiter = "?";
        public const string QueryStringNameValuePathDelimiter = "=";
        public const string ErrorMessageUnknownTransformerType = "Unknown transformer type.";
        public const string ResponseContentTypeHeaderName = "Content-Length";
        public const string Mp3ResponseContentDispositionHeaderValueTemplate = "attachment; filename={0}";
        public const string ResponseContentDispositionHeaderName = "Content-Disposition";
        public const string Mp3ResponseContentType = "audio/mpeg";
        public const string FilePageVirtualPath = "~/File.aspx";
        public const string FileSourceParameterName = "source";
        public const string UrlPathSeparator = "/";
    }
}