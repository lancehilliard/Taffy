using System;
using System.Text;
using System.Web;
using Taffy.Configuration;

namespace Taffy.Transform {
    public class Url {
        public static string GetFileUrl(string originalUrl, string destinationRelativeUrlBase) {
            var hexOriginalUrl = Hex.FromString(originalUrl);
            //var base64OriginalUrl = GetBase64OriginalUrl(originalUrl);
            var originalUrlFileName = GetOriginalUrlFileName(originalUrl);
            var fileRelativeUrl = GetRelativeUrl(hexOriginalUrl, originalUrlFileName);
            var result = ConvertRelativeUrlToAbsoluteUrl(fileRelativeUrl);
            return result;
        }

        private static string GetRelativeUrl(string base64OriginalUrl, string originalUriFileName) {
            var applicationPath = HttpContext.Current.Request.ApplicationPath;
            return applicationPath + "/" + base64OriginalUrl + "/" + originalUriFileName;
        }

        private static string GetBase64OriginalUrl(string originalUrl) {
            var originalUrlBytes = Encoding.ASCII.GetBytes(originalUrl);
            return Convert.ToBase64String(originalUrlBytes);
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