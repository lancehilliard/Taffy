using Taffy.Configuration;

namespace Taffy.Transform.Id3 {
    public interface IId3TransformerFactory {
        IId3Transformer GetTransformer(TransformerTypes transformerType);
    }
}