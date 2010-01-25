using System;
using Taffy.Configuration;

namespace Taffy.Web {
    public class BasePage : System.Web.UI.Page {
        protected bool IsDebugRequest() {
            var result = Boolean.TrueString.Equals(Request[Constants.DebugUrlParameterName], StringComparison.InvariantCultureIgnoreCase);
            return result;
        }
    }
}