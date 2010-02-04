using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using Taffy.Configuration;
using Taffy.Memory;
using Taffy.Transform;

namespace Taffy.Web {
    public class Global : HttpApplication {
        private static readonly IUrlyTransformer UrlyTransformer = new UrlyTransformer(new ApplicationCache());
        private static readonly List<string> IgnorableFileNames = new List<string> { Constants.FavIconFilename, Constants.WebResourceFilename };

        protected void Application_Error(object sender, EventArgs e) {
            if (Settings.ErrorFeedbackEnabled) {
                var exception = Server.GetLastError().GetBaseException();
                var asset = Request[Constants.FileSourceParameterName] ?? Request.RawUrl;
                SendErrorFeedback(exception, asset);
            }
        }

        private void SendErrorFeedback(Exception exception, string asset) {
            try {
                var message = Server.UrlEncode(exception.Message);
                var stackTrace = Server.UrlEncode(exception.StackTrace);
                asset = Server.UrlEncode(asset);
                var errorFeedbackUrl = string.Format(Constants.ErrorFeedbackUrlTemplate, message, stackTrace, asset);
                var webRequest = WebRequest.Create(errorFeedbackUrl);
                webRequest.Timeout = 1000 * Settings.ErrorFeedbackConnectionTimeoutInSeconds;
                webRequest.GetResponse();
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e) {
            RewriteUrl();
        }

        private static void RewriteUrl() {
            var httpContext = HttpContext.Current;
            var request = httpContext.Request;
            var path = request.Path;
            var pathFileName = VirtualPathUtility.GetFileName(path);
            var isRequestForDefaultDocument = request.ApplicationPath.Equals("/" + pathFileName);
            if (!path.Contains(Constants.ElmahFilename) && !IgnorableFileNames.Contains(pathFileName) && !isRequestForDefaultDocument && !System.IO.File.Exists(HttpContext.Current.Server.MapPath(pathFileName))) {
                var podcastUrl = GetPodcastUrl(request);
                var fileUrl = Constants.FilePageVirtualPath + Constants.UrlQueryStringDesignator + Constants.FileSourceParameterName + Constants.QueryStringNameValuePairDelimiter + podcastUrl;
                httpContext.RewritePath(fileUrl);
            }
        }

        private static string GetPodcastUrl(HttpRequest request) {
            var localPath = request.Url.LocalPath;
            var startIndexWhenNotInVirtualDirectory = Constants.UrlPathSeparator.Length;
            var startIndexWhenInVirtualDirectory = request.ApplicationPath.Length + Constants.UrlPathSeparator.Length;
            var startIndex = request.ApplicationPath.Equals(Constants.UrlPathSeparator) ? startIndexWhenNotInVirtualDirectory : startIndexWhenInVirtualDirectory;
            var localPathWithinApplication = localPath.Substring(startIndex);
            var lastSlashIndex = localPathWithinApplication.LastIndexOf(Constants.UrlPathSeparator);
            var urlKey = localPathWithinApplication.Substring(0, lastSlashIndex);
            var result = UrlyTransformer.Expand(urlKey);
            return result;
        }
    }
}