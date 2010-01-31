using System;
using System.Collections.Generic;
using System.Web;
using Taffy.Configuration;
using Taffy.Memory;
using Taffy.Transform;

namespace Taffy.Web {
    public class Global : HttpApplication {
        private static readonly IUrlyTransformer UrlyTransformer = new UrlyTransformer(new ApplicationCache());
        private static readonly List<string> IgnorableFileNames = new List<string> { Constants.FavIconFilename, Constants.WebResourceFilename };

        protected void Application_BeginRequest(object sender, EventArgs e) {
            RewriteUrl();
        }

        private static void RewriteUrl() {
            var httpContext = HttpContext.Current;
            var request = httpContext.Request;
            var path = request.Path;
            var pathFileName = VirtualPathUtility.GetFileName(path);
            var isRequestForDefaultDocument = request.ApplicationPath.Equals("/" + pathFileName);
            if (!IgnorableFileNames.Contains(pathFileName) && !isRequestForDefaultDocument && !System.IO.File.Exists(HttpContext.Current.Server.MapPath(pathFileName))) {
                var podcastUrl = GetPodcastUrl(request);
                var fileUrl = Constants.FilePageVirtualPath + Constants.UrlQueryStringDelimiter + Constants.FileSourceParameterName + Constants.QueryStringNameValuePathDelimiter + podcastUrl;
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