using System;
using System.Collections.Generic;

namespace Taffy.Transform.Audio {
    public class Mp3LibraryTransformer : IAudioTransformer {
        public void ToWav(string mp3FileName, string wavFileName) {
            throw new NotImplementedException();
        }

        public void ToMp3(string wavFileName, string mp3FileName) {
            throw new NotImplementedException();
        }

        public void Stretch(string wavFileName, string stretchedWavFileName) {
            throw new NotImplementedException();
        }

        public byte[] Concatenate(string[] inputFiles) {
            throw new NotImplementedException();
        }
    }
}