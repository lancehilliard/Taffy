using System;
using System.Web.UI;
using Taffy.Configuration;

namespace Taffy.Web {
    public partial class Taffy : MasterPage {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                HandleGet();
            }
            LameNotInstalledContainer.Visible = !System.IO.File.Exists(Settings.LameFileName);
        }

        private void HandleGet() {
            var assemblyVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            VersionLabel.Text = string.Format(Messages.VersionTemplate, assemblyVersion.Major, assemblyVersion.Minor, assemblyVersion.MajorRevision, assemblyVersion.MinorRevision);
        }
    }
}