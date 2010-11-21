using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Taffy.Configuration;
using Taffy.Memory;
using Taffy.Transform;

namespace Taffy.Web {
    public class Global : HttpApplication {
        private static readonly IUrlyTransformer UrlyTransformer = new UrlyTransformer(new ApplicationCache());
        private static readonly List<string> IgnorableFileNames = new List<string> { Constants.FavIconFilename, Constants.WebResourceFilename };
        private static readonly IAccelerationPercentageTransformer AccelerationPercentageTransformer = new AccelerationPercentageTransformer();

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
                var webRequest = WebRequest.Create(Constants.ErrorFeedbackUrl);
                webRequest.Method = "POST";
                var postData = string.Format("message={0}&stacktrace={1}&asset={2}", message, stackTrace, asset);
                webRequest.Timeout = 1000 * Settings.ErrorFeedbackConnectionTimeoutInSeconds;
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = postData.Length;
                var newStream = webRequest.GetRequestStream();
                var data = new ASCIIEncoding().GetBytes(postData);
                newStream.Write(data, 0, data.Length);
                newStream.Close();
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
                var localPathWithinApplication = GetLocalPathWithinApplication(request);
                var podcastUrl = GetPodcastUrl(localPathWithinApplication);
                var accelerationPercentage = GetAccelerationPercentage(localPathWithinApplication);
                var fileUrl = Constants.FilePageVirtualPath + Constants.UrlQueryStringDesignator + Constants.FileSourceParameterName + Constants.QueryStringNameValuePairDelimiter + podcastUrl + Constants.QueryStringNameValuePairsDelimiter + Constants.AccelerationPercentageParameterName + Constants.QueryStringNameValuePairDelimiter + accelerationPercentage;
                httpContext.RewritePath(fileUrl);
            }
        }

        private static int GetAccelerationPercentage(string localPathWithinApplication) {
            var result = Constants.AccelerationPercentageDefault;
            var firstSlashIndex = localPathWithinApplication.IndexOf(Constants.UrlPathSeparator);
            var lastSlashIndex = localPathWithinApplication.LastIndexOf(Constants.UrlPathSeparator);
            if (firstSlashIndex != lastSlashIndex) {
                var firstAccelerationPercentageCharacterIndex = firstSlashIndex + 1;
                var length = lastSlashIndex - firstAccelerationPercentageCharacterIndex;
                var percent = localPathWithinApplication.Substring(firstAccelerationPercentageCharacterIndex, length);
                result = AccelerationPercentageTransformer.ParseAccelerationPercentage(percent);
            }
            return result;
        }

        private static string GetPodcastUrl(string localPathWithinApplication) {
            var firstSlashIndex = localPathWithinApplication.IndexOf(Constants.UrlPathSeparator);
            var urlKey = localPathWithinApplication.Substring(0, firstSlashIndex);
            var result = UrlyTransformer.Expand(urlKey);
            return result;
        }

        private static string GetLocalPathWithinApplication(HttpRequest request) {
            var localPath = request.Url.LocalPath;
            var startIndexWhenNotInVirtualDirectory = Constants.UrlPathSeparator.Length;
            var startIndexWhenInVirtualDirectory = request.ApplicationPath.Length + Constants.UrlPathSeparator.Length;
            var startIndex = request.ApplicationPath.Equals(Constants.UrlPathSeparator) ? startIndexWhenNotInVirtualDirectory : startIndexWhenInVirtualDirectory;
            return localPath.Substring(startIndex);
        }
    }
}