using System;
using System.Threading;
using Taffy.Configuration;
using Taffy.Transform.Mp3Transformers;

namespace Taffy.Transform {
    public class Mp3TransformerFactory {
        public static ITransformer GetTransformer(TransformerTypes transformerType) {
            ITransformer result;
            switch (transformerType) {
                case TransformerTypes.CommandLine:
                    result = new Mp3CommandLineTransformer();
                    break;
                case TransformerTypes.Library:
                    result = new Mp3LibraryTransformer();
                    break;
                default:
                    throw new Exception("Unknown transformer type.");
            }
            return result;
        }
    }
}