using System;
using System.Net;
using Elmah;
using Taffy.Configuration;

namespace Taffy.Web {
    public class BasePage : System.Web.UI.Page {
        protected bool IsDebugRequest() {
            var result = Boolean.TrueString.Equals(Request[Constants.DebugUrlParameterName], StringComparison.InvariantCultureIgnoreCase);
            return result;
        }

        protected void ShowAlert(string message) {
            var script = "$(document).ready(function(){showAlert('" + message.Replace("'", @"\x27") + "');});";
            script = @"<script type=""text/javascript"">" + script + "</script>";
            Page.ClientScript.RegisterStartupScript(typeof(string), Constants.AlertScriptKey, script);
        }

        protected bool ElmahIsOnline(string requestUriString) {
            bool result;
            var webRequest = WebRequest.Create(requestUriString);
            try {
                webRequest.GetResponse();
                result = true;
            }
            catch (Exception) {
                result = false;
            }
            return result;
        }

        protected void DisplayError(string message, Exception exception) {
            string messageToDisplay;
            if (exception != null) {
                var exceptionToLog = new Exception(message, exception);
                ErrorSignal.FromCurrentContext().Raise(exceptionToLog);
                messageToDisplay = Messages.ViewErrorReportingForMoreInformation(message);
            }
            else {
                messageToDisplay = message;
            }
            ShowAlert(messageToDisplay);
        }

        protected static bool IsUrlAbsoluteAndValid(string taffyAddress) {
            var result = Uri.IsWellFormedUriString(taffyAddress, UriKind.Absolute);
            return result;
        }
    }
}