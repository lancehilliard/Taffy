using System;
using System.Reflection;

namespace Taffy.Configuration {
    public class Messages {
        public static string ErrorUrlNotWellFormed = "Error: Original RSS URL is not well-formed.";
        public static string VersionTemplate = "{0}.{1} ({2})";
        public static string ErrorUnableToCreateOpmlFile = "Unable to create OPML file.";
        public static string ErrorUnableToCreateTaffyFeed = "Unable to create Taffy feed.";
        public static string ErrorUnableToServeFile = "Unable to serve file.";
        public static string ErrorInvalidPodcatcherTaffyAddress = "The Taffy Address is not valid.";

        public static string ViewErrorReportingForMoreInformation(string message) {
            return "<p>" + message + "</p><p>If Error Reporting is enabled, it may provide more information.</p>";
        }

    }
}