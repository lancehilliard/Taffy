using System.IO;
using Mp3Lib;

namespace Taffy.Transform.File {
    public class FileTransformer : IFileTransformer {
        public void Concatenate(string[] inputFiles, string resultFileName) {
            var memoryStream = new MemoryStream();
            foreach (var inputFile in inputFiles) {
                var inputFileBytes = System.IO.File.ReadAllBytes(inputFile);
                memoryStream.Write(inputFileBytes, 0, inputFileBytes.Length);
            }
            var resultBytes = memoryStream.ToArray();
            System.IO.File.WriteAllBytes(resultFileName, resultBytes);
        }
    }

    public interface IFileTransformer {
        void Concatenate(string[] inputFiles, string resultFileName);
    }
}