using System.Collections.Generic;
using System.IO;

namespace Taffy.Sys {
    public class Files {
        public static void CreateTemporaryFiles(out string wavFileName, out string stretchedWavFileName, out string stretchedMp3FileName) {
            wavFileName = Path.GetTempFileName();
            stretchedWavFileName = Path.GetTempFileName();
            stretchedMp3FileName = Path.GetTempFileName();
        }

        public static void DeleteFiles(IEnumerable<string> filePathsToDelete) {
            foreach (var filePathToDelete in filePathsToDelete) {
                File.Delete(filePathToDelete);
            }
        }
    }
}