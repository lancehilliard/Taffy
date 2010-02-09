using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using Taffy.Configuration;
using Taffy.Memory;
using Taffy.Sys;
using Taffy.Transform;
using Taffy.Transform.Audio;
using Taffy.Transform.File;
using Taffy.Transform.Id3;

namespace Taffy.Web {
    public partial class File : BasePage {
        private IUrlTransformer _urlTransformer;
        private IAudioTransformerFactory _mp3TransformerFactory;
        private IAudioTransformerFactory _wavTransformerFactory;
        private IId3TransformerFactory _id3TransformerFactory;
        private IFileTransformerFactory _fileTransformerFactory;
        private IApplicationCache _applicationCache;

        protected void Page_Init(object sender, EventArgs e) {
            _applicationCache = new ApplicationCache();
            IUrlyTransformer urlyTransformer = new UrlyTransformer(_applicationCache);
            _urlTransformer = new UrlTransformer(urlyTransformer);
            _mp3TransformerFactory = new Mp3TransformerFactory();
            _wavTransformerFactory = new WavTransformerFactory();
            _id3TransformerFactory = new Id3TransformerFactory();
            _fileTransformerFactory = new FileTransformerFactory();
        }

        protected void Page_Load(object sender, EventArgs e) {
            var sourceHref = Request[Constants.FileSourceParameterName];
            if (sourceHref == null && IsDebugRequest()) {
                sourceHref = "http://www.theonion.com/content/files/radionews/W09_001_World_Sun.mp3"; 
            }
            var originalFileName = _urlTransformer.GetFileName(sourceHref);

            PrepareResponse(originalFileName);

            var downloadFileName = Path.GetTempFileName();
            var cacheKey = Guid.NewGuid().ToString();
            var myThread = new Thread(() => GetTransformedBytes(sourceHref, downloadFileName, cacheKey)) { IsBackground = true };
            myThread.Start();

            // This code will be revisisted when the time comes to put an intro MP3 on the download.  In the meantime, the background thread will still be used, since it's harmless.
            //var introBytes = System.IO.File.ReadAllBytes(@"C:\TaffyDependencies\taffy.mp3.mp3");
            //WriteBytesToResponse(introBytes);

            byte[] transformedBytes = null;
            while (transformedBytes == null) {
                transformedBytes = _applicationCache.Get(cacheKey) as byte[];
            }

            WriteBytesToResponse(transformedBytes);
            _applicationCache.Remove(cacheKey);
            Response.End();
        }

        private void PrepareResponse(string originalFileName) {
            Response.BufferOutput = false;
            Response.Clear();
            Response.ContentType = Constants.Mp3ResponseContentType;
            var contentDisposition = string.Format(Constants.FilenameResponseContentDispositionHeaderValueTemplate, originalFileName);
            Response.AddHeader(Constants.ResponseContentDispositionHeaderName, contentDisposition);
        }

        private void GetTransformedBytes(string sourceHref, string sourceFileName, string cacheKey) {
            var sourceBytes = _applicationCache.Get(sourceHref) as byte[];
            if (sourceBytes == null) {
                DownloadFile(sourceHref, sourceFileName);
                var mp3Transformer = _mp3TransformerFactory.GetTransformer(Settings.TransformerType);
                var wavTransformer = _wavTransformerFactory.GetTransformer(Settings.TransformerType);
                var id3Transformer = _id3TransformerFactory.GetTransformer(Settings.TransformerType);
                var fileTransformer = _fileTransformerFactory.GetTransformer();
                string wavTempFileName, stretchedWavTempFileName, stretchedMp3TempFileName, sourceMp3TempFileName, concatenatedMp3TempFileName;
                Files.CreateTemporaryFiles(out wavTempFileName, out stretchedWavTempFileName, out stretchedMp3TempFileName, out sourceMp3TempFileName, out concatenatedMp3TempFileName);
                mp3Transformer.ToWav(sourceFileName, wavTempFileName);
                wavTransformer.Stretch(wavTempFileName, stretchedWavTempFileName);
                wavTransformer.ToMp3(wavTempFileName, stretchedMp3TempFileName);
                wavTransformer.ToMp3(stretchedWavTempFileName, stretchedMp3TempFileName);
                string sourceBytesFileName;
                if (Settings.AppendOriginalAudioEnabled) {
                    wavTransformer.ToMp3(wavTempFileName, sourceMp3TempFileName);
                    var inputFiles = new[] { stretchedMp3TempFileName, sourceMp3TempFileName };
                    fileTransformer.Concatenate(inputFiles, concatenatedMp3TempFileName);
                    sourceBytesFileName = concatenatedMp3TempFileName;
                }
                else {
                    sourceBytesFileName = stretchedMp3TempFileName;
                }
                id3Transformer.CopyId3Tags(sourceFileName, sourceBytesFileName);
                sourceBytes = System.IO.File.ReadAllBytes(sourceBytesFileName);
                if (sourceBytes.Length > 0) {
                    _applicationCache.Add(sourceHref, sourceBytes, DateTime.Now.AddHours(Settings.NumberOfHoursToCacheStretchedPodcasts));
                }
                Files.DeleteFiles(new List<string> { wavTempFileName, stretchedWavTempFileName, stretchedMp3TempFileName, sourceFileName, sourceMp3TempFileName, concatenatedMp3TempFileName });
            }
            _applicationCache.Add(cacheKey, sourceBytes);
        }

        private void WriteBytesToResponse(byte[] sourceBytes) {
            if (sourceBytes != null && sourceBytes.Length > 0) {
                Response.AddHeader(Constants.ResponseContentLengthHeaderName, sourceBytes.Length.ToString());
                using (var sourceStream = new MemoryStream(sourceBytes, false)) {
                    sourceStream.WriteTo(Response.OutputStream);
                }
            }
        }

        private static void DownloadFile(string url, string destinationFileName) {
            var client = new WebClient();
            client.DownloadFile(url, destinationFileName);
        }
    }
}