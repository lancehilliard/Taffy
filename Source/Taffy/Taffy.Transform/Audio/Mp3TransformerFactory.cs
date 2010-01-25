using System;
using Taffy.Configuration;

namespace Taffy.Transform.Audio {
    public class Mp3TransformerFactory : IAudioTransformerFactory {
        public IAudioTransformer GetTransformer(TransformerTypes transformerType) {
            IAudioTransformer result;
            switch (transformerType) {
                case TransformerTypes.CommandLine:
                    result = new Mp3CommandLineTransformer();
                    break;
                case TransformerTypes.Library:
                    result = new Mp3LibraryTransformer();
                    break;
                default:
                    throw new Exception(Constants.ErrorMessageUnknownTransformerType);
            }
            return result;
        }
    }
}