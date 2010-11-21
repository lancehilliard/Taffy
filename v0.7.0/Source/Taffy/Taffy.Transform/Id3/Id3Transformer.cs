using Mp3Lib;

namespace Taffy.Transform.Id3 {
    public interface IId3Transformer {
        void CopyId3Tags(string sourceFileName, string destinationFileName);
    }
}