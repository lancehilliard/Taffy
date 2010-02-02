using System;
using System.Net;
using System.Web.UI;
using Taffy.Transform;

namespace Taffy.Web {
    public partial class ErrorReporting : BasePage {
        protected void Page_Load(object sender, EventArgs e) {
            IUrlTransformer urlTransformer = new UrlTransformer(null);
            var requestUriString = urlTransformer.ConvertRelativeUrlToAbsoluteUrl("~/elmah.axd");
            var elmahIsOnline = ElmahIsOnline(requestUriString);
            ElmahEnabledPanel.Visible = elmahIsOnline;
            ElmahDisabledPanel.Visible = !elmahIsOnline;
        }

    }
}