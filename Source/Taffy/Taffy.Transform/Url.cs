using System;
using System.Web;
using Taffy.Configuration;

namespace Taffy.Transform {
    public class Url {
        // gets the job done until an acceptable URL rewriting solution can be implemented for both IIS6 IIS7
        public static string GetFileUrl(string originalUrl, string destinationRelativeUrlBase) {
            var modifiedUrl = HttpContext.Current.Server.UrlEncode(originalUrl);
            return ConvertRelativeUrlToAbsoluteUrl(destinationRelativeUrlBase) + "?" + Constants.FileSourceParameterName + "=" + modifiedUrl;
        }

        // the value returned by this isn't servable from IIS7 out of the box, which doesn't send non-aspx URLs through the ASP.NET ISAPI module (and therefore doesn't execute Global.asax, which redirects the request to File.aspx)
        //private string GetFileUrlThatTakesAdvantageOfUrlRewriting(string originalUrl) {
        //    var modifiedUrl = originalUrl.Remove(originalUrl.IndexOf(":"), 2);
        //    return ConvertRelativeUrlToAbsoluteUrl("~" + Constants.FileUrlPrefix) + modifiedUrl;
        //}

        public static string ConvertRelativeUrlToAbsoluteUrl(string relativeUrl) {
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