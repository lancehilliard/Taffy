using System;
using Taffy.Configuration;
using Taffy.Sys;

namespace Taffy.Transform.Mp3Transformers {
    public class Mp3CommandLineTransformer : ITransformer {
        public void ToWav(string mp3FileName, string wavFileName) {
            // mpg123 --wav "%FILEPATH%.wav" "%FILEPATH%"
            var arguments = @"--wav """ + wavFileName + @""" """ + mp3FileName + @"""";
            Process.Execute(Settings.Mpg123FileName, arguments, true);
        }

        public void ToMp3(string wavFileName, string mp3FileName) {
            throw new NotImplementedException();
        }

        public void Stretch(string wavFileName, string stretchedWavFileName) {
            throw new NotImplementedException();
        }
    }
}