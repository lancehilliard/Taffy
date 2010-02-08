using System;
using System.Collections.Generic;
using System.IO;
using Taffy.Configuration;
using Taffy.Sys;

namespace Taffy.Transform.Audio {
    public class Mp3CommandLineTransformer : IAudioTransformer {
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

        public void Concatenate(string[] inputFiles, string combinedFileName) {
            var memoryStream = new MemoryStream();
            foreach (var inputFile in inputFiles) {
                var inputFileBytes = File.ReadAllBytes(inputFile);
                memoryStream.Write(inputFileBytes, 0, inputFileBytes.Length);
            }
            File.WriteAllBytes(combinedFileName, memoryStream.ToArray());
            //var inputFilesList = string.Join(@"""+""", inputFiles);
            //var arguments = @"/c copy /b """ + inputFilesList + @""" """ + combinedFileName + @"""";
            //Process.Execute("cmd", arguments, true);
        }
    }
}