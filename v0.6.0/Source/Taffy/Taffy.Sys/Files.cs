using System.Collections.Generic;
using System.IO;

namespace Taffy.Sys {
    public class Files {
        public static void CreateTemporaryFiles(out string wavFileName, out string stretchedWavFileName, out string stretchedMp3FileName, out string sourceMp3TempFileName) {
            wavFileName = Path.GetTempFileName();
            stretchedWavFileName = Path.GetTempFileName();
            stretchedMp3FileName = Path.GetTempFileName();
            sourceMp3TempFileName = Path.GetTempFileName();
        }

        public static void DeleteFiles(IEnumerable<string> filePathsToDelete) {
            foreach (var filePathToDelete in filePathsToDelete) {
                File.Delete(filePathToDelete);
            }
        }
    }
}