using System;
using System.Reflection;
using System.Web.UI;
using Taffy.Configuration;

namespace Taffy.Web {
    public partial class Taffy : MasterPage {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                HandleGetRequest();
                BetaLiteral.Visible = Constants.IsBetaRelease;
            }
            LameNotInstalledContainer.Visible = LameSetup.IsLameMissing();
        }

        private void HandleGetRequest() {
            VersionLabel.Text = string.Format(Messages.VersionTemplate, AssemblyVersion.Major, AssemblyVersion.Minor, BuildDate);
        }

        public string BuildDate {
            get {
                var version = AssemblyVersion;
                var daysSince20000101 = TimeSpan.TicksPerDay * version.Build;
                var secondsSinceMidnightAtMomentOfBuild = TimeSpan.TicksPerSecond * 2 * version.Revision;
                var buildDateTime = new DateTime(2000, 1, 1).Add(new TimeSpan(daysSince20000101 + secondsSinceMidnightAtMomentOfBuild));
                var result = buildDateTime.ToString(Constants.BuildDateMessageDateFormatString);
                return result;
            }
        }

        protected Version AssemblyVersion {
            get {
                var result = Assembly.GetExecutingAssembly().GetName().Version;
                return result;
            }
        }
    }
}