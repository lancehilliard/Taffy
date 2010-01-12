using System;
using Taffy.Configuration;
using Taffy.Transform.WavTransformers;

namespace Taffy.Transform {
    public class WavTransformerFactory {
        public static ITransformer GetTransformer(TransformerTypes transformerType) {
            ITransformer result;
            switch (transformerType) {
                case TransformerTypes.CommandLine:
                    result = new WavCommandLineTransformer();
                    break;
                case TransformerTypes.Library:
                    result = new WavLibraryTransformer();
                    break;
                default:
                    throw new Exception("Unknown transformer type.");
            }
            return result;
        }
    }
}