using System.Collections.Generic;

namespace Taffy.Transform.Audio {
    public interface IAudioTransformer {
        void ToWav(string mp3FileName, string wavFileName);
        void ToMp3(string wavFileName, string mp3FileName);
        void Stretch(string wavFileName, string stretchedWavFileName, int accelerationPercentage);
    }
}