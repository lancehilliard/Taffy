using Taffy.Configuration;

namespace Taffy.Transform.Audio {
    public interface IAudioTransformerFactory {
        IAudioTransformer GetTransformer(TransformerTypes transformerType);
    }
}