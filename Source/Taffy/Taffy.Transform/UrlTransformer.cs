using System;
using System.Web;
using Taffy.Configuration;

namespace Taffy.Transform {
    public class UrlTransformer : IUrlTransformer {
        private const string UrlPortPrefix = ":";
        private readonly IUrlyTransformer _urlyTransformer;

        public UrlTransformer(IUrlyTransformer urlyTransformer) {
            _urlyTransformer = urlyTransformer;
        }

        public string GetFileUrl(string originalUrl) {
            var originalUrlKey = _urlyTransformer.Shorten(originalUrl);
            var originalUrlFileName = GetOriginalUrlFileName(originalUrl);
            var fileRelativeUrl = GetRelativeUrl(originalUrlKey, originalUrlFileName);
            var result = ConvertRelativeUrlToAbsoluteUrl(fileRelativeUrl);
            return result;
        }

        private static string GetRelativeUrl(string originalUrlKey, string originalUriFileName) {
            var applicationPath = HttpContext.Current.Request.ApplicationPath;
            return applicationPath + Constants.UrlPathSeparator + originalUrlKey + Constants.UrlPathSeparator + originalUriFileName;
        }

        private static string GetOriginalUrlFileName(string originalUrl) {
            var originalUri = new Uri(originalUrl);
            var originalUriSegments = originalUri.Segments;
            var originalUriSegmentsCount = originalUriSegments.Length;
            return originalUriSegments[originalUriSegmentsCount - 1];
        }

        private static string ConvertRelativeUrlToAbsoluteUrl(string relativeUrl) {
            var uri = HttpContext.Current.Request.Url;
            var result = uri.Scheme + Uri.SchemeDelimiter + uri.Host + UrlPortPrefix + uri.Port + VirtualPathUtility.ToAbsolute(relativeUrl);
            return result;
        }

        public string GetFileName(string url) {
            var sourceUri = new Uri(url);
            var segments = sourceUri.Segments;
            var result = segments[segments.Length - 1];
            return result;
        }
    }

    public interface IUrlTransformer {
        string GetFileUrl(string originalUrl);
        string GetFileName(string url);
    }
}