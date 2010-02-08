using System;
using System.Configuration;
using System.Web;
using System.Web.Hosting;
using Elmah;

namespace Taffy.Configuration {
    public class Settings {
        private static readonly System.Configuration.Configuration WebConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
        private const string AppendOriginalAudioEnabledKey = "AppendOriginalAudioEnabled";
        private const string ErrorFeedbackEnabledKey = "ErrorFeedbackEnabled";

        public static string Mpg123FileName {
            get {
                return HostingEnvironment.MapPath(Constants.Mpg123VirtualPathFileName);
            }
            //get { return ConfigurationManager.AppSettings["Mpg123FileName"]; }
        }

        public static string SoundStretchFileName {
            get {
                return HostingEnvironment.MapPath(Constants.SoundStretchVirtualPathFileName);
            }
            //get { return ConfigurationManager.AppSettings["SoundStretchFileName"]; }
        }

        public static string LameFileName {
            get {
                return HostingEnvironment.MapPath(Constants.LameVirtualPathFileName);
            }
            //get { return ConfigurationManager.AppSettings["LameFileName"]; }
        }

        public static int NumberOfHoursToCacheStretchedPodcasts {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["NumberOfHoursToCacheStretchedPodcasts"] ?? Constants.NumberOfHoursToCacheStretchedPodcastsDefault.ToString()); }
        }

        public static TransformerTypes TransformerType {
            get {
                TransformerTypes transformerType;
                try {
                    transformerType = (TransformerTypes)Enum.Parse(typeof(TransformerTypes), ConfigurationManager.AppSettings["TransformerType"]);
                }
                catch (Exception e) {
                    ErrorSignal.FromCurrentContext().Raise(e);
                    transformerType = Constants.TransformerTypeDefault;
                }
                return transformerType;
            }
        }

        public static int ErrorFeedbackConnectionTimeoutInSeconds {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["ErrorFeedbackConnectionTimeoutInSeconds"] ?? Constants.ErrorFeedbackConnectionTimeoutInSecondsDefault.ToString()); }
        }

        static Settings() {
#if DEBUG
            ConfigurationManager.AppSettings.Set(ErrorFeedbackEnabledKey, bool.TrueString);
            ConfigurationManager.AppSettings.Set(AppendOriginalAudioEnabledKey, bool.TrueString);
#endif
        }

        public static bool ErrorFeedbackEnabled {
            get {
                bool result;
                if (!bool.TryParse(ConfigurationManager.AppSettings[ErrorFeedbackEnabledKey], out result)) {
                    result = Constants.ErrorFeedbackEnabledDefault;
                }
                return result;
            }
            set { SetSetting(ErrorFeedbackEnabledKey, value.ToString()); }
        }

        public static bool AppendOriginalAudioEnabled {
            get {
                bool result;
                if (!bool.TryParse(ConfigurationManager.AppSettings[AppendOriginalAudioEnabledKey], out result)) {
                    result = Constants.AppendOriginalAudioEnabledDefault;
                }
                return result;
            }
            set { SetSetting(AppendOriginalAudioEnabledKey, value.ToString()); }
        }

        private static void SetSetting(string key, string value) {
            ConfigurationManager.AppSettings.Set(key, value);
            var appSetting = WebConfiguration.AppSettings.Settings[key];
            appSetting.Value = value;
            WebConfiguration.Save();
        }
    }
}