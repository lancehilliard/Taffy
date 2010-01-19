using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Taffy.Configuration;
using Taffy.Transform;

namespace Taffy.Web {
    public class Global : System.Web.HttpApplication {
        private static readonly Urly Urly = new Urly();

        protected void Application_Start(object sender, EventArgs e) {

        }

        protected void Session_Start(object sender, EventArgs e) {

        }

        protected void Application_BeginRequest(object sender, EventArgs e) {
            RewriteUrl();
        }

        private static void RewriteUrl() {
            var httpContext = HttpContext.Current;
            var request = httpContext.Request;
            var path = request.Path;
            var pathFileName = VirtualPathUtility.GetFileName(path);
            if (!pathFileName.Equals(Constants.FeedPageFileName)) {
                var podcastUrl = GetPodcastUrl(request);
                var fileUrl = Constants.FilePageVirtualPath + "?" + Constants.FileSourceParameterName + "=" + podcastUrl;
                httpContext.RewritePath(fileUrl);
            }

            //if (path.Contains(Settings.FOO)) {
            //    string newPath = path.Replace(Settings.FOO, Constants.FOO);
            //    httpContext.RewritePath(newPath, true);
            //}
        }

        private static string GetPodcastUrl(HttpRequest request) {
            var localPath = request.Url.LocalPath;
            var startIndex = request.ApplicationPath.Equals("/") ? 1 : request.ApplicationPath.Length + 1;
            var localPathWithinApplication = localPath.Substring(startIndex);
            var lastSlashIndex = localPathWithinApplication.LastIndexOf("/");
            var urlKey = localPathWithinApplication.Substring(0, lastSlashIndex);
            var url = Urly.Expand(urlKey);
            //var fileName = localPath.Substring(lastSlashIndex + 1);
            //var decodedUrlBytes = Convert.FromBase64String(base64Url);
            //var decodedUrlString = Encoding.ASCII.GetString(decodedUrlBytes);
            var result = url;
            System.IO.File.WriteAllText(@"C:\foo.txt", result);
            return result;
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e) {

        }

        protected void Application_Error(object sender, EventArgs e) {

        }

        protected void Session_End(object sender, EventArgs e) {

        }

        protected void Application_End(object sender, EventArgs e) {

        }
    }
}