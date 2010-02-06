using System;
using System.Web.UI;
using Taffy.Configuration;

namespace Taffy.Web {
    public partial class LameSetup : Page {
        protected void Page_Load(object sender, EventArgs e) {
            var lameMissing = !System.IO.File.Exists(Settings.LameFileName);
            LameNotInstalledContainer.Visible = lameMissing;
            LameInstalledContainer.Visible = !lameMissing;
            if (lameMissing) {
                LameConfiguredPathLabel.Text = Settings.LameFileName;
                LameDownloadHyperLink.NavigateUrl = Constants.DependencyDownloadsUrl;
            }
        }

        public static bool IsLameMissing() {
            return !System.IO.File.Exists(Settings.LameFileName);
        }
    }
}