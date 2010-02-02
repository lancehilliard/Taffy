using System;
using System.Configuration;
using System.Web;
using System.Web.Hosting;
using Elmah;

namespace Taffy.Configuration {
    public class Settings {
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
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["NumberOfHoursToCacheStretchedPodcasts"] ?? Constants.NumberOfHoursToCacheStretchedPodcasts.ToString()); }
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
    }
}