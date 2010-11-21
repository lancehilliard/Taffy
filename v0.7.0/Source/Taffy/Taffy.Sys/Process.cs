using System.Diagnostics;

namespace Taffy.Sys {
    public class Process {
        public static void Execute(string fileName, string arguments, bool waitForExit) {
            var processStartInfo = GetProcessStartInfo(fileName, arguments);
            var process = System.Diagnostics.Process.Start(processStartInfo);
            if (process != null && waitForExit) {
                process.WaitForExit();
            }
        }

        private static ProcessStartInfo GetProcessStartInfo(string fileName, string arguments) {
            var processStartInfo = new ProcessStartInfo(fileName, arguments) {UseShellExecute = false, CreateNoWindow = true};
            return processStartInfo;
        }

    }
}