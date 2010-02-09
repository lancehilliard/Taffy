using System;
using Taffy.Configuration;

namespace Taffy.Transform.File {
    public class FileTransformerFactory : IFileTransformerFactory {
        public IFileTransformer GetTransformer() {
            IFileTransformer result = new FileTransformer();
            return result;
        }
    }

    public interface IFileTransformerFactory {
        IFileTransformer GetTransformer();
    }
}