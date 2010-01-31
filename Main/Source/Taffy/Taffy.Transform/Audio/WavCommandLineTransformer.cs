using System;
using System.Web;
using Taffy.Configuration;
using Taffy.Sys;

namespace Taffy.Transform.Audio {
    public class WavCommandLineTransformer : IAudioTransformer {
        public void ToWav(string mp3FileName, string wavFileName) {
            throw new NotImplementedException();
        }

        public void ToMp3(string wavFileName, string mp3FileName) {
            // lame "%FILEPATH%.fast.wav" "%FILEPATH%.fast.mp3"
            var arguments = @"""" + wavFileName + @""" """ + mp3FileName + @"""";
            Process.Execute(Settings.LameFileName, arguments, true);
        }

        public void Stretch(string wavFileName, string stretchedWavFileName) {
            // soundstretch "%FILEPATH%.wav" "%FILEPATH%.fast.wav" -tempo=+35
            var arguments = @"""" + wavFileName + @""" """ + stretchedWavFileName + @""" -tempo=+35";
            Process.Execute(Settings.SoundStretchFileName, arguments, true);
        }
    }
}