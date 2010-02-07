using System;
using Taffy.Configuration;

namespace Taffy.Transform.Audio {
    public class WavTransformerFactory : IAudioTransformerFactory {
        public IAudioTransformer GetTransformer(TransformerTypes transformerType) {
            IAudioTransformer result;
            switch (transformerType) {
                case TransformerTypes.CommandLine:
                    result = new WavCommandLineTransformer();
                    break;
                case TransformerTypes.Library:
                    result = new WavLibraryTransformer();
                    break;
                default:
                    throw new Exception(Constants.ErrorMessageUnknownTransformerType);
            }
            return result;
        }
    }
}