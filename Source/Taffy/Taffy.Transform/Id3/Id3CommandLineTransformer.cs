using Taffy.Configuration;
using Taffy.Sys;

namespace Taffy.Transform.Id3 {
    public class Id3CommandLineTransformer : IId3Transformer {
        #region IId3Transformer Members

        public void CopyId3Tags(string sourceFileName, string destinationFileName) {
            // id3 -D source.mp3 -1 -2 dest.mp3
            var arguments = @"-D """ + sourceFileName + @""" -1 -2 """ + destinationFileName + @"""";
            Process.Execute(Settings.Id3FileName, arguments, true);
        }

        #endregion
    }
}