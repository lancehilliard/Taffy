using System;
using System.Configuration;

namespace Taffy.Configuration {
    public class Settings {
        public static string Mpg123FileName {
            get { return ConfigurationManager.AppSettings["Mpg123FileName"]; }
        }

        public static string SoundStretchFileName {
            get { return ConfigurationManager.AppSettings["SoundStretchFileName"]; }
        }

        public static string LameFileName {
            get { return ConfigurationManager.AppSettings["LameFileName"]; }
        }

        public static int NumberOfHoursToCachePodcastDownloads {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["NumberOfHoursToCachePodcastDownloads"] ?? "0"); }
        }

        public static TransformerTypes TransformerType {
            get {
                var transformerType = (TransformerTypes)Enum.Parse(typeof(TransformerTypes), ConfigurationManager.AppSettings["TransformerType"]);
                return transformerType;
            }
        }
    }
}