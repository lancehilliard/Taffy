using System;
using System.Text;
using System.Web;
using Taffy.Configuration;

namespace Taffy.Transform {
    public class Url {
        static readonly Urly Urly = new Urly();

        public static string GetFileUrl(string originalUrl) {
            var originalUrlKey = Urly.Shorten(originalUrl);
            var originalUrlFileName = GetOriginalUrlFileName(originalUrl);
            var fileRelativeUrl = GetRelativeUrl(originalUrlKey, originalUrlFileName);
            var result = ConvertRelativeUrlToAbsoluteUrl(fileRelativeUrl);
            return result;
        }

        private static string GetRelativeUrl(string originalUrlKey, string originalUriFileName) {
            var applicationPath = HttpContext.Current.Request.ApplicationPath;
            return applicationPath + "/" + originalUrlKey + "/" + originalUriFileName;
        }

        private static string GetOriginalUrlFileName(string originalUrl) {
            var originalUri = new Uri(originalUrl);
            var originalUriSegments = originalUri.Segments;
            var originalUriSegmentsCount = originalUriSegments.Length;
            return originalUriSegments[originalUriSegmentsCount - 1];
        }

        private static string ConvertRelativeUrlToAbsoluteUrl(string relativeUrl) {
            var uri = HttpContext.Current.Request.Url;
            var result = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port + VirtualPathUtility.ToAbsolute(relativeUrl);
            return result;
        }

        public static string GetFileName(string url) {
            var sourceUri = new Uri(url);
            var segments = sourceUri.Segments;
            var result = segments[segments.Length - 1];
            return result;
        }
    }
}