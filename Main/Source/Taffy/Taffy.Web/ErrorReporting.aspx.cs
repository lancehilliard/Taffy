using System;
using System.Net;
using System.Web.UI;
using Taffy.Transform;

namespace Taffy.Web {
    public partial class ErrorReporting : Page {
        protected void Page_Load(object sender, EventArgs e) {
            IUrlTransformer urlTransformer = new UrlTransformer(null);
            var requestUriString = urlTransformer.ConvertRelativeUrlToAbsoluteUrl("~/elmah.axd");
            var elmahIsOnline = ElmahIsOnline(requestUriString);
            ElmahEnabledPanel.Visible = elmahIsOnline;
            ElmahDisabledPanel.Visible = !elmahIsOnline;
        }

        private static bool ElmahIsOnline(string requestUriString) {
            bool result;
            var webRequest = WebRequest.Create(requestUriString);
            try {
                webRequest.GetResponse();
                result = true;
            }
            catch (Exception e) {
                result = false;
            }
            return result;
        }
    }
}