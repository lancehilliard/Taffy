using System;
using System.Web.UI;
using Taffy.Configuration;

namespace Taffy.Web {
    public partial class Preferences : Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                ErrorFeedbackEnabledCheckBox.Checked = Settings.ErrorFeedbackEnabled;
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e) {
            Settings.ErrorFeedbackEnabled = ErrorFeedbackEnabledCheckBox.Checked;
        }
    }
}