using System;
using Taffy.Configuration;

namespace Taffy.Transform.Id3 {
    public class Id3TransformerFactory : IId3TransformerFactory {
        public IId3Transformer GetTransformer(TransformerTypes transformerType) {
            IId3Transformer result;
            switch (transformerType) {
                case TransformerTypes.CommandLine:
                    result = new Id3CommandLineTransformer();
                    break;
                case TransformerTypes.Library:
                    result = new Id3LibraryTransformer();
                    break;
                default:
                    throw new Exception(Constants.ErrorMessageUnknownTransformerType);
            }
            return result;
        }
    }
}